using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class MyAttributes : MonoBehaviour
{
    //���̾�
    //1.���� (��ʰ��ɼ�ġ, ��ʺҰ��ɼ�ġ)�� ���� ��� ���� �̷��� ������ ����
    //2.��� ���� ��ġ�� ���� ������
    //3.�� ���� ��� ���ɰ� �Ұ������� ������ �����ְ� ���� ������ ������
    //4.��� ������ ���� ��
    //5.���� ���� get ������� �����ؼ� �ǽð����� ������
    //��Ʈ����Ʈ ���� ����
    //����
    //1.�������� ��Ʈ����Ʈ�� ������ ��� ����
    //2.�÷��̾��� ������ ��������
    [SerializeField] public Dictionary<string, float> globalVariable { get; }
    [SerializeField] protected List<float> positiveEffects;  //��
    [SerializeField] protected List<float> negativeEffects;  //��
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
                    multiplier *= 0.5f; // ����: �� ������ ���� ���Ȱ� �� �������� ���� ��� ���ذ� �������� ����
                }
            }
        }
        return multiplier;
    }

    protected float CustomFunction(float incomingDamage)
    {
        // ������ �Դ� ���� ���
        float damageTaken = incomingDamage - defenseBonus + defensePenalty;
        if (damageTaken < 0)
        {
            damageTaken = 0; // ���� ���� ���� �ʵ��� ����
        }

        // ���ظ� ���� �� ü�� ����
        float health = 100; // ���÷� ü���� 100���� ����
        health -= damageTaken * effectMultplier;
        return damageTaken;
    }
    public Stat stat;
    public Stat stat1;
    public Stat stat2;
    private void Start()
    {
        //��� ���� ����
        //�߰� �κ��� ���ø� ���� �۵��ϴ� �ǹ� �� ������{���ø�,������,������,��ʰ�} �̷��� �ְ� ���ø��� ǥ����
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
    //����
    //1.
    //2.
    public Dictionary<string, Stat> Stats { get; set; }

    public CharacterStatus(Dictionary<string, Stat> stats)
    {
        Stats = new Dictionary<string, Stat>(stats);
        Calculate();
        FixedCalculate();
    }


    //��������
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
                Debug.Log(stat.Key + "��� �� ��� : " + finalStat);
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
                Debug.Log(stat.Key + "���� �� ��� : " + finalStat);
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

