using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardGridModule
{
    public static void Mixing_Cards(GameObject discardGrid)
    {
        int val = discardGrid.transform.childCount;
        //ī�� ���� �κ�
        for (int i = 0; i < val; i++)
        {
            int random = (Random.Range(0, discardGrid.transform.childCount)-1);
        }
    }
}
