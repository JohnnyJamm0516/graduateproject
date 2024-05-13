using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataCreator : MonoBehaviour
{

    public void On_Save_Card_Data()
    {
        //프리팹 자체 직렬화 하여 자식 개체까지 저장
        //중요한 점은 자식개체가 진짜 카드 데이터
    }
    public void On_Load_Card_Data()
    {
        //런타임 중에 카드에 변화가 생길경우 호출되는 메서드
        //역시 자식개체의 변화의 적용 여부를 결정함
    }
}
