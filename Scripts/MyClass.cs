using System.Collections.Generic;
using UnityEngine;

public class MyClass : MyAttributes
{
    //어트리뷰트 사용 예시
    //현재 구현
    //1.긍정적 효과리스트의 내용이 모두 더해지는 기능임 힘 5 민첩 5 이러면 10이 더해지는 구조
    //2.부정적 효과리스트의 내용이 모두 더해지는 기능임 힘 5 민첩 5 이러면 10이 더해지는 구조
    //3.저항 속성은 엄밀히 따지면 긍정적 효과이지만 
    /*
    void Start()
    {
        positiveEffects = new List<float>();
        negativeEffects = new List<float>();
        effectMultipliers = new List<float>();

        // 예시로 값을 넣어줍니다.
        positiveEffects.Add(10); // 방어력 추가
        positiveEffects.Add(5);  // 회복력 추가
        negativeEffects.Add(20); // 독 데미지 추가
        negativeEffects.Add(15); // 빙결 추가

        // 저항 스탯 값 추가
        effectMultipliers.Add(5); // 독 데미지 저항 추가

        float damageTaken = CustomFunction(50);
        Debug.Log("Damage taken: " + damageTaken);
    }*/
}
