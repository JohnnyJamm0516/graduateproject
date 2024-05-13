using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Mana_Data_UI
{
    //마나와 관련된 데이터


    //#초기 구현
    //최대 마나


    //#후순위 구현
    //#턴단위 고려사항
    //마나


    public int mana_count;
    public int max_mana;
}
[Serializable]
public class Draw_Data_UI
{
    //뽑을 카드 더미의 데이터


    //#초기 구현
    //모든 카드 데이터 배열


    //#후순위 구현
    //#턴 단위 고려사항
    //뽑을 카드 더미 배열 저장
}
[Serializable]
public class Discard_Data_UI
{
    //버려진 카드 더미의 데이터


    //#초기 구현
    //묘지 데이터 배열 초기 값이니 0


    //#후순위 구현
    //#턴 단위 고려사항
}
[Serializable]
public class Card_Data_UI
{
    //카드 자체의 변화된 사항의 데이터


    //#초기 구현
    //카드에 변화가 이루어진 상황을 저장


    //#후순위 구현
    //#턴 단위 고려사항
    //예시)강화, 특정 아이템, 같은 것 개수 강화, 사용 횟수 강화등


}
[Serializable]
public class Exception_Card_Data_UI
{
    //소멸되는 상황과 같은 예외 상태의 카드에 대한 데이터
    

    //#초기 구현
    //소멸된 카드더미


    //#후순위 구현
    //무작위 카드 생성 배열
    //특정 카드 생성 배열
}
[Serializable]
public class EndTurn_Data_UI
{
    //턴의 행동에 대한 데이터
    //예시)펜촉
    

    
    //#초기구현
    //저장 내용 없음


    //#후순위 구현
    //#턴 단위 고려사항
}
[Serializable]
public class Player_Data_UI
{
    //플레이어의 상태에 대한 데이터


    //#초기구현
    //HP
    public string name;
    public float HP;
    public float maxHP;
    public float shield;
    public bool isPlayer;

    public string logo;


    //#후순위 구현
    //방어도
    //이로운 효과
    //해로운 효과
    //가하는 속성 보정 값(아이템 보정 요소)
    //받는 속성 보정 값(아이템 보정 요소) 
    //#턴 단위 고려사항
}
[Serializable]
public class Enemy_Data_UI : Player_Data_UI
{
    //적의 상태에 대한 데이터


    //#초기구현
    //HP
    //행동
    [SerializeField] string mob_Attribute;
    [SerializeField] public string[] mob_Act;


    //#후순위 구현
    //가하는 속성
    //방어 속성


    //#턴 단위 고려사항
}
[Serializable]
public class Equipment_Data_UI 
{
    //플레이어의 장비에 대한 데이터
    //제한된 슬롯(버그때문에)에 장착하는 것을 장비라고 부름
    //변화가 생기는 데이터


    //#초기 구현
    //무기 


    //#후순위 구현
    //정수(능력치+고유효과)
    //방어구(능력치,고유효과)
    //장신구(능력치,고유효과)
    //시너지 효과 구현


    public string name;
    public string description;
}
[Serializable]
public class Item_Data_UI
{
    //플레이어의 아이템에 대한 데이터
    //제한 되어있는 것을 아이템이라고 부름
    //변화가 생기지 않는 데이터


    //#초기 구현
    //온오프 방식


    //#후순위 구현
    //동일 아이템 획득 가능 구조 설계 및 구현
    //시너지 효과 구현


    public string name;
    public string description;
}
[Serializable]
public class Stage_Data_UI
{
    //현재 진행중인 게임의 맵에 대한 데이터

    

    //#초기 구현
    //게임생성시 생성된 맵 배열 저장
    //맵 이동 배열 저장
    //인카운터 게이지


    //#후순위 구현
}
[Serializable]
public class Collect_Data_UI
{
    //해금요소에 대한 데이터
    //#후순위구현


    //#초기 구현
    //이벤트 수집 기록
    //아이템 및 장비 수집 기록
    //엔딩 수집 기록
    //도전과제 수집 기록



}




[Serializable]
public class GameData
{
    //UI 데이터의 원본
    //#1대1 매칭
    public Mana_Data_UI mana_Data;
    public Draw_Data_UI draw_Data;
    public Discard_Data_UI discard_Data;
    public Card_Data_UI card_Data;
    public Exception_Card_Data_UI exception_Card_Data;
    public EndTurn_Data_UI endTurn_Data;


    //은닉 필드의 원본
    public Equipment_Data_UI[] enable_equip;
    public Item_Data_UI[] enable_item;
    public Stage_Data_UI stage_Data;
    public Collect_Data_UI collect_Data;
    public int power;
}
