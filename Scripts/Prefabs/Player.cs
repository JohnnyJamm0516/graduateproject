using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //객체 설명
    //플레이어 객체에 연결된 객체
    //데이터 베이스 역할을 하며 플레이어의 자식개체를 지배하는 역할을 함


    //역할
    //1.데이터 변경 반영   
        //데이터는 모두 정적, 즉 참조형태임 그래서 런타임에 반영해주는 부분이 필요함
        //update 함수에 넣어서 그냥 계속 반영되도록 해놓았음 최적화 기찮 머리아픔 이벤트 통해서 하는 방식이 있지만 너무 생산성이 나오지 않음 배제
    //2.피해가 반영되는 부분을 넣을 것.
        //속성
        //상성
        //상태이상
        //방어도



    public Game_Manager game_Manager;
    public Player_Data_UI player_Data;
    public Image player;
    public Sprite player_Source;

    public Image HP_Bar;
    public Sprite HP_Bar_Source;
    public TextMeshProUGUI HP_Text;

    public Image shield;
    public Sprite shield_Source;
    public TextMeshProUGUI shield_Text;


    private List<ActOverTimeModule> aot;
    public string imagePath;

    public Image_SO image_SO;

    private void Awake()
    {
        game_Manager = Game_Manager.instance;
        aot = new();
        Change_Data();
        List<Image_SO_Data> image_List = image_SO.FindSpritesByTheme("Assets/Image/32x32_monsters");
        player_Source = Image_SO.FindSpriteByName(image_List, player_Data.logo);



        //player_Data = game_Manager.player_Data.player_Data[0];
    }

    private void Update()
    {
        Change_Data();
        List<Image_SO_Data> image_List = image_SO.FindSpritesByTheme("Assets/Image/32x32_monsters");
        player_Source = Image_SO.FindSpriteByName(image_List, player_Data.logo);
    }
    private void Change_Data()
    {
        HP_Text.text = $"{player_Data.HP}/{player_Data.maxHP}";
        if (player_Data.HP >= player_Data.maxHP)
        {
            HP_Bar.fillAmount = 1;
        }
        else
        {
            HP_Bar.fillAmount = (float)(player_Data.HP / player_Data.maxHP);
        }

        gameObject.name = player_Data.name;
        player.sprite = player_Source;
        HP_Bar.sprite = HP_Bar_Source;
        shield.sprite = shield_Source;
        shield_Text.text = player_Data.shield.ToString();
        if (player_Data.isPlayer)
        {
            tag = "Player";
        }
        else
        {
            tag = "Enemy";
        }
    }

    //여기 수정해야함
    /*
    public void On_Hit(float damage)
    {
        float cal = damage * 1;    //상태이상 체크후 반영되는 값
        //조건문 구현


        float result = On_HitDamage(cal);
        Debug.Log("받을 데미지 : " + result);
        if(result > 0)
        {
            player_Data.HP -= result;
        }
    }
    public float On_HitDamage(float damage)
    {
        float result = player_Data.shield - damage;
        //방어도 시스템 플레이어에게 구현해야하나 고민
        if (result < 0)
        {
            player_Data.shield = 0;
            return -result;
        }
        else
        {
            player_Data.shield = result;
            return 0;
        }
    }*/
}
