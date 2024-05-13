using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pile : MonoBehaviour
{
    public GameObject drawText;//택스트 변경을 위해 사용하는 오브젝트
    public GameObject pile;//실제 카드 더미를 넣을 공간
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
