using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class ActOverTimeModule
{
    //객체 설명
    //1.상태이상 객체
    //2.시간 트리거에 영향을 받음
        //지속피해 스택만큼 즉시 피해를 입히는 것과 같은 여러 요소들이 있기 때문에 값을 저장할 필요가 있음

    //역할
    //1.턴 시작, 종료, 또는 턴 진행 중 호출되는 값(호출되는 부분)들을 계산하여 반영시켜줌
        //반영은 플레이어에게 어느정도의 피해가 동작한다 알려주는 부분
    //2.횟수, 피해량, 지속시간의 종류등의 데이터를 받아서 계산해줌


    //지속적인 행동을 주관하는 모듈
    //턴 시작 시행(횟수 비례)
    //턴 종료 시행(횟수 비례)
    protected List<SkillModule> skils = new();
    protected int trunStartCount;       //턴 시작 시행횟수 2번 발동한다 1
    protected int trunEndCount;         //..
    protected int life;                 //사용가능한 횟수
    protected int useLife;              //사용하는 횟수   
}
