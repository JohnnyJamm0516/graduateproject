using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Headache/Scripts/VariableManager")]
public class VariableManager : MonoBehaviour
{
    MyAttributes myAttributes;
    Dictionary<string, float> globalVariable;
    private void Start()
    {
        try
        {
            myAttributes = Game_Manager.instance.GetComponent<MyAttributes>();
            globalVariable = myAttributes.globalVariable;
        }
        catch
        {
            Debug.Log("���� �Ŵ��� ����");
        }
    }
    public Dictionary<string, float> GlobalVariable
    {
        get;
        set;
    }
}
