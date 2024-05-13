using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DrawGridModule : MonoBehaviour
{
    //뽑을 카드 더미 관리
    //역할
    //1.On_Add_Card
    //  설명 : 뽑을 카드 더미에 카드를 더하고자 함
    //  1)원형은 (데이터(참조형), 프리팹(복제 대상), 그리드 인덱싱 대상, 소유자)
    //  2)오버로딩을 통하여 (데이터)만 넣어도 작동가능 하도록 변경
    //  3)추후 다른 카드더미에 추가할 때 원형을 사용해서 사용가능 하도록함이 목표
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card, GameObject parent, Game_Manager game_Manager)
    {
        //카드 만들어주는 시퀀스
        //역할
        //1.카드
        //  1)프리팹 기반 카드 인스턴스 생성
        //  2)부모 설정
        //  3)활성화
        //  4)데이터 연동
        //  5)게임매니저의 리스트에 목록 추가 
        //2.카드 만든 개수 출력
        //  1)디버그 로그 찍기 (프리팹 내용+개수)출력 
        


        Debug.Log($"{card.ToString()}가 {cards_Data.data.Count}개 생성되었습니다");
        if(cards_Data != null)
        {
            for (int i = 0; i < cards_Data.data.Count; i++)
            {
                GameObject obj = Instantiate(card);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(true);
                Card objCard = obj.transform.GetComponent<Card>();
                objCard.data = cards_Data.data[i];
                objCard.data.variableManager = Game_Manager.instance.childObjects[13];
                game_Manager.draw_List.Add(obj);
            }
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card, GameObject parent)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        if(game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        GameObject parent = game_Manager.object_DrawGrid;
        if (game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        GameObject parent = game_Manager.object_DrawGrid;
        GameObject card = game_Manager.card_Prefab;
        if (game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
}
