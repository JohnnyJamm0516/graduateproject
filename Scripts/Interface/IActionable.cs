using System;
using System.Collections.Generic;
using UnityEngine;


//�������̽� ���� �������̽�
public interface IActionable
{
    //�׼� �������̽� ���� �ʹ� �Լ��� ������ ��� �л��� ��å���� �����
    //�ڵ� �߰��� ���÷����� �̿��Ͽ� �� �� ������ ���ɿ� �ʹ� ū ������尡 �߻��� �� ���� ����

    abstract Dictionary<string, Action<string>> GetActions();
}
