using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public class Mana : MonoBehaviour
{
    public GameObject manaText;
    TextMeshProUGUI text_GUI;
    Game_Manager game_Manager;
    private void Start()
    {
        game_Manager = Game_Manager.instance;
    }
    public void On_Change()
    {
        if (text_GUI == null)
        {
            text_GUI = manaText.GetComponent <TextMeshProUGUI>();
        }
        text_GUI.text = game_Manager.mana_count.ToString() + "/" + game_Manager.max_mana;
    }
    private void Update()
    {
        On_Change();
    }
}
