using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effect : MonoBehaviour
{
    Game_Manager game_Manager;
    public Image image;
    public Sprite icon;
    private void Start()
    {
        game_Manager = Game_Manager.instance;
    }
    private void Update()
    {
        image.sprite = icon;
    }
}
