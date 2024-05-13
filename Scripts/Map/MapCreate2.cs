using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class Map_Create : MonoBehaviour
{
    public Tilemap tilemap; // Unity Inspector���� �Ҵ�
    public Tile baseTile; // �⺻ Ÿ��, Unity Inspector���� �Ҵ�
    //public Dictionary<string, Tile> roomTypeTiles = new Dictionary<string, Tile>();
    public Tile startTile; // ���� Ÿ��, Unity Inspector���� �Ҵ�
    public int width = 20; // ���� ���� ũ��
    public int height = 20; // ���� ���� ũ��
    public string Roomfilepath = "Mapdata";
    private Map_Data rooms;

    HashSet<Vector3> placedPositions = new HashSet<Vector3>();

    public List<RoomTypeTile> roomTypeTilesList = new List<RoomTypeTile>();
    public List<RoomData> roomDataList = new List<RoomData>();

    private int ct = 0; //�� ���� ī����


    [System.Serializable]
    public class Map_Data
    {
        public List<RoomData> mapDataList;
    }

    // �� �����͸� ��Ÿ���� Ŭ����
    [System.Serializable]
    public class RoomData
    {
        public string floorId;
        public string roomId;
        public string rewardPotion;
        public string rewardCon;
        public string increaseBossGauge;
        public string roomImgPath;
        public string monsterList;
        public int monsteramount;
        public string money;
        public string rewardCard;
        public string rewardAcc;
        public bool conTF;
        public bool accTF;
        public string roomType;
    }
    [System.Serializable]
    public class RoomTypeTile
    {
        public string roomType;
        public Tile tile;
    }

    // �� ������ ����Ʈ
    Vector3Int HexOffset(int x, int y)
    {
        // ������ Ÿ���� �������� �迭�Ǿ� �ִٰ� ����
        // ¦�� �Ǵ� Ȧ�� ���� ���� Y ��ǥ�� �ٸ��� �����˴ϴ�.
        int newY = y + (x / 2); // Ȥ�� x >> 1; ¦�� ���� ���
        return new Vector3Int(x, newY, 0);
    }



    public static Map_Data LoadMapData(string path)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        if (textAsset == null)
        {
            Debug.LogError("Cannot find JSON file at path: " + path);
            return null;
        }

        return JsonUtility.FromJson<Map_Data>(textAsset.text);
    }

    void Start()
    {
        rooms = LoadMapData(Roomfilepath);
        if (rooms != null)
        {
            Vector3Int startPosition = new Vector3Int(0, 0, 0); // ���� ��ġ ����
            PlaceStartRoom(startPosition); // ���� �� ��ġ
            ExpandRoomsFromStart(startPosition); // ���������� ���� ������� ��ġ
        }
        Debug.Log($"�� ���� ������ {ct}�� �Դϴ�.");//�� ���� ī����

    }

    void PlaceStartRoom(Vector3Int position)
    {
        tilemap.SetTile(position, startTile); // ���� Ÿ�� ��ġ
        placedPositions.Add(position);
    }


    bool CanPlaceRoomAt(Vector3 position)
    {
        // �̹� ��ġ�� ��ġ�� ���� �ִ��� Ȯ��
        return !placedPositions.Contains(position);
    }

    void ExpandRoomsFromStart(Vector3Int startPosition)
    {
        List<string> roomTypes = new List<string> { "Battle", "Rest", };//"Shop",  "Event" };
        Queue<Vector3Int> positionsQueue = new Queue<Vector3Int>();
        positionsQueue.Enqueue(startPosition);

        while (positionsQueue.Count > 0)
        {
            Vector3Int currentPosition = positionsQueue.Dequeue();
            foreach (Vector3Int direction in new Vector3Int[] {Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right})
            {
                Vector3Int newPosition = currentPosition + direction;
                if (CanPlaceRoomAt(newPosition))
                {
                    string selectedRoomType = roomTypes[Random.Range(0, roomTypes.Count)]; // �� ���� ������ ����
                    PlaceRoom(newPosition, selectedRoomType);
                    positionsQueue.Enqueue(newPosition); // �� ��ġ�� ť�� �߰��Ͽ� �ش� ��ġ���� �� �ٸ� ���� ���� ���� �� �ְ� ��
                }
            }
        }
    }

    void PlaceRoom(Vector3Int position, string roomType)
    {
        RoomTypeTile roomTile = roomTypeTilesList.Find(item => item.roomType == roomType);
        if (roomTile != null && roomTile.tile != null)
        {
            tilemap.SetTile(position, roomTile.tile);
            placedPositions.Add(position);
        }
    }


    /*void PlaceRooms(string roomType, string floorId, int minCount, int maxCount)
    {
        var filteredRooms = rooms.mapDataList.Where(room => room.roomType == roomType && room.floorId == floorId).ToList();
        int roomCount = UnityEngine.Random.Range(minCount, maxCount + 1);

        for (int i = 0; i < roomCount; i++)
        {
            bool placed = false;
            int retryCount = 0; // ��õ� Ƚ�� ī����
            int maxRetry = 25; // �ִ� ��õ� Ƚ��

            while (!placed && retryCount < maxRetry)
            {
                //Vector3Int position = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), 0);
                Vector3Int position = new Vector3Int(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height), 0);
                // ������ �׸��忡 �°� position ��ȯ
                position = HexOffset(position.x, position.y);

                if (CanPlaceRoomAt(position))
                {
                    int randomIndex = UnityEngine.Random.Range(0, filteredRooms.Count);
                    RoomData selectedRoom = filteredRooms[randomIndex];

                    Debug.Log($"�� {selectedRoom.roomId}�� {position}�� ��ġ�Ͽ����ϴ�.");
                    placedPositions.Add(position); // ��ġ�� ��ġ�� �߰�
                    ct++;//�� ���� ī����

                    Tile tile = roomTypeTilesList.Find(item => item.roomType == roomType)?.tile;
                    if (tile != null)
                    {
                        tilemap.SetTile(position, tile);
                    }
                    else
                    {
                        Debug.LogError($"'{roomType}'�� �ش��ϴ� Ÿ���� �����ϴ�.");
                    }
                    placed = true; // ���� ���������� ��ġ�����Ƿ� true�� ����
                }
                else
                {
                    Debug.Log($"���� {position}�� ��ġ�� �� �����ϴ�. �ٸ� ��ġ�� �õ��մϴ�.");
                    retryCount++; // ��õ� Ƚ�� ����
                }
            }

            if (!placed)
            {
                Debug.LogError($"�� ��ġ�� �����߽��ϴ�. ��� ��õ��� �����߽��ϴ�. ������ Ȯ���� �ּ���.");
            }
        }
    }*/


}
