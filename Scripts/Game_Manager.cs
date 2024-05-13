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

    // IEnumerable �������̽� ����
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

[Serializable]
public class Game_Manager : MonoBehaviour
{
    //��ü ����
    //���� �Ŵ��� ������ ����
    //1.������ ����(����, �ҷ�����)
    //2.������ ����(�ٸ� ���� ���������� �����͸� �ϳ� �������)
    //3.�̺�Ʈ �ý���

    //����
    //���� �ҷ����� ���ο����� �۵� �����ϵ��� �ٲ�


    //�ܺ�
    //�̺�Ʈ�� ���Ͽ� ȣ��




    //��ũ �ʿ��� ������
    public static Game_Manager instance;    //�̱���
    public static Cards_SO cards_SO;

    public GameObject player_Prefab;    //�÷��̾� ���� ������
    public GameObject card_Prefab;      //ī�� ������


    //new() ���� �ʵ�� �� ���� �ʵ�
    [SerializeField] private GameData game1_Save = new();              //game1 ���̺� ����
    [SerializeField] private CardData card_Save = new();              //card_data ���̺� ����
    [SerializeField] private Stages_Data stages_Save = new();         //����� ��������(�� �迭) ���
    [SerializeField] private Data<Card_UI> deck_Save = new();         //�� �ҷ�����
    [SerializeField] private Data<Player_Data_UI> player_Save = new();//�÷��̾�(����ΰű���) ���� �ҷ�����
    [SerializeField] private Data<Enemy_Data_UI> enemies_Data = new();//���� �迭 ���

    public List<GameObject> cards_List = new();     //ī�� ����
    public List<GameObject> draw_List = new();      //���� ī�� ����
    public List<GameObject> hand_List = new();      //���� ����
    public List<GameObject> discard_List = new();   //���� ī�� ����


    //�÷��̾�, �� ���ӿ�����Ʈ ��� ���۷��� Ÿ��
    public List<GameObject> player_List = new();
    public List<GameObject> enemy_List = new();


    //�ڵ� ���� �Ǵ� �κ�
    public List<GameObject> childObjects = new();   //�ڽ� ������Ʈ ��� ����
    public GameObject object_Mana;             //���� ������Ʈ
    public GameObject object_PlayerGrid;       //�÷��̾� ��ġ
    public GameObject object_EnemyGrid;        //�� ��ġ
    public GameObject object_DrawGrid;         //���� ī����� ��ġ
    public GameObject object_DiscardGrid;      //���� ī����� ��ġ

    public int mana_count;
    public int max_mana;


    //������ ���� ��� ���� ����
    string Path;
    public string dataPath;//���� ������ ���
    public string cardPath;//ī�� ������ ���
    public string stagesPath;//�������� ���
    public string deckPath;//�� ���
    public string playerPath;
    public string enemiesPath;//�� ���
    public string cardsPath;





    private void Start()
    {
        //<���� ���̽� ȣ��>

        //�ӽ÷� �÷��̾� ����
        Init_Player(); //�÷��̾� ��ġ �������� �ѹ��� �۵� ����ƽ���� �����ؾ���

        //�ӽ÷� �� ����
        Init_Enemies(); //�� ��ġ �������� �ѹ��� �۵� ����ƽ���� �����ؾ���

        //ĳ���� �ٶ󺸴� ���� ����
        player_List[0].transform.Find("PlayerImage").DORotate(new Vector3(0, 180, 0), 0f);  //���ҽ� �ϰ����� ��� ��ġ ������ �ʿ��ؼ� �־���

        //���� ī�� ���� ���� 
        ExFuc();        //���� ���� �� �ϴ��� �ѹ��� �۵��ϴ� "�� ���" ī�� ���� ��� ���� ����ƽ���� �ٲ�� �� �Ͱ���

        //�� ������ ����
        ExTurn();       //���� ���� �� ��� ���� ��ȣ�� ��� �� ���� ������ ���۵ǵ��� �ٲ��ִ� �κ�
        
    }


    private void Awake()
    {
        //���� �Ŵ����� ó�� ����� ���İ��� ������

        //��� ����
        SetPath();

        //�̱���
        SetSingleton();
        //DontDestroyOnLoad(this.gameObject);

        //�ʵ� ���۷��� ����
        RefFieldSet();

        //�ε� ����
        Load_Data();
        Load_Card();
        Load_Stages();
        Load_Deck();
        Load_Player();
        Load_Enemies();

        //�ڽ� ������Ʈ �迭 ����
        FindChildObjects();
    }
    public void Awake_GameManger()
    {
        //���� �Ŵ����� ó�� ����� ���İ��� ������

        //��� ����
        SetPath();

        //�̱���
        SetSingleton();
        //DontDestroyOnLoad(this.gameObject);

        //�ʵ� ���۷��� ����
        RefFieldSet();

        //�ε� ����
        Load_Data();
        Load_Card();
        Load_Stages();
        Load_Deck();
        Load_Player();
        Load_Enemies();

        //�ڽ� ������Ʈ �迭 ����
        FindChildObjects();
    }


