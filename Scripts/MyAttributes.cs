using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class MyAttributes : MonoBehaviour
{
    //레이어
    //1.값은 (비례가능수치, 비례불가능수치)로 존재 모든 값을 이렇게 나누어 놓음
    //2.비례 가능 수치에 값이 곱해짐
    //3.그 값을 비례 가능과 불가능으로 나누어 더해주고 다음 연산을 수행함
    //4.모든 연산이 끝난 끝
    //5.연산 값을 get 방식으로 전달해서 실시간으로 보여줌
    //어트리뷰트 구현 예제
    //역할
    //1.실질적인 어트리뷰트의 내용을 담고 있음
    //2.플레이어의 스탯을 만져야함
    [SerializeField] public Dictionary<string, float> globalVariable { get; }
    [SerializeField] protected List<float> positiveEffects;  //값
    [SerializeField] protected List<float> negativeEffects;  //값
    [SerializeField] protected List<float> effectMultipliers; //

    public float defenseBonus { get { return Calculate_DefenseBonus(); } }
    public float defensePenalty { get { return Calculate_DefensePenalty(); } }
    public float effectMultplier { get { return Calculate_EffectMultiplier(); } }
    private float Calculate_DefenseBonus()
    {
        float bonus = 0;
        foreach (float positiveEffect in positiveEffects)
        {
            bonus += positiveEffect;
        }
        return bonus;
    }

    private float Calculate_DefensePenalty()
    {
        float penalty = 0;
        foreach (float negativeEffect in negativeEffects)
        {
            penalty += negativeEffect;
        }
        return penalty;
    }

    private float Calculate_EffectMultiplier()
    {
        float multiplier = 1;
        foreach (float resistance in effectMultipliers)
        {
            foreach (float negativeEffect in negativeEffects)
            {
                if (negativeEffect == resistance)
                {
                    multiplier *= 0.5f; // 예시: 독 데미지 저항 스탯과 독 데미지가 같은 경우 피해가 절반으로 감소
                }
            }
        }
        return multiplier;
    }

    protected float CustomFunction(float incomingDamage)
    {
        // 실제로 입는 피해 계산
        float damageTaken = incomingDamage - defenseBonus + defensePenalty;
        if (damageTaken < 0)
        {
            damageTaken = 0; // 음수 값이 되지 않도록 조정
        }

        // 피해를 입은 후 체력 감소
        float health = 100; // 예시로 체력을 100으로 가정
        health -= damageTaken * effectMultplier;
        return damageTaken;
    }
    public Stat stat;
    public Stat stat1;
    public Stat stat2;
    private void Start()
    {
        //사용 예제 설명
        //추가 부분은 탬플릿 에서 작동하는 의미 즉 변수명{탬플릿,고정값,최종값,비례값} 이렇게 있고 탬플릿은 표현식
        stat = new Stat { CalculationTemplate = "str", FixedValue = 10, ProportionalValue = 10 };
        stat1 = new Stat { CalculationTemplate = "dex", FixedValue = 10, ProportionalValue = 10 };
        stat2 = new Stat { ProportionalValue = 100, FixedValue = 10, CalculationTemplate = "luck*str" };
        Dictionary<string, Stat> stats = new ();
        stats.Add("str", stat);
        stats.Add("dex", stat1);
        stats.Add("luck", stat2);
        new CharacterStatus(stats);
    }
}
[Serializable]
public struct Stat
{
    public float ProportionalValue;
    public float FixedValue;
    public string CalculationTemplate;
    public float FinalStat;

    public Stat(float proportionalValue, float fixedValue, string calculationTemplate)
    {
        ProportionalValue = proportionalValue;
        FixedValue = fixedValue;
        CalculationTemplate = calculationTemplate;
        FinalStat = fixedValue * (1 + proportionalValue / 100);
    }
}
[Serializable]
public class CharacterStatus
{
    //역할
    //1.
    //2.
    public Dictionary<string, Stat> Stats { get; set; }

    public CharacterStatus(Dictionary<string, Stat> stats)
    {
        Stats = new Dictionary<string, Stat>(stats);
        Calculate();
        FixedCalculate();
    }


    //변형하자
    public void Calculate()
    {
        try
        {

            foreach (var stat in Stats)
            {
                string key = stat.Key;
                string expression = stat.Value.CalculationTemplate;
                foreach (var value in Stats)
                {
                    var propotionValue = value.Value.FixedValue * (1 + value.Value.ProportionalValue / 100);
                    expression = expression.Replace(value.Key, propotionValue.ToString());
                }
                float finalStat = Stats[key].FinalStat;
                finalStat = EvaluateExpression(expression);
                Debug.Log(stat.Key + "비례 값 계산 : " + finalStat);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Calculation error : " + e.Message);
        }
    }
    public void FixedCalculate()
    {
        try
        {
            foreach (var stat in Stats)
            {
                string key = stat.Key;
                string expression = stat.Value.CalculationTemplate;
                foreach (var value in Stats)
                {
                    expression = expression.Replace(value.Key, value.Value.FixedValue.ToString());
                }
                float finalStat = Stats[key].FinalStat;
                finalStat = EvaluateExpression(expression);
                Debug.Log(stat.Key + "고정 값 계산 : " + finalStat);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Calculation error : " + e.Message);
        }
    }
    private float EvaluateExpression(string expression)
    {
        DataTable dt = new();
        object result = dt.Compute(expression, "");
        return Convert.ToSingle(result);
    }
}

