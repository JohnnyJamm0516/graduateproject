using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


[AddComponentMenu("Headache/Scripts/ActionManager")]
public class ActionManager : MonoBehaviour
{
    // ��ü ��� ��ũ��Ʈ
    // �׼� �Ŵ����� �־��ְ� �߰� ������Ʈ�� MonoBehaviour,IAcionable �� ��ӹ޴� Ŭ���� ����
    // �׷��� ������� Ŭ������ GetActions()�� ��ȯ �Լ� �����


    //���� �κ�

    //�׽�Ʈ awake
    [SerializeField]
    public Dictionary<string, Action<string>> ActionList = new();
    [SerializeField]
    GameObject actionManager;
    private void Awake()
    {
        actionManager = gameObject;
        Init_SO();
    }


    //�Լ� ��ųʸ� �߰�
    private void Init_SO()
    {
        IActionable[] actionComponent = actionManager.GetComponents<IActionable>();
        foreach (IActionable actionable in actionComponent)
        {
            AddActions(actionable);
        }
    }

    //�Լ� ��ü ����
    public void ExecuteEffects(string effectString)
    {
        List<string> functionInfo = parseTextToData(effectString);
        //�Լ��� ��� ��������ִ� �ϰ� ���� �Լ� ����
        //Debug.Log("******************************" + effectString + "******************************");
        foreach (string fInfo in functionInfo)
        {
            ExecuteFunction(fInfo);
        }
        //Debug.Log("******************************" + effectString + " End" + "******************************");

    }

    //�Լ� �ؽ�Ʈ �и� ��� ���� �Է� "{Func1(123)}{Func()}" => list[0] = "Func1(123)", list[1] = "Func()"
    public static List<string> parseTextToData(string effectString)
    {
        //�Է¹��� ������ �и� �Լ�

        //{}�ܸ����� ���� �ܸ� �и�
        string pattern = @"\{(.*?)\}";

        //�������� �˻� �� ������ �и�
        MatchCollection matchs = Regex.Matches(effectString, pattern);

        List<string> functionInfoList = new();


        //�и��� �����͸� ���� �Լ� ȣ��
        foreach (Match match in matchs)
        {
            //�����Ͱ� �ҷ��� �����κ�
            if (match.Success)
            {
                //�Լ� �̸� �и�
                string functionInfo = match.Groups[1].Value;

                functionInfoList.Add(functionInfo);
            }
        }
        return functionInfoList;
    }


    //���� �Լ�
    //�Լ� �븮 ����
    private void ExecuteFunction(string functionInfo)
    {
        //�븮 �Լ� ���� ���

        //�Լ� �̸�, �Ķ���� �и�
        string pattern = @"(\w+)\((.*?)\)";
        Match match = Regex.Match(functionInfo, pattern);


        //���� �κ� ���� ó��
        if (match.Success)
        {
            string funcName = match.Groups[1].Value;
            string funcParm = match.Groups[2].Value;
            //Debug.Log("=========================="+funcName+ "==========================");
            FindActionWithNameAndExecute(funcName, funcParm); 
            //Debug.Log("==========================" + funcName + " End" + "==========================");
        }
    }


    //�Լ� ���� �и��� ����Ʈ ��ȯ �Լ�
    private static List<FuncInfo> parseFunctionData(List<string> functionInfos)
    {
        //�Է� ���� functionInfo[0] = "Func1(500)"


        //�븮 �Լ� ���� ���

        //�Լ� �̸�, �Ķ���� �и�
        string pattern = @"(\w+)\((.*?)\)";
        List<FuncInfo> funcInfos = new();
        foreach (string str in functionInfos)
        {
            Match match = Regex.Match(str, pattern);
            if (match.Success)
            {
                FuncInfo funcInfo = new();
                string funcName = match.Groups[1].Value;
                string funcParm = match.Groups[2].Value;
                funcInfo.funcName = funcName;
                funcInfo.funcParm = funcParm;
                funcInfos.Add(funcInfo);
            }
        }


        //���� �κ� ���� ó��
        return funcInfos;
    }

    //�̸����� �Լ� ã�� �ֱ�
    private void FindActionWithNameAndExecute(string funcName, string parm)
    {
        if (!ActionList.ContainsKey(funcName))
        {
            Debug.Log("���� �Լ�");
        }
        else
        {
            ActionList[funcName].Invoke(parm);
        }
    }

    //�������̽� ���� �߰� �Լ�
    private void AddActions(IActionable actionable)
    {
        foreach (var action in actionable.GetActions())
        {
            ActionList.Add(action.Key, action.Value);
        }
    }
}
public struct FuncInfo
{
    public string funcName;
    public string funcParm;
}