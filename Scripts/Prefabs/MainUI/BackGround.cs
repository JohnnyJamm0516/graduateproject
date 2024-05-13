using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    Game_Manager game_Manager;
    public Image_SO SO;
    Image image;
    public int sourceNum;// 미완성
    public string folderPath;
    public string imageName;
    List<Image_SO_Data> list;
    Sprite source;
    private void Start()
    {
        game_Manager = Game_Manager.instance;
        image = GetComponent<Image>();
        list = SO.FindSpritesByTheme(folderPath);
        source = Image_SO.FindSpriteByName(list, imageName);
    }
    private void Update()
    {
        if (source)
        {
            image.sprite = source;
        }
        else
        {
            Debug.Log(this.gameObject.ToString() + "에서 이미지 부재 발생");
        }
    }
}
