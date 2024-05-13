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
    // 객체 통괄 스크립트
    // 액션 매니저에 넣어주고 추가 컴포넌트로 MonoBehaviour,IAcionable 를 상속받는 클래스 생성
    // 그렇게 만들어진 클래스에 GetActions()에 반환 함수 만들기


    //예제 부분

    //테스트 awake
    [SerializeField]
    public Dictionary<string, Action<string>> ActionList = new();
    [SerializeField]
    GameObject actionManager;
    private void Awake()
    {
        actionManager = gameObject;
        Init_SO();
    }


    //함수 딕셔너리 추가
    private void Init_SO()
    {
        IActionable[] actionComponent = actionManager.GetComponents<IActionable>();
        foreach (IActionable actionable in actionComponent)
        {
            AddActions(actionable);
        }
    }

    //함수 전체 실행
    public void ExecuteEffects(string effectString)
    {
        List<string> functionInfo = parseTextToData(effectString);
        //함수를 대신 실행시켜주는 일괄 실행 함수 실행
        //Debug.Log("******************************" + effectString + "******************************");
        foreach (string fInfo in functionInfo)
        {
            ExecuteFunction(fInfo);
        }
        //Debug.Log("******************************" + effectString + " End" + "******************************");

    }

    //함수 텍스트 분리 모듈 예시 입력 "{Func1(123)}{Func()}" => list[0] = "Func1(123)", list[1] = "Func()"
    public static List<string> parseTextToData(string effectString)
    {
        //입력받은 데이터 분리 함수

        //{}단막으로 묶인 단막 분리
        string pattern = @"\{(.*?)\}";

        //패턴으로 검색 후 데이터 분리
        MatchCollection matchs = Regex.Matches(effectString, pattern);

        List<string> functionInfoList = new();


        //분리된 데이터를 통한 함수 호출
        foreach (Match match in matchs)
        {
            //데이터가 불량시 판정부분
            if (match.Success)
            {
                //함수 이름 분리
                string functionInfo = match.Groups[1].Value;

                functionInfoList.Add(functionInfo);
            }
        }
        return functionInfoList;
    }


    //원형 함수
    //함수 대리 실행
    private void ExecuteFunction(string functionInfo)
    {
        //대리 함수 실행 기능

        //함수 이름, 파라미터 분리
        string pattern = @"(\w+)\((.*?)\)";
        Match match = Regex.Match(functionInfo, pattern);


        //실행 부분 예외 처리
        if (match.Success)
        {
            string funcName = match.Groups[1].Value;
            string funcParm = match.Groups[2].Value;
            //Debug.Log("=========================="+funcName+ "==========================");
            FindActionWithNameAndExecute(funcName, funcParm); 
            //Debug.Log("==========================" + funcName + " End" + "==========================");
        }
    }


    //함수 정보 분리후 리스트 반환 함수
    private static List<FuncInfo> parseFunctionData(List<string> functionInfos)
    {
        //입력 예제 functionInfo[0] = "Func1(500)"


        //대리 함수 실행 기능

        //함수 이름, 파라미터 분리
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


        //실행 부분 예외 처리
        return funcInfos;
    }

    //이름으로 함수 찾아 주기
    private void FindActionWithNameAndExecute(string funcName, string parm)
    {
        if (!ActionList.ContainsKey(funcName))
        {
            Debug.Log("없는 함수");
        }
        else
        {
            ActionList[funcName].Invoke(parm);
        }
    }

    //인터페이스 예시 추가 함수
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