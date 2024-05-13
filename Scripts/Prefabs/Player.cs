using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //��ü ����
    //�÷��̾� ��ü�� ����� ��ü
    //������ ���̽� ������ �ϸ� �÷��̾��� �ڽİ�ü�� �����ϴ� ������ ��


    //����
    //1.������ ���� �ݿ�   
        //�����ʹ� ��� ����, �� ���������� �׷��� ��Ÿ�ӿ� �ݿ����ִ� �κ��� �ʿ���
        //update �Լ��� �־ �׳� ��� �ݿ��ǵ��� �س����� ����ȭ ���� �Ӹ����� �̺�Ʈ ���ؼ� �ϴ� ����� ������ �ʹ� ���꼺�� ������ ���� ����
    //2.���ذ� �ݿ��Ǵ� �κ��� ���� ��.
        //�Ӽ�
        //��
        //�����̻�
        //��



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

    //���� �����ؾ���
    /*
    public void On_Hit(float damage)
    {
        float cal = damage * 1;    //�����̻� üũ�� �ݿ��Ǵ� ��
        //���ǹ� ����


        float result = On_HitDamage(cal);
        Debug.Log("���� ������ : " + result);
        if(result > 0)
        {
            player_Data.HP -= result;
        }
    }
    public float On_HitDamage(float damage)
    {
        float result = player_Data.shield - damage;
        //�� �ý��� �÷��̾�� �����ؾ��ϳ� ���
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
