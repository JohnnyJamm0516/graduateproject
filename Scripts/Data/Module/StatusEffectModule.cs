using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatusEffectModule
{
    //객체 설명
    //1.상태이상 객체
    //2.시간에 제한 받지 않음
    //횟수나 행동같은 트리거에 영향 받음

    //역할
    //1.상태이상 객체의 원본 정보를 담고 있음
    //2.레이어 개념이 필요할 것으로 보임 일단 넣어놈

    public statusEffectsLayer Layer;
}

