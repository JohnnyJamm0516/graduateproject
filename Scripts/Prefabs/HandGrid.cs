using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandGrid : MonoBehaviour
{
    //객체 설명
    //카드 뽑아서 관리하는 스크립트
    //손패를 관리한다고 생각하면 편함
    //시각화도 일단 여기에 들어감

    //역할
    //1.카드 뽑기     카드 뽑기 이벤트 함수로 구현
    //  1)딜레이까지 구현했음
    //2.카드 버리기      카드 버리기 이벤트 함수로 구현
    //  1)아직 구현할 필요 없음
    //3.카드 사용       (카드 사용됨 호출)
    //  1)use 알고리즘은 손패에서 처리
    //  2)카드는 데이터만 가지고 있음
    //4.손패 정렬

    //API
    //1.DrawCard()
    //  카드 뽑기
    //2.On_CircularLayout()
    //  카드 정렬 명령
    //3.



    public static float radius = 2000f;
    public static GameObject cardPrefab;
    public static float startAngle = 105f;  // 시작 각도
    public static float endAngle = 75f;   // 종료 각도
    public static float card_area = 200f;
    public static float duration = 0.5f;

    public GameObject draggedCard;
    private Vector3 initialCardPosition;
    private Quaternion initialCardRotation;
    public static Texture2D yourCursorTexture;
    public static Texture2D originCursorTexture;

    public List<GameObject> card_List;

    public static GameObject instance;

    public GameObject DamageInstance;



    public void DrawCard(int counts)
    {
        StartCoroutine(drawToDelayCard(Game_Manager.instance, this.gameObject, counts, 1f));
    }
    public static void On_CircularLayout(List<GameObject> card_Objects, float duration)
    {
        int cardCount = card_Objects.Count;
        if (cardCount > 10) return; //10장 제한
        float c_startAngle = startAngle - (10 - cardCount) * 1.5f;
        float c_endAngle = endAngle + (10 - cardCount) * 1.5f;

        Debug.Log("카드 정렬 개수 : "+cardCount);
        for (int i = 0; i < cardCount; i++)
        {
            float angle = Mathf.Lerp(90, 90, 0);
            switch (cardCount)
            {
                case 0:
                    return;
                case 1:
                    angle = Mathf.Lerp(90, 90, 0);
                    break;
                default:
                    angle = Mathf.Lerp(c_startAngle, c_endAngle, i / (float)(cardCount - 1 == 0 ? 1 : cardCount - 1));
                    break;
            }
            float radians = angle * Mathf.Deg2Rad;

            float x = Mathf.Cos(radians) * radius;
            float y = Mathf.Sin(radians) * radius;

            // 원의 중심을 바라보는 각도
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(x, y, 0f));
            float count_lerf = (10 - cardCount) * 8;
            //Vector3 card_position = new Vector3(x + instance.transform.position.x, y + instance.transform.position.y + count_lerf, 0f);
            Vector3 card_position = new Vector3(x+960, y + count_lerf-1080-540-270, 0f);
            
            float size = 1.0f - (0.04f * cardCount);
            Vector3 scale_calcul = new Vector3(size, size, 1f);

            //움직임, 각도, 크기 재설정 duration 동안
            card_Objects[i].transform.DORotate(rotation.eulerAngles, duration, RotateMode.Fast);
            card_Objects[i].transform.DOMove(card_position, duration);
            card_Objects[i].transform.DOScale(scale_calcul, duration);
        }
    }
    private void init_Grid()
    {
        draggedCard = null;
        instance = GameObject.Find("Discard");
        card_List = Game_Manager.instance.hand_List;
    }
    private IEnumerator drawToDelayCard(Game_Manager game_Manager,GameObject parent, int num, float delay)
    {
        init_Grid();
        for (int i = 0; i < num; i++)
        {
            //카드 위치 변경
            int count = game_Manager.draw_List.Count;
            int random = UnityEngine.Random.Range(0, count - 1);
            try
            {
                GameObject gb = game_Manager.draw_List[random];     //
                game_Manager.draw_List.Remove(gb);
                game_Manager.hand_List.Add(gb);     //추가
                On_Hand(gb, parent.transform);      //카드에 이벤트 트리거 추가, 부모 설정
                gb.transform.DOMove(instance.transform.position, 0f);
            }
            catch
            {
                Debug.Log("카드 뽑기 문제 발생");
            }
            //카드 정렬
            On_CircularLayout(card_List, 0.5f);

            //기다리기
            yield return new WaitForSeconds(delay);
        }
    }
    void Card_Use(GameObject targetObject)
    {

        PlayerImage target = targetObject.GetComponent<PlayerImage>();
        bool isPlayer = target.isPlayer(draggedCard);
        card_List.Remove(draggedCard);
        draggedCard.transform.DOKill();             //

        //구 버전 딜 넣기
        Card card = draggedCard.GetComponent<Card>();
        if(card.data.var == "즉사기")
        {
            Game_Manager.instance.DeathOfDeath();
            return;
        }
        //target.player.On_Hit(card.data.effect);

        //데미지 띄우기
        Vector3 damagePos = targetObject.transform.position + new Vector3(0,100,0);
        GameObject gb = Instantiate(DamageInstance, targetObject.transform);
        gb.transform.position = damagePos;
        gb.GetComponent<DamageInstance>().damage = int.Parse(card.data.effect.ToString());
        StartCoroutine(FadeOutFuc(1f, gb));


        On_CircularLayout(card_List, duration);     //카드 사용후 재배치
        draggedCard = null;                         //
    }
    IEnumerator FadeOutFuc(float seconds, GameObject destroyObj)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(destroyObj);
    }



    private void On_Hand(GameObject card, Transform parent)
    {
        AddDraggableBehavior(card);
        card.transform.SetParent(parent);
    }
    private static void Out_Hand(GameObject card, Transform parent)
    {
        Destroy(card.AddComponent<EventTrigger>());
        card.transform.SetParent(parent);
    }

    void AddDraggableBehavior(GameObject card)
    {
        // EventTrigger 컴포넌트 추가
        EventTrigger eventTrigger = card.AddComponent<EventTrigger>();

        // 드래그 시작 이벤트
        EventTrigger.Entry onBeginDrag = new EventTrigger.Entry();
        onBeginDrag.eventID = EventTriggerType.BeginDrag;
        onBeginDrag.callback.AddListener((data) => OnBeginDrag((PointerEventData)data));
        eventTrigger.triggers.Add(onBeginDrag);

        // 드래그 진행 이벤트
        EventTrigger.Entry onDrag = new EventTrigger.Entry();
        onDrag.eventID = EventTriggerType.Drag;
        onDrag.callback.AddListener((data) => OnDrag((PointerEventData)data));
        eventTrigger.triggers.Add(onDrag);

        // 드롭 이벤트
        EventTrigger.Entry onEndDrag = new EventTrigger.Entry();
        onEndDrag.eventID = EventTriggerType.EndDrag;
        onEndDrag.callback.AddListener((data) => OnEndDrag((PointerEventData)data));
        eventTrigger.triggers.Add(onEndDrag);
    }
    void On_Card_Renderer_Disable()
    {
        if(draggedCard is not null)
        {
            Card card = draggedCard.GetComponent<Card>();
            card.SetEnableRenderer(false);
        }
    }
    void On_Card_Renederer_Set_Alpha(float alpha)
    {
        if(draggedCard != null)
        {
            Card card = draggedCard.GetComponent<Card>();
            card.SetRendererAlpha(alpha);
        }
        
    }
    void On_Card_Renderer_Enable()
    {
        if(draggedCard is not null)
        {
            Card card = draggedCard.GetComponent<Card>();
            card.SetEnableRenderer(true);
            On_Card_Renederer_Set_Alpha(1f);
        }
    }

    void OnBeginDrag(PointerEventData eventData)
    {
        if(draggedCard is null)
        {
            Debug.Log("드래그 시작");
            draggedCard = eventData.pointerDrag;
            initialCardPosition = eventData.position; // 원래 위치 저장
            initialCardRotation = draggedCard.transform.rotation;
            draggedCard.transform.rotation = Quaternion.identity;
        }
    }

    void OnDrag(PointerEventData eventData)
    {
        if (draggedCard is not null)
        {
            RectTransform rectTransform = draggedCard.GetComponent<RectTransform>();
            //거리 비례 흐릿함 만들기
            //float alpha = 1f - Mathf.Clamp01(distance/200);
            //On_Card_Renederer_Set_Alpha(alpha); 
            float distance = Vector3.Distance(eventData.position, initialCardPosition);
            if(distance > 100)
            {

            }
            else
            {
                rectTransform.anchoredPosition += eventData.delta;
            }



            SetCursorForCardOutside(!(distance > card_area));
        }
    }


    //여기 수정해야됨
    void OnEndDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = mousePosition;


        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach(RaycastResult result in results)
        {
            GameObject clObj = result.gameObject;
            PlayerImage plImg = clObj.GetComponent<PlayerImage>();
            if(plImg is not null)
            {
                //수정해야하는 부분
                //단일 책임 원칙 위배
                Card_Use(clObj);
                Debug.Log("클릭된 UI : " + clObj.name);
                return;
            }
            Debug.Log("클릭된 UI : " + clObj.name);
        }
        On_CircularLayout(card_List, duration);       //카드 움직이기
        On_Card_Renderer_Enable();
        draggedCard = null;
        Cursor.SetCursor(originCursorTexture, Vector2.zero, CursorMode.Auto);
    }


    IEnumerator MoveAndRotateToInitialPosition(float duration, float elapsedTime)
    {
        while(elapsedTime < duration)
        {
            draggedCard.transform.position = Vector3.Lerp(draggedCard.transform.position, initialCardPosition, elapsedTime / duration);
            draggedCard.transform.rotation = Quaternion.Slerp(draggedCard.transform.rotation, initialCardRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        draggedCard.transform.position = initialCardPosition;
        draggedCard.transform.rotation = initialCardRotation;
        draggedCard.SetActive(true);
        draggedCard = null;
    }
    static void SetCursorForCardOutside(bool able)
    {
        // 카드가 일정 거리를 벗어나면 커서를 변경
        if (able)
        {
            Cursor.SetCursor(originCursorTexture, Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(yourCursorTexture, Vector2.zero, CursorMode.Auto);
        }
    }
}
