using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerImage : MonoBehaviour
{
    //�÷��̾�(�� ����)���� ��� �̺�Ʈ�� �߻��� �� üũ�ϴ� �κ�
    //�� ī�� ����ϴ� �κ��̶�� ���� ��
    //���� ����
    //1.��Ʈ ������ �ߵ��ϴ� �κ�
   

    public Player player;       //������ ���̽�
    public bool isPlayer(GameObject cardObject)
    {
        //ī�� ��� �̺�Ʈ �߻��� �۵��� ��� ������ ��
        //cardObjet���� �������� ���·� ���� �� ��


        if (player.player_Data.isPlayer)
        {
            Debug.Log("�÷��̾� ��� �̺�Ʈ �߻�");
        }
        else
        {
            Debug.Log("�� ��� �̺�Ʈ �߻�");
        }
        Card useCard = cardObject.GetComponent<Card>();

        //AttackModule.On_TrueDamage(player, useCard);
        //player.On_Hit(useCard.data.effect);


        return player.player_Data.isPlayer;
    }
}
