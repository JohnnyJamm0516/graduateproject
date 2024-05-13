using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;


public class Map_Create : MonoBehaviour
{
    public Tilemap tilemap; // Unity Inspector에서 할당
    public Tile baseTile; // 기본 타일, Unity Inspector에서 할당
    //public Dictionary<string, Tile> roomTypeTiles = new Dictionary<string, Tile>();
    public Tile startTile; // 시작 타일, Unity Inspector에서 할당
    public int width = 20; // 맵의 가로 크기
    public int height = 20; // 맵의 세로 크기
    public string Roomfilepath = "Mapdata";
    private Map_Data rooms;

    HashSet<Vector3> placedPositions = new HashSet<Vector3>();

    public List<RoomTypeTile> roomTypeTilesList = new List<RoomTypeTile>();
    public List<RoomData> roomDataList = new List<RoomData>();

    private int ct = 0; //방 개수 카운터


    [System.Serializable]
    public class Map_Data
    {
        public List<RoomData> mapDataList;
    }

    // 방 데이터를 나타내는 클래스
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

    // 방 데이터 리스트
    Vector3Int HexOffset(int x, int y)
    {
        // 육각형 타일이 수직으로 배열되어 있다고 가정
        // 짝수 또는 홀수 열에 따라 Y 좌표가 다르게 조정됩니다.
        int newY = y + (x / 2); // 혹은 x >> 1; 짝수 열일 경우
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
            Vector3Int startPosition = new Vector3Int(0, 0, 0); // 시작 위치 설정
            PlaceStartRoom(startPosition); // 시작 방 배치
            ExpandRoomsFromStart(startPosition); // 시작점에서 방을 뻗어나가게 배치
        }
        Debug.Log($"총 방의 개수는 {ct}개 입니다.");//방 개수 카운터

    }

    void PlaceStartRoom(Vector3Int position)
    {
        tilemap.SetTile(position, startTile); // 시작 타일 배치
        placedPositions.Add(position);
    }


    bool CanPlaceRoomAt(Vector3 position)
    {
        // 이미 배치된 위치에 방이 있는지 확인
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
                    string selectedRoomType = roomTypes[Random.Range(0, roomTypes.Count)]; // 방 유형 무작위 선택
                    PlaceRoom(newPosition, selectedRoomType);
                    positionsQueue.Enqueue(newPosition); // 새 위치를 큐에 추가하여 해당 위치에서 또 다른 방을 뻗어 나갈 수 있게 함
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
            int retryCount = 0; // 재시도 횟수 카운터
            int maxRetry = 25; // 최대 재시도 횟수

            while (!placed && retryCount < maxRetry)
            {
                //Vector3Int position = new Vector3Int(UnityEngine.Random.Range(0, width), UnityEngine.Random.Range(0, height), 0);
                Vector3Int position = new Vector3Int(UnityEngine.Random.Range(-width, width), UnityEngine.Random.Range(-height, height), 0);
                // 육각형 그리드에 맞게 position 변환
                position = HexOffset(position.x, position.y);

                if (CanPlaceRoomAt(position))
                {
                    int randomIndex = UnityEngine.Random.Range(0, filteredRooms.Count);
                    RoomData selectedRoom = filteredRooms[randomIndex];

                    Debug.Log($"방 {selectedRoom.roomId}를 {position}에 배치하였습니다.");
                    placedPositions.Add(position); // 배치된 위치에 추가
                    ct++;//방 개수 카운터

                    Tile tile = roomTypeTilesList.Find(item => item.roomType == roomType)?.tile;
                    if (tile != null)
                    {
                        tilemap.SetTile(position, tile);
                    }
                    else
                    {
                        Debug.LogError($"'{roomType}'에 해당하는 타일이 없습니다.");
                    }
                    placed = true; // 방을 성공적으로 배치했으므로 true로 설정
                }
                else
                {
                    Debug.Log($"방을 {position}에 배치할 수 없습니다. 다른 위치를 시도합니다.");
                    retryCount++; // 재시도 횟수 증가
                }
            }

            if (!placed)
            {
                Debug.LogError($"방 배치에 실패했습니다. 모든 재시도가 실패했습니다. 설정을 확인해 주세요.");
            }
        }
    }*/


}
