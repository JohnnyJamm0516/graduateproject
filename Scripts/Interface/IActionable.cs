using System;
using System.Collections.Generic;
using UnityEngine;


//인터패이스 예제 인터페이스
public interface IActionable
{
    //액션 인터페이스 추후 너무 함수가 많아질 경우 분산의 대책으로 사용함
    //자동 추가는 리플렉션을 이용하여 할 수 있지만 성능에 너무 큰 오버헤드가 발생할 것 같아 배제

    abstract Dictionary<string, Action<string>> GetActions();
}
