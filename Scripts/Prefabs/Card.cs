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
    //����
    //1.ī���� �����Ͱ� �����Ǹ� ������ ���� ���̵��� �ٲپ���
    //2.ī�� ������ ���� �ý��� ����
    //3.�ĺ��ڸ� ã�Ƽ� ���� ID�� ��밡����


    //ī���� ������Ʈ ���� ��ũ��Ʈ
    //ī�� �����ʹ� �����ͺ��̽� ����



    Game_Manager game_Manager;
    public Image_SO SO;
    public int instanceID;      //ī�� �ĺ���
    public Card_UI data; //���� �� �ڵ� ����
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
        //�ʱ�ȭ
        game_Manager = Game_Manager.instance;           //�����ͺ��̽� ����
        instanceID = gameObject.GetInstanceID();        //�ĺ��� ���
    }
    public void Change_Card()
    {
        //ī�� ������ ����� ����Ǵ� �κ�

        gameObject.name = data.card_name+gameObject.GetInstanceID().ToString();    //Ÿ��Ʋ 
        card_Name_Text.text = data.card_name;    //Ÿ��Ʋ �̸� ����
        card_Cost_Text.text = data.cost.ToString(); //�ڽ�Ʈ ����
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
        //ī�� ���� �۵��Ǵ� �κ�
    }

    private void On_SetData()
    {

    }

    private void Awake()
    {
        
    }
}
