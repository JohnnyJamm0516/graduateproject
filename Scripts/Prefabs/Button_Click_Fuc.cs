using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Click_Fuc : MonoBehaviour
{
    GameObject manager;
    public string fucStr;

    private void Start()
    {
        Button btnCom = gameObject.GetComponent<Button>();
    }

    public void On_Click_Button()
    {
        //�Ŵ����� �޽����� ������ ����
        manager.SendMessage(fucStr);
    }
}
