using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data<T> : IEnumerable<T>
{
    public List<T> data;
    public IEnumerator<T> GetEnumerator()
    {
        return data.GetEnumerator();
    }

    // IEnumerable 인터페이스 구현
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public class Game_Manager : MonoBehaviour
{
    //객체 정의
    //게임 매니저 역할을 해줌
    //1.데이터 관리(저장, 불러오기)
    //2.데이터 연동(다른 곳에 참조형으로 데이터를 하나 취급해줌)
    //3.이벤트 시스템

    //내부
    //저장 불러오기 내부에서만 작동 가능하도록 바꿈


    //외부
    //이벤트를 통하여 호출




    //링크 필요한 프리팹
    public static Game_Manager instance;    //싱글톤
    public static Cards_SO cards_SO;

    public GameObject player_Prefab;    //플레이어 원본 프리팹
    public GameObject card_Prefab;      //카드 프리팹


    //new() 생성 필드들 즉 소유 필드
    [SerializeField] private GameData game1_Save = new();              //game1 세이브 파일
    [SerializeField] private CardData card_Save = new();              //card_data 세이브 파일
    [SerializeField] private Stages_Data stages_Save = new();         //저장된 스테이지(적 배열) 목록
    [SerializeField] private Data<Card_UI> deck_Save = new();         //덱 불러오기
    [SerializeField] private Data<Player_Data_UI> player_Save = new();//플레이어(몇명인거까지) 상태 불러오기
    [SerializeField] private Data<Enemy_Data_UI> enemies_Data = new();//적의 배열 목록

    public List<GameObject> cards_List = new();     //카드 도감
    public List<GameObject> draw_List = new();      //뽑을 카드 더미
    public List<GameObject> hand_List = new();      //손패 더미
    public List<GameObject> discard_List = new();   //버린 카드 더미


    //플레이어, 적 게임오브젝트 목록 레퍼런스 타입
    public List<GameObject> player_List = new();
    public List<GameObject> enemy_List = new();


    //자동 연결 되는 부분
    public List<GameObject> childObjects = new();   //자식 오브젝트 모두 포함
    public GameObject object_Mana;             //마나 오브젝트
    public GameObject object_PlayerGrid;       //플레이어 위치
    public GameObject object_EnemyGrid;        //적 위치
    public GameObject object_DrawGrid;         //뽑을 카드더미 위치
    public GameObject object_DiscardGrid;      //버린 카드더미 위치

    public int mana_count;
    public int max_mana;


    //데이터 저장 경로 설정 변수
    string Path;
    public string dataPath;//게임 데이터 경로
    public string cardPath;//카드 데이터 경로
    public string stagesPath;//스테이지 경로
    public string deckPath;//덱 경로
    public string playerPath;
    public string enemiesPath;//적 경로
    public string cardsPath;





    private void Start()
    {
        //<예시 케이스 호출>

        //임시로 플레이어 생성
        Init_Player(); //플레이어 배치 전투마다 한번만 작동 스태틱으로 구현해야함

        //임시로 적 생성
        Init_Enemies(); //적 배치 전투마다 한번만 작동 스태틱으로 구현해야함

        //캐릭터 바라보는 방향 변경
        player_List[0].transform.Find("PlayerImage").DORotate(new Vector3(0, 180, 0), 0f);  //리소스 일관성이 없어서 배치 변경이 필요해서 넣었음

        //뽑을 카드 더미 생성 
        ExFuc();        //전투 시작 시 일단은 한번만 작동하는 "덱 기반" 카드 생성 모듈 역시 스태틱으로 바꿔야 할 것같음

        //턴 시퀀스 실행
        ExTurn();       //전투 시작 시 모두 정상 신호일 경우 턴 시작 시퀀스 시작되도록 바꿔주는 부분
        
    }


    private void Awake()
    {
        //게임 매니저가 처음 실행시 거쳐가는 시퀀스

        //경로 지정
        SetPath();

        //싱글톤
        SetSingleton();
        //DontDestroyOnLoad(this.gameObject);

        //필드 레퍼런스 매핑
        RefFieldSet();

        //로드 시작
        Load_Data();
        Load_Card();
        Load_Stages();
        Load_Deck();
        Load_Player();
        Load_Enemies();

        //자식 오브젝트 배열 생성
        FindChildObjects();
    }
    public void Awake_GameManger()
    {
        //게임 매니저가 처음 실행시 거쳐가는 시퀀스

        //경로 지정
        SetPath();

        //싱글톤
        SetSingleton();
        //DontDestroyOnLoad(this.gameObject);

        //필드 레퍼런스 매핑
        RefFieldSet();

        //로드 시작
        Load_Data();
        Load_Card();
        Load_Stages();
        Load_Deck();
        Load_Player();
        Load_Enemies();

        //자식 오브젝트 배열 생성
        FindChildObjects();
    }


    private void Init_Player()
    {
        if (player_Save == null)
        {
            Debug.Log(playerPath + "에 데이터가 없습니다.");
        }
        else
        {
            for (int i = 0; i < player_Save.data.Count; i++)
            {
                GameObject gameObject = Instantiate(player_Prefab);
                Player player_Script = gameObject.GetComponent<Player>();
                player_Script.player_Data = player_Save.data[i];
                gameObject.transform.SetParent(object_PlayerGrid.transform);
                player_List.Add(gameObject);
            }
        }
    }



    private void Init_Enemies()
    {
        if (enemies_Data == null)
        {
            Debug.Log(enemiesPath + "에 데이터가 없습니다.");
        }
        else
        {
            for (int i = 0; i < enemies_Data.data.Count; i++)
            {
                GameObject gameObject = Instantiate(player_Prefab);
                Player player_Script = gameObject.GetComponent<Player>();
                player_Script.player_Data = enemies_Data.data[i];
                gameObject.transform.SetParent(object_EnemyGrid.transform);

                enemy_List.Add(gameObject);
            }
        }
    }

    private void SetPath()
    {
        Path = Application.persistentDataPath + "/";
        dataPath = Path + "game1";
        cardPath = Path + "card_data";
        stagesPath = Path + "stages_data";
        deckPath = Path + "deck";
        playerPath = Path + "player";
        enemiesPath = Path + "enemys";
        cardsPath = Path + "cards";
    }
    private void SetSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }
    }
    private void RefFieldSet()
    {

        object_Mana = transform.Find("Mana").gameObject;
        object_PlayerGrid = transform.Find("PlayerGrid").gameObject;
        object_EnemyGrid = transform.Find("EnemyGrid").gameObject;
        object_DrawGrid = transform.Find("DrawGrid").gameObject;
        object_DiscardGrid = transform.Find("DiscardGrid").gameObject;
    }

    void FindChildObjects()
    {
        foreach(Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }
    }



    public void Save_Data_Fuc(string datapath, object data, bool prettyPrint)
    {
        //데이터 저장 모듈

        Debug.Log(datapath + "에 저장");
        string json = JsonUtility.ToJson(data, prettyPrint);
        File.WriteAllText(datapath, json);
    }

    public void Save_Data()
    {
        //게임 데이터 저장하는 기능\
        if (File.Exists(dataPath))
        {

        }
        Save_Data_Fuc(dataPath, game1_Save, true);


    }
    public void Save_Card()
    {
        //카드 데이터 저장하는 기능
        Save_Data_Fuc(cardPath, card_Save, true);
    }
    public void Save_Stages()
    {
        Save_Data_Fuc(stagesPath, stages_Save, true);
    }
    public void Save_Deck()
    {
        Save_Data_Fuc(deckPath, deck_Save, true);
    }
    public void Save_Player()
    {
        Save_Data_Fuc(playerPath, player_Save, true);
    }
    public void Save_Enemys()
    {
        Save_Data_Fuc(enemiesPath, enemies_Data, true);
    }
    public void Load_Data()
    {
        //모든 데이터 로드하는 기능
        //게임 켜질 때 작동
        if (File.Exists(dataPath))
        {
            Debug.Log(dataPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(dataPath);
            game1_Save = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.Log(dataPath + "에 저장된 파일이 없습니다.");
            Save_Data();
            Load_Data();
        }
    }
    public void Load_Card()
    {
        //카드를 불러오는 기능

        if (File.Exists(cardPath))
        {
            Debug.Log(cardPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(cardPath);
            card_Save = JsonUtility.FromJson<CardData>(json);
        }
        else
        {
            Debug.Log(cardPath + "에 저장된 파일이 없습니다.");
            Save_Card();
            Load_Card();
        }
    }
    public void Load_Stages()
    {
        //카드를 불러오는 기능

        if (File.Exists(stagesPath))
        {
            Debug.Log(stagesPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(stagesPath);
            stages_Save = JsonUtility.FromJson<Stages_Data>(json);
        }
        else
        {
            Debug.Log(stagesPath + "에 저장된 파일이 없습니다.");
            Save_Stages();
            Load_Stages();
        }
    }
    public void Load_Deck()
    {
        //덱 불러오기
        if (File.Exists(deckPath))
        {
            Debug.Log(deckPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(deckPath);
            deck_Save = JsonUtility.FromJson<Data<Card_UI>>(json);
        }
        else
        {
            Debug.Log(deckPath + "에 저장된 파일이 없습니다.");
            Save_Deck();
            Load_Deck();
        }
    }
    public void Load_Player()
    {
        if (File.Exists(playerPath))
        {
            Debug.Log(playerPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(playerPath);
            player_Save = JsonUtility.FromJson<Data<Player_Data_UI>>(json);
        }
        else
        {
            Debug.Log(playerPath + "에 저장된 파일이 없습니다.");
            Save_Player();
            Load_Player();
        }
    }
    public void Load_Enemies()
    {
        //모든 데이터 로드하는 기능
        //게임 켜질 때 작동
        if (File.Exists(enemiesPath))
        {
            Debug.Log(enemiesPath + "가 정상적으로 로드되었습니다.");
            string json = File.ReadAllText(enemiesPath);
            enemies_Data = JsonUtility.FromJson<Data<Enemy_Data_UI>>(json);
        }
        else
        {
            Debug.Log(enemiesPath + "에 저장된 파일이 없습니다.");
            Save_Enemys();
            Load_Enemies();
        }
    }


    public void On_Ex_Fuc()
    {
        //임시 저장 프로토콜
        Save_Data();
        Save_Card();
        Save_Stages();
        Save_Enemys();
        Save_Deck();
        Save_Player();
    }




    private void ExFuc()
    {
        //카드 생성 시퀀스
        DrawGridModule.On_Add_Card(deck_Save);
    }
    //public GameObject damageInstance;   //데미지 인스턴스 예시 매칭
    //현재 인스턴스가 없음
    public GameObject layer;    //게임 종료 합성 레이어 예시 매칭
    public GameObject title;    //전투 종료 인스턴스 예시 매칭
    public void DeathOfDeath()
    {
        //턴 종료 예시
        foreach(var enemy in enemy_List)
        {
            Destroy(enemy);
        }
        StartCoroutine(LayerLoad());
    }
    public IEnumerator LayerLoad()
    {
        //게임 종료 예시
        GameObject gb = Instantiate(layer, this.gameObject.transform);
        for(int i = 0; i < 100; i++)
        {
            CanvasRenderer cr = gb.gameObject.transform.GetComponent<CanvasRenderer>();
            cr.SetAlpha((0.9f*i)/100);
            yield return new WaitForSeconds(0.01f);
        }
        Instantiate(title, gameObject.transform);
    }



    public void Enemy_Turn()
    {

        //타겟 설정
        GameObject targetObject = player_List[0];       //0번 위치
        Player player = targetObject.GetComponent<Player>();


        //행동 표시
        //StartCoroutine(Ex_EX(0.5f, targetObject, player));

        //움직임
        StartCoroutine(EnemyMove(0.5f, targetObject, player));

        //행동 적용
        StartCoroutine(EnemyAct(0.5f, targetObject, player));

        
    }

    IEnumerator EnemyMove(float seconds, GameObject targetObject, Player player)
    {
        //적 움직여주는 부분
        foreach(var enemy in enemy_List)
        {
            //이동 부분
            Vector3 pos = enemy.transform.position;
            enemy.transform.DOMove(targetObject.transform.position, seconds);

            yield return new WaitForSeconds(seconds);
            //돌아가기
            enemy.transform.DOMove(pos, seconds);
            yield return new WaitForSeconds(seconds);
        }
    }
    IEnumerator EnemyAct(float seconds, GameObject targetObject, Player player)
    {
        //적 움직여주는 부분
        foreach (var enemy in enemies_Data)
        {
            //효과 부분
            int random_choice = UnityEngine.Random.Range(0, enemy.mob_Act.Length);
            string mob_Act_Str = enemy.mob_Act[random_choice];
            GameObject.Find("ActionManager").GetComponent<ActionManager>().ExecuteEffects(mob_Act_Str);
            yield return new WaitForSeconds(seconds);
        }
    }
    public void MyTurn()
    {
        ExTurn();
    }

    IEnumerator Ex_EX(float seconds, GameObject targetObject, Player player)
    {
        //적 공격 예제
        //StartCoroutine(FadeOutFuc(0.5f, targetObject, player));
        yield return new WaitForSeconds(1f);
        //StartCoroutine(FadeOutFuc(0.5f, targetObject, player));
        yield return new WaitForSeconds(1f);
        //StartCoroutine(FadeOutFuc(0.5f, targetObject, player));
        yield return new WaitForSeconds(1f);
    }

    /*
    IEnumerator FadeOutFuc(float seconds, GameObject targetObject, Player player)
    {

        //현재 인스턴스가 없어서 미작동

        //데미지를 띄우고 서서히 사라지면서 위로 이동함
        //player.On_Hit(10);
        Vector3 damagePos = targetObject.transform.position + new Vector3(0, 100, 0);



        GameObject gb = Instantiate(damageInstance, targetObject.transform);
        gb.transform.position = damagePos;
        gb.GetComponent<DamageInstance>().damage = 10;

        Vector3 movePos = gb.transform.position + new Vector3(0, 150, 0);
        gb.transform.DOMove(movePos, 0.5f);
        yield return new WaitForSeconds(seconds);
        Destroy(gb);
    }*/


    public void ExTurn()
    {
        Ex_Turn_Data.Ex_Turn();     //턴 시퀀스 시작
    }
    private class Ex_Turn_Data
    {
        public static void Ex_Turn()
        {
            GameObject gb = GameObject.Find("HandGrid");
            HandGrid hand = gb.GetComponent<HandGrid>();
            hand.DrawCard(5);
        }
    }
}
