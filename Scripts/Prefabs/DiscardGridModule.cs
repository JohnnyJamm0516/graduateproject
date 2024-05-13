using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardGridModule
{
    public static void Mixing_Cards(GameObject discardGrid)
    {
        int val = discardGrid.transform.childCount;
        //카드 섞기 부분
        for (int i = 0; i < val; i++)
        {
            int random = (Random.Range(0, discardGrid.transform.childCount)-1);
        }
    }
}
