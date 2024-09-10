using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 读档只能读到每个回合开始的时候，所以存档记录的有索引i，玩家血量，位置，背包里的钱，背包的物品状态
/// </summary>

public enum roundName
{
    day1,day2, day3, breakround
}


public class Timer : MonoBehaviour
{
    public static int i = 0;
    public float[] roundlength = { 60f,90f,120f,1f };  //各个回合的计时器的阈值
   
    public TextMeshProUGUI text;
    public static float currenttime = 0f;

    
    void Start()
    {
      currenttime = roundlength[i];
    }

    
    void Update()
    {
        count();
              
    }
    //计时
    void count()
    {
        //倒计时       
        currenttime -= Time.deltaTime;
            showtime(currenttime);
 if ((currenttime <=0) && i <= 2)
        {
            i++;
            currenttime = roundlength[i];
            SceneManager.LoadScene("horizotalcross 1");

        }
        else if (i > 2)
        {
            {
            SceneManager.LoadScene("te");
                i = 0;
            }
            Debug.Log("游戏结束");
        }

        //正计时,初始时间改为0f
        //currenttime += Time.deltaTime;
        //showtime(currenttime);
        //if ((currenttime >= roundlength[i])&&i<=2 ) 
        //{   
        //     i++;           
        //    ResetTime();
        //    Debug.Log("开始新一轮计时"+i);
        //}
        //else if (i > 2)
        //{
        //    Debug.Log("游戏结束");
        //}
    }
    //显示时间
    void showtime(float time)
    {
        string sec;
        string min;
         if((int)time / 60 == 0) min = "00";
         else min =((int)time/60).ToString("D2" );

        if (time <= 60f) sec = ((int)time).ToString("D2");
        else sec = ((int)time%60).ToString("D2");

        text.text = min+":"+sec;
    }
    //时间达到一定时长后重置计时器，重新开始计时
    private void ResetTime()
    {
        currenttime = 0;
    }

}
