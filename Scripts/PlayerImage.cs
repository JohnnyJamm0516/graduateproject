using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerImage : MonoBehaviour
{
    //플레이어(적 포함)에게 드롭 이벤트가 발생할 시 체크하는 부분
    //즉 카드 사용하는 부분이라고 보면 됨
    //구현 역할
    //1.히트 판정시 발동하는 부분
   

    public Player player;       //데이터 베이스
    public bool isPlayer(GameObject cardObject)
    {
        //카드 드롭 이벤트 발생시 작동할 기능 나열할 것
        //cardObjet에서 가져오는 형태로 적용 될 듯


        if (player.player_Data.isPlayer)
        {
            Debug.Log("플레이어 드롭 이벤트 발생");
        }
        else
        {
            Debug.Log("적 드롭 이벤트 발생");
        }
        Card useCard = cardObject.GetComponent<Card>();

        //AttackModule.On_TrueDamage(player, useCard);
        //player.On_Hit(useCard.data.effect);


        return player.player_Data.isPlayer;
    }
}
