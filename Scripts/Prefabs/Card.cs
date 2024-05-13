using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    //역할
    //1.카드의 데이터가 변동되면 변동된 값이 보이도록 바꾸어줌
    //2.카드 데이터 변경 시스템 구현
    //3.식별자를 찾아서 고유 ID를 사용가능함


    //카드의 컴포넌트 접근 스크립트
    //카드 데이터는 데이터베이스 값임



    Game_Manager game_Manager;
    public Image_SO SO;
    public int instanceID;      //카드 식별자
    public Card_UI data; //만들 때 자동 연결
    public Image card_Rare_Image;
    public Image card_Name_Image;
    public Image card_Cost_Image;
    public Image card_Image;
    public Image card_Text_Image;
    public Image card_Grid_Image;
    public Image card_Type_Image;


    public TextMeshProUGUI card_Name_Text;
    public TextMeshProUGUI card_Cost_Text;
    public TextMeshProUGUI card_Text_Text;
    public TextMeshProUGUI card_Type_Text;


    public CanvasRenderer card_Rare_Image_Renderer;
    public CanvasRenderer card_Name_Image_Renderer;
    public CanvasRenderer card_Cost_Image_Renderer;
    public CanvasRenderer card_Image_Renderer;
    public CanvasRenderer card_Text_Image_Renderer;
    public CanvasRenderer card_Grid_Image_Renderer;
    public CanvasRenderer card_Type_Image_Renderer;


    public CanvasRenderer card_Name_Text_Renderer;
    public CanvasRenderer card_Cost_Text_Renderer;
    public CanvasRenderer card_Text_Text_Renderer;
    public CanvasRenderer card_Type_Text_Renderer;




    private void Start()
    {
        //초기화
        game_Manager = Game_Manager.instance;           //데이터베이스 연동
        instanceID = gameObject.GetInstanceID();        //식별자 등록
    }
    public void Change_Card()
    {
        //카드 데이터 변경시 적용되는 부분

        gameObject.name = data.card_name+gameObject.GetInstanceID().ToString();    //타이틀 
        card_Name_Text.text = data.card_name;    //타이틀 이름 변경
        card_Cost_Text.text = data.cost.ToString(); //코스트 변경
        Dictionary<string, object> variables = data.Data_Dictionary;
        string text_result = Card_UI.CalculateAndReplace(data.text, variables);
        card_Text_Text.text = text_result;
        card_Type_Text.text = data.type;

        List<Image_SO_Data> image_List = SO.FindSpritesByTheme("Assets/Image/Card_Logo");
        card_Image.sprite = Image_SO.FindSpriteByName(image_List, data.image);

        List<Image_SO_Data> rarity_Image_List = SO.FindSpritesByTheme("Assets/Image/Card_Rarity");
        card_Rare_Image.sprite = Image_SO.FindSpriteByName(rarity_Image_List, data.image);

        List<Image_SO_Data> type_Image_List = SO.FindSpritesByTheme("Assets/Image/Card_Type");
        card_Type_Image.sprite = Image_SO.FindSpriteByName(type_Image_List, data.image);
    }
    private void Update()
    {
        Change_Card();
    }
    public void SetEnableRenderer(bool able)
    {
        card_Rare_Image.enabled = able;
        card_Name_Image.enabled = able;
        card_Cost_Image.enabled = able;
        card_Image.enabled = able;
        card_Text_Image.enabled = able;
        card_Grid_Image.enabled = able;
        card_Type_Image.enabled = able;


        card_Name_Text.enabled = able;
        card_Cost_Text.enabled = able;
        card_Text_Text.enabled = able;
        card_Type_Text.enabled = able;
    }
    public void SetRendererAlpha(float alpha)
    {
        card_Rare_Image_Renderer.SetAlpha(alpha);
        card_Name_Image_Renderer.SetAlpha(alpha);
        card_Cost_Image_Renderer.SetAlpha(alpha);
        card_Image_Renderer.SetAlpha(alpha);
        card_Text_Image_Renderer.SetAlpha(alpha);
        card_Grid_Image_Renderer.SetAlpha(alpha);
        card_Type_Image_Renderer.SetAlpha(alpha);


        card_Cost_Text_Renderer.SetAlpha(alpha);
        card_Name_Text_Renderer.SetAlpha(alpha);
        card_Text_Text_Renderer.SetAlpha(alpha);
        card_Type_Text_Renderer.SetAlpha(alpha);
    }

    public void On_Used()
    {
        //카드 사용시 작동되는 부분
    }

    private void On_SetData()
    {

    }

    private void Awake()
    {
        
    }
}
