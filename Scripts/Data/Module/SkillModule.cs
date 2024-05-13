using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SkillModule
{
    //객체 설명
    //실제 행동은 모두 스킬판정을 지닐 것임
    //즉 일반 공격도 1스킬 이런 식으로 적용됨
    //5 데미지를 주고 5 방어도를 얻는다 = 하나의 스킬 모듈
    //6 데미지를 주고 1 피해를 입고 1방어도를 잃는다 = 하나의 스킬모듈



    //역할
    //공격의 리스트르 받아서 일괄 적용하도록 하는 방식으로 반영될 것임
    //1.여러 행동들의 조합을 정의할 수 있도록 조합


    List<ActObject> actionDatas;
}
public class ActObject
{
    //대상 List   (적용 범위, 플레이어인가, 전체인가, 단일인가, 무작위인가)
    List<TargetData> target;
    private class TargetData
    {

    }
    //액션 List   (액션, 액션 변수)
    List<ActionData> actionData;
    private class ActionData
    {

    }
    //플레이어 List (미정)
}





