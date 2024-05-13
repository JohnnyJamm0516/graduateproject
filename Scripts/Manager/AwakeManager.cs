using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeManager : MonoBehaviour
{
    [SerializeField]
    Game_Manager game_Manager;
    private void Awake()
    {
        game_Manager = GameObject.FindAnyObjectByType<Game_Manager>();
    }
}
