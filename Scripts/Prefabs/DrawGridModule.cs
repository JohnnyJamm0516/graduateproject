using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DrawGridModule : MonoBehaviour
{
    //���� ī�� ���� ����
    //����
    //1.On_Add_Card
    //  ���� : ���� ī�� ���̿� ī�带 ���ϰ��� ��
    //  1)������ (������(������), ������(���� ���), �׸��� �ε��� ���, ������)
    //  2)�����ε��� ���Ͽ� (������)�� �־ �۵����� �ϵ��� ����
    //  3)���� �ٸ� ī����̿� �߰��� �� ������ ����ؼ� ��밡�� �ϵ������� ��ǥ
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card, GameObject parent, Game_Manager game_Manager)
    {
        //ī�� ������ִ� ������
        //����
        //1.ī��
        //  1)������ ��� ī�� �ν��Ͻ� ����
        //  2)�θ� ����
        //  3)Ȱ��ȭ
        //  4)������ ����
        //  5)���ӸŴ����� ����Ʈ�� ��� �߰� 
        //2.ī�� ���� ���� ���
        //  1)����� �α� ��� (������ ����+����)��� 
        


        Debug.Log($"{card.ToString()}�� {cards_Data.data.Count}�� �����Ǿ����ϴ�");
        if(cards_Data != null)
        {
            for (int i = 0; i < cards_Data.data.Count; i++)
            {
                GameObject obj = Instantiate(card);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(true);
                Card objCard = obj.transform.GetComponent<Card>();
                objCard.data = cards_Data.data[i];
                objCard.data.variableManager = Game_Manager.instance.childObjects[13];
                game_Manager.draw_List.Add(obj);
            }
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card, GameObject parent)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        if(game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data, GameObject card)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        GameObject parent = game_Manager.object_DrawGrid;
        if (game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
    public static void On_Add_Card(Data<Card_UI> cards_Data)
    {
        Game_Manager game_Manager = Game_Manager.instance;
        GameObject parent = game_Manager.object_DrawGrid;
        GameObject card = game_Manager.card_Prefab;
        if (game_Manager is not null)
        {
            On_Add_Card(cards_Data, card, parent, game_Manager);
        }
    }
}
