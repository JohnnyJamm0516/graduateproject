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
        //매니저에 메시지를 보내는 형식
        manager.SendMessage(fucStr);
    }
}
