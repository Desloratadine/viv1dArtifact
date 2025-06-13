using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : MonoBehaviour
{
    public bool IsEnd = false; //是否结束战斗
    //先确认主循环
    void OnBattle()
    {
        // 确认先手后手
        int turn = CheckTurn(); 
        if (turn==0)
        {
            PlayerTurn();
        }
        else if (turn == 1)
        {
            //敌人先手
            EnemyTurn();
        }
        if(IsEnd)
        EndTurn();
    }
    //确认先手后手
    private int CheckTurn()
    {
        int turn =Random.Range(0, 2);
        return turn;
    }
    private void PlayerTurn()//玩家回合
    {

    }
    private void EnemyTurn()//敌人AI
    {

    }
    //回合结算
    private void EndTurn()
    {
        //结算回合

        //检查胜负

        //切换回合
    }
}
