using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using JetBrains.Annotations;

[Serializable]
public class CardData
{
    //역할
    //행동의 리스트를 가져오는 부분
    //카드와 관련된 데이터를 포괄하는 데이터

    //구현 필요
    //1.함수(행동)의 이름 가져오기
    //2.함수 대리자(delegate) 가져오기
    //3.이름은 에디터에서 편집가능 하도록 가져오는 것 저번에 만든 것 응용하면 됨
    //4.대리자는 실제 카드의 대리자(delegate) 변수(또는 Action)를 통하여 실행하도록 만들 것
    

    //밑에 부분을 어택 모듈의 리스트로 가져올 예정

    //밑에 부분을 방어 모듈의 리스트로 가져올 예정
    public DeffenceModule deffenceDatas;//방어
}

[Serializable]
public class Card_UI
{
    //카드의 몸체를 담당하는 원본 틀

    public string card_name;            //이름
    public string cost;                 //소모 마나
    public string text;                 //요약 설명
    public string var;
    public string attribute;            //상성속성 #후순위
    public string rarity;
    public string type;                 //공격, 방어 등 행동 특성
    public string effect_Template;      //효과 변수
    public string cumulativeChainEffect;//체인 강화 변수
    public GameObject variableManager;
    public float effect
    {
        //카드의 효과의 변수를 읽어오는 부분
        get
        {
            Dictionary<string, object> data_Dictionary = ConvertStringToDictionary(effect_Template);
            string str = CalculateAndReplace(effect_Template, Data_Dictionary);
            return Convert.ToSingle(str);
        }
    }                //효과 변수 가져오기
    public string image;
    public string rarity_Image;
    public string type_Image;
    public int cardCount;



    //내부 딕셔너리 분리 멤버. 즉 실제 카드의 값이 되는 데이터. 실 적용 값이면서 내부 변수 값을 우선시 해서 사용하도록 바꿀 것.
    public Dictionary<string, object> Data_Dictionary
    {
        get
        {
            Dictionary<string, object> data_dictionary = ConvertStringToDictionary(var);
            //게임오브젝트 예외 처리
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



    //딕셔너리 분리 함수
    public static Dictionary<string, object> ConvertStringToDictionary(string input)
    {
        //예시 입력 : "{str:5,luck:76}"  =>  {{"str",5},{"luck",76}}
        //즉 딕셔너리 분리 함수


        Dictionary<string, object> result = new Dictionary<string, object>();

        // 정규표현식을 사용하여 키-값 쌍 추출
        MatchCollection matches = Regex.Matches(input, @"(\w+):([^,}]+)");

        foreach (Match match in matches)
        {
            // 추출된 키와 값을 가져옴
            string key = match.Groups[1].Value.Trim();
            string valueString = match.Groups[2].Value.Trim();

            // 값을 적절한 형식으로 변환하여 Dictionary에 추가
            object value = ConvertStringToType(valueString);
            result[key] = value;
        }

        return result;
    }

    private static object ConvertStringToType(string valueString)
    {
        // 여러 데이터 형식에 대한 변환 로직 추가
        // 여기서는 int, float, string에 대한 예제만 포함
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
            return valueString; // 기본적으로는 문자열로 처리
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
                // 변수에 해당하는 템플릿 부분을 찾아내기
                string variableTemplate = $"{{{variable.Key}}}";
                str = str.Replace(variableTemplate, variable.Value.ToString());
                // 변수 값을 템플릿에 대입
            }
            // 수학적 표현식을 계산
            var dataTable = new System.Data.DataTable();
            try
            {
                //float 값일 경우
                //var result = dataTable.Compute(str, "");
                //list.Add(result.ToString());
                //int 값일 경우
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


    //참조 값 넣기


    public Additional_Description[] additional_Description;//추가 설명(고정된 값)
}
[Serializable]
public class AdditionalData
{
    //카드의 태그 단어의 추가 설명에 대한 데이터


    public string name;
    public string description;
}
[Serializable]
public class Additional_Description
{


    string name;
    string description;
}
