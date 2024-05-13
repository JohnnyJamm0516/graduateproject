using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public GameObject drawText;//�ý�Ʈ ������ ���� ����ϴ� ������Ʈ
    public GameObject pile;//���� ī�� ���̸� ���� ����
    TextMeshProUGUI text_GUI;
    public void On_Change()
    {
        if (text_GUI == null)
        {
            text_GUI = drawText.GetComponent<TextMeshProUGUI>();
        }
        else if(pile != null)
        {
            text_GUI.text = pile.transform.childCount.ToString();
        }
        
    }
    private void Update()
    {
        On_Change();
    }
}
