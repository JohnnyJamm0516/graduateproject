using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData
{
    public List<MapDataItem> mapDataList;
}


[System.Serializable]
public class MapDataItem
{
    public string floorId;
    public string roomId;
    public string rewardPotion;
    public string rewardCon;
    public string increaseBossGauge;
    public string roomImgPath;
    public int monsteramount;
    public string money;
    public string rewardAcc;
    public bool conTF;
    public bool accTF;
    public string roomType;
}


public class Map_Game_Trans : MonoBehaviour
{
    // Singleton instance
    public static Map_Game_Trans Instance { get; private set; }

    public static List<Enemy_Data_UI> enemies;
    public List<Enemy_Data_UI> Getenemies
    {
        get
        {
            if (enemies == null)
            {
                return enemies = new List<Enemy_Data_UI>();
            }
            else
            {
                return enemies;
            }
        }
        //set;
    }
    public static List<Card_UI> cards;
    public List<Card_UI> Getcards
    {
        get
        {
            if (cards == null)
            {
                return cards = new List<Card_UI>();
            }
            else
            {
                return cards;
            }
        }
        //set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function for loading enemies and cards data
    public void LoadData(List<Enemy_Data_UI> enemyData, List<Card_UI> cardData)
    {
        enemies = enemyData;
        cards = cardData;
    }

    // Functions to access data
    public List<Enemy_Data_UI> GetEnemies()
    {
        return enemies;
    }

    public List<Card_UI> GetCards()
    {
        return cards;
    }
}
