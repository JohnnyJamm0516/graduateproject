using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using JetBrains.Annotations;

[Serializable]
public class CardData
{
    //����
    //�ൿ�� ����Ʈ�� �������� �κ�
    //ī��� ���õ� �����͸� �����ϴ� ������

    //���� �ʿ�
    //1.�Լ�(�ൿ)�� �̸� ��������
    //2.�Լ� �븮��(delegate) ��������
    //3.�̸��� �����Ϳ��� �������� �ϵ��� �������� �� ������ ���� �� �����ϸ� ��
    //4.�븮�ڴ� ���� ī���� �븮��(delegate) ����(�Ǵ� Action)�� ���Ͽ� �����ϵ��� ���� ��
    

    //�ؿ� �κ��� ���� ����� ����Ʈ�� ������ ����

    //�ؿ� �κ��� ��� ����� ����Ʈ�� ������ ����
    public DeffenceModule deffenceDatas;//���
}

[Serializable]
public class Card_UI
{
    //ī���� ��ü�� ����ϴ� ���� Ʋ

    public string card_name;            //�̸�
    public string cost;                 //�Ҹ� ����
    public string text;                 //��� ����
    public string var;
    public string attribute;            //�󼺼Ӽ� #�ļ���
    public string rarity;
    public string type;                 //����, ��� �� �ൿ Ư��
    public string effect_Template;      //ȿ�� ����
    public string cumulativeChainEffect;//ü�� ��ȭ ����
    public GameObject variableManager;
    public float effect
    {
        //ī���� ȿ���� ������ �о���� �κ�
        get
        {
            Dictionary<string, object> data_Dictionary = ConvertStringToDictionary(effect_Template);
            string str = CalculateAndReplace(effect_Template, Data_Dictionary);
            return Convert.ToSingle(str);
        }
    }                //ȿ�� ���� ��������
    public string image;
    public string rarity_Image;
    public string type_Image;
    public int cardCount;



    //���� ��ųʸ� �и� ���. �� ���� ī���� ���� �Ǵ� ������. �� ���� ���̸鼭 ���� ���� ���� �켱�� �ؼ� ����ϵ��� �ٲ� ��.
    public Dictionary<string, object> Data_Dictionary
    {
        get
        {
            Dictionary<string, object> data_dictionary = ConvertStringToDictionary(var);
            //���ӿ�����Ʈ ���� ó��
            if(variableManager is null)
            {
                Dictionary<string, float> dict2 = variableManager.GetComponent<VariableManager>().GlobalVariable;
                foreach (var pair in dict2)
                {
                    if (!data_dictionary.ContainsKey(pair.Key))
                    {
                        data_dictionary.Add(pair.Key, pair.Value);
                    }
                }
            }
            return data_dictionary;
        }
    }



    //��ųʸ� �и� �Լ�
    public static Dictionary<string, object> ConvertStringToDictionary(string input)
    {
        //���� �Է� : "{str:5,luck:76}"  =>  {{"str",5},{"luck",76}}
        //�� ��ųʸ� �и� �Լ�


        Dictionary<string, object> result = new Dictionary<string, object>();

        // ����ǥ������ ����Ͽ� Ű-�� �� ����
        MatchCollection matches = Regex.Matches(input, @"(\w+):([^,}]+)");

        foreach (Match match in matches)
        {
            // ����� Ű�� ���� ������
            string key = match.Groups[1].Value.Trim();
            string valueString = match.Groups[2].Value.Trim();

            // ���� ������ �������� ��ȯ�Ͽ� Dictionary�� �߰�
            object value = ConvertStringToType(valueString);
            result[key] = value;
        }

        return result;
    }

    private static object ConvertStringToType(string valueString)
    {
        // ���� ������ ���Ŀ� ���� ��ȯ ���� �߰�
        // ���⼭�� int, float, string�� ���� ������ ����
        if (int.TryParse(valueString, out int intValue))
        {
            return intValue;
        }
        else if (float.TryParse(valueString, out float floatValue))
        {
            return floatValue;
        }
        else if (double.TryParse(valueString, out double doubleValue))
        {
            return doubleValue;
        }
        else
        {
            return valueString; // �⺻�����δ� ���ڿ��� ó��
        }
    }
    public static string CalculateAndReplace(string template, Dictionary<string, object> variables)
    {
        string[] text = template.Split(" ");
        List<string> list = new List<string>();
        foreach(var val in text)
        {
            string str = val;
            foreach (var variable in variables)
            {
                // ������ �ش��ϴ� ���ø� �κ��� ã�Ƴ���
                string variableTemplate = $"{{{variable.Key}}}";
                str = str.Replace(variableTemplate, variable.Value.ToString());
                // ���� ���� ���ø��� ����
            }
            // ������ ǥ������ ���
            var dataTable = new System.Data.DataTable();
            try
            {
                //float ���� ���
                //var result = dataTable.Compute(str, "");
                //list.Add(result.ToString());
                //int ���� ���
                var result = dataTable.Compute(str, "");
                if (result != DBNull.Value)
                {
                    float floatValue = Convert.ToSingle(result);
                    int roundedValue = Mathf.RoundToInt(floatValue);
                    list.Add(roundedValue.ToString());
                }
            }
            catch
            {
                list.Add(val);
            }
        }

        string text_result = "";
        foreach (var str in list)
        {
            text_result += " " + str;
        }
        return text_result;
    }


    //���� �� �ֱ�


    public Additional_Description[] additional_Description;//�߰� ����(������ ��)
}
[Serializable]
public class AdditionalData
{
    //ī���� �±� �ܾ��� �߰� ���� ���� ������


    public string name;
    public string description;
}
[Serializable]
public class Additional_Description
{


    string name;
    string description;
}