    private void Init_Player()
    {
        if (player_Save == null)
        {
            Debug.Log(playerPath + "�� �����Ͱ� �����ϴ�.");
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
            Debug.Log(enemiesPath + "�� �����Ͱ� �����ϴ�.");
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
        //������ ���� ���

        Debug.Log(datapath + "�� ����");
        string json = JsonUtility.ToJson(data, prettyPrint);
        File.WriteAllText(datapath, json);
    }

    public void Save_Data()
    {
        //���� ������ �����ϴ� ���\
        if (File.Exists(dataPath))
        {

        }
        Save_Data_Fuc(dataPath, game1_Save, true);


    }
    public void Save_Card()
    {
        //ī�� ������ �����ϴ� ���
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
        //��� ������ �ε��ϴ� ���
        //���� ���� �� �۵�
        if (File.Exists(dataPath))
        {
            Debug.Log(dataPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(dataPath);
            game1_Save = JsonUtility.FromJson<GameData>(json);
        }
        else
        {
            Debug.Log(dataPath + "�� ����� ������ �����ϴ�.");
            Save_Data();
            Load_Data();
        }
    }
    public void Load_Card()
    {
        //ī�带 �ҷ����� ���

        if (File.Exists(cardPath))
        {
            Debug.Log(cardPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(cardPath);
            card_Save = JsonUtility.FromJson<CardData>(json);
        }
        else
        {
            Debug.Log(cardPath + "�� ����� ������ �����ϴ�.");
            Save_Card();
            Load_Card();
        }
    }
    public void Load_Stages()
    {
        //ī�带 �ҷ����� ���

        if (File.Exists(stagesPath))
        {
            Debug.Log(stagesPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(stagesPath);
            stages_Save = JsonUtility.FromJson<Stages_Data>(json);
        }
        else
        {
            Debug.Log(stagesPath + "�� ����� ������ �����ϴ�.");
            Save_Stages();
            Load_Stages();
        }
    }
    public void Load_Deck()
    {
        //�� �ҷ�����
        if (File.Exists(deckPath))
        {
            Debug.Log(deckPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(deckPath);
            deck_Save = JsonUtility.FromJson<Data<Card_UI>>(json);
        }
        else
        {
            Debug.Log(deckPath + "�� ����� ������ �����ϴ�.");
            Save_Deck();
            Load_Deck();
        }
    }
    public void Load_Player()
    {
        if (File.Exists(playerPath))
        {
            Debug.Log(playerPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(playerPath);
            player_Save = JsonUtility.FromJson<Data<Player_Data_UI>>(json);
        }
        else
        {
            Debug.Log(playerPath + "�� ����� ������ �����ϴ�.");
            Save_Player();
            Load_Player();
        }
    }
    public void Load_Enemies()
    {
        //��� ������ �ε��ϴ� ���
        //���� ���� �� �۵�
        if (File.Exists(enemiesPath))
        {
            Debug.Log(enemiesPath + "�� ���������� �ε�Ǿ����ϴ�.");
            string json = File.ReadAllText(enemiesPath);
            enemies_Data = JsonUtility.FromJson<Data<Enemy_Data_UI>>(json);
        }
        else
        {
            Debug.Log(enemiesPath + "�� ����� ������ �����ϴ�.");
            Save_Enemys();
            Load_Enemies();
        }
    }


    public void On_Ex_Fuc()
    {
        //�ӽ� ���� ��������
        Save_Data();
        Save_Card();
        Save_Stages();
        Save_Enemys();
        Save_Deck();
        Save_Player();
    }




    private void ExFuc()
    {
        //ī�� ���� ������
        DrawGridModule.On_Add_Card(deck_Save);
    }
    //public GameObject damageInstance;   //������ �ν��Ͻ� ���� ��Ī
    //���� �ν��Ͻ��� ����
    public GameObject layer;    //���� ���� �ռ� ���̾� ���� ��Ī
    public GameObject title;    //���� ���� �ν��Ͻ� ���� ��Ī
    public void DeathOfDeath()
    {
        //�� ���� ����
        foreach(var enemy in enemy_List)
        {
            Destroy(enemy);
        }
        StartCoroutine(LayerLoad());
    }
    public IEnumerator LayerLoad()
    {
        //���� ���� ����
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

        //Ÿ�� ����
        GameObject targetObject = player_List[0];       //0�� ��ġ
        Player player = targetObject.GetComponent<Player>();


        //�ൿ ǥ��
        //StartCoroutine(Ex_EX(0.5f, targetObject, player));

        //������
        StartCoroutine(EnemyMove(0.5f, targetObject, player));

        //�ൿ ����
        StartCoroutine(EnemyAct(0.5f, targetObject, player));

        
    }

    IEnumerator EnemyMove(float seconds, GameObject targetObject, Player player)
    {
        //�� �������ִ� �κ�
        foreach(var enemy in enemy_List)
        {
            //�̵� �κ�
            Vector3 pos = enemy.transform.position;
            enemy.transform.DOMove(targetObject.transform.position, seconds);

            yield return new WaitForSeconds(seconds);
            //���ư���
            enemy.transform.DOMove(pos, seconds);
            yield return new WaitForSeconds(seconds);
        }
    }
    IEnumerator EnemyAct(float seconds, GameObject targetObject, Player player)
    {
        //�� �������ִ� �κ�
        foreach (var enemy in enemies_Data)
        {
            //ȿ�� �κ�
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
        //�� ���� ����
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

        //���� �ν��Ͻ��� ��� ���۵�

        //�������� ���� ������ ������鼭 ���� �̵���
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
        Ex_Turn_Data.Ex_Turn();     //�� ������ ����
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
