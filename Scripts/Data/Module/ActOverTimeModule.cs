using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ActOverTimeModule
{
    //��ü ����
    //1.�����̻� ��ü
    //2.�ð� Ʈ���ſ� ������ ����
        //�������� ���ø�ŭ ��� ���ظ� ������ �Ͱ� ���� ���� ��ҵ��� �ֱ� ������ ���� ������ �ʿ䰡 ����

    //����
    //1.�� ����, ����, �Ǵ� �� ���� �� ȣ��Ǵ� ��(ȣ��Ǵ� �κ�)���� ����Ͽ� �ݿ�������
        //�ݿ��� �÷��̾�� ��������� ���ذ� �����Ѵ� �˷��ִ� �κ�
    //2.Ƚ��, ���ط�, ���ӽð��� �������� �����͸� �޾Ƽ� �������


    //�������� �ൿ�� �ְ��ϴ� ���
    //�� ���� ����(Ƚ�� ���)
    //�� ���� ����(Ƚ�� ���)
    protected List<SkillModule> skils = new();
    protected int trunStartCount;       //�� ���� ����Ƚ�� 2�� �ߵ��Ѵ� 1
    protected int trunEndCount;         //..
    protected int life;                 //��밡���� Ƚ��
    protected int useLife;              //����ϴ� Ƚ��   
}
