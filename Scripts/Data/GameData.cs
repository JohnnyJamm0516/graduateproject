using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Mana_Data_UI
{
    //������ ���õ� ������


    //#�ʱ� ����
    //�ִ� ����


    //#�ļ��� ����
    //#�ϴ��� �������
    //����


    public int mana_count;
    public int max_mana;
}
[Serializable]
public class Draw_Data_UI
{
    //���� ī�� ������ ������


    //#�ʱ� ����
    //��� ī�� ������ �迭


    //#�ļ��� ����
    //#�� ���� �������
    //���� ī�� ���� �迭 ����
}
[Serializable]
public class Discard_Data_UI
{
    //������ ī�� ������ ������


    //#�ʱ� ����
    //���� ������ �迭 �ʱ� ���̴� 0


    //#�ļ��� ����
    //#�� ���� �������
}
[Serializable]
public class Card_Data_UI
{
    //ī�� ��ü�� ��ȭ�� ������ ������


    //#�ʱ� ����
    //ī�忡 ��ȭ�� �̷���� ��Ȳ�� ����


    //#�ļ��� ����
    //#�� ���� �������
    //����)��ȭ, Ư�� ������, ���� �� ���� ��ȭ, ��� Ƚ�� ��ȭ��


}
[Serializable]
public class Exception_Card_Data_UI
{
    //�Ҹ�Ǵ� ��Ȳ�� ���� ���� ������ ī�忡 ���� ������
    

    //#�ʱ� ����
    //�Ҹ�� ī�����


    //#�ļ��� ����
    //������ ī�� ���� �迭
    //Ư�� ī�� ���� �迭
}
[Serializable]
public class EndTurn_Data_UI
{
    //���� �ൿ�� ���� ������
    //����)����
    

    
    //#�ʱⱸ��
    //���� ���� ����


    //#�ļ��� ����
    //#�� ���� �������
}
[Serializable]
public class Player_Data_UI
{
    //�÷��̾��� ���¿� ���� ������


    //#�ʱⱸ��
    //HP
    public string name;
    public float HP;
    public float maxHP;
    public float shield;
    public bool isPlayer;

    public string logo;


    //#�ļ��� ����
    //��
    //�̷ο� ȿ��
    //�طο� ȿ��
    //���ϴ� �Ӽ� ���� ��(������ ���� ���)
    //�޴� �Ӽ� ���� ��(������ ���� ���) 
    //#�� ���� �������
}
[Serializable]
public class Enemy_Data_UI : Player_Data_UI
{
    //���� ���¿� ���� ������


    //#�ʱⱸ��
    //HP
    //�ൿ
    [SerializeField] string mob_Attribute;
    [SerializeField] public string[] mob_Act;


    //#�ļ��� ����
    //���ϴ� �Ӽ�
    //��� �Ӽ�


    //#�� ���� �������
}
[Serializable]
public class Equipment_Data_UI 
{
    //�÷��̾��� ��� ���� ������
    //���ѵ� ����(���׶�����)�� �����ϴ� ���� ����� �θ�
    //��ȭ�� ����� ������


    //#�ʱ� ����
    //���� 


    //#�ļ��� ����
    //����(�ɷ�ġ+����ȿ��)
    //��(�ɷ�ġ,����ȿ��)
    //��ű�(�ɷ�ġ,����ȿ��)
    //�ó��� ȿ�� ����


    public string name;
    public string description;
}
[Serializable]
public class Item_Data_UI
{
    //�÷��̾��� �����ۿ� ���� ������
    //���� �Ǿ��ִ� ���� �������̶�� �θ�
    //��ȭ�� ������ �ʴ� ������


    //#�ʱ� ����
    //�¿��� ���


    //#�ļ��� ����
    //���� ������ ȹ�� ���� ���� ���� �� ����
    //�ó��� ȿ�� ����


    public string name;
    public string description;
}
[Serializable]
public class Stage_Data_UI
{
    //���� �������� ������ �ʿ� ���� ������

    

    //#�ʱ� ����
    //���ӻ����� ������ �� �迭 ����
    //�� �̵� �迭 ����
    //��ī���� ������


    //#�ļ��� ����
}
[Serializable]
public class Collect_Data_UI
{
    //�رݿ�ҿ� ���� ������
    //#�ļ�������


    //#�ʱ� ����
    //�̺�Ʈ ���� ���
    //������ �� ��� ���� ���
    //���� ���� ���
    //�������� ���� ���



}




[Serializable]
public class GameData
{
    //UI �������� ����
    //#1��1 ��Ī
    public Mana_Data_UI mana_Data;
    public Draw_Data_UI draw_Data;
    public Discard_Data_UI discard_Data;
    public Card_Data_UI card_Data;
    public Exception_Card_Data_UI exception_Card_Data;
    public EndTurn_Data_UI endTurn_Data;


    //���� �ʵ��� ����
    public Equipment_Data_UI[] enable_equip;
    public Item_Data_UI[] enable_item;
    public Stage_Data_UI stage_Data;
    public Collect_Data_UI collect_Data;
    public int power;
}
