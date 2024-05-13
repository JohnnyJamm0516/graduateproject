using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsGridModule
{
    public static void On_Init_Card(Cards_SO cards_SO, GameObject card_Prefab, GameObject cardsGrid, List<GameObject> cards_Object)
    {
        //카드 도감 보여주는 곳(모든 카드 임)
        Data<Card_UI> data_SO = cards_SO.cards;
        foreach(var card in data_SO)
        {
            GameObject obj = Object.Instantiate(card_Prefab, cardsGrid.transform);
            obj.GetComponent<Card>().data = card;
            cards_Object.Add(obj);
        }
    }
}
