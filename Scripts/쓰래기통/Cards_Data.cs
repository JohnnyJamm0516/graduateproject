using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[Serializable]
public class Cards_Data : MonoBehaviour
{
    [SerializeField]
    public Data<Card_UI> cards;
    public Card_UI On_Find_With_Name(string card_name)
    {
        Card_UI result;
        foreach (var card in cards)
        {
            if(card.card_name.Equals(card_name))
            {
                result = card;
                return result;
            }
        }
        return null;
    }
    public List<string> On_Get_Card_Names()
    {
        List<string> result = new List<string>();
        foreach(var card in cards)
        {
            result.Add(card.card_name);
        }
        return result;
    }
}
