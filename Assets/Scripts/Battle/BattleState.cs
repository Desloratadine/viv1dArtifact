using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    public bool IsEnd = false; //�Ƿ����ս��
    //��ȷ����ѭ��
    void OnBattle()
    {
        // ȷ�����ֺ���
        int turn = CheckTurn(); 
        if (turn==0)
        {
            PlayerTurn();
        }
        else if (turn == 1)
        {
            //��������
            EnemyTurn();
        }
        if(IsEnd)
        EndTurn();
    }
    //ȷ�����ֺ���
    private int CheckTurn()
    {
        int turn =Random.Range(0, 2);
        return turn;
    }
    private void PlayerTurn()//��һغ�
    {

    }
    private void EnemyTurn()//����AI
    {

    }
    //�غϽ���
    private void EndTurn()
    {
        //����غ�

        //���ʤ��

        //�л��غ�
    }
}
