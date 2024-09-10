using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ����ֻ�ܶ���ÿ���غϿ�ʼ��ʱ�����Դ浵��¼��������i�����Ѫ����λ�ã��������Ǯ����������Ʒ״̬
/// </summary>

public enum roundName
{
    day1,day2, day3, breakround
}


public class Timer : MonoBehaviour
{
    public static int i = 0;
    public float[] roundlength = { 60f,90f,120f,1f };  //�����غϵļ�ʱ������ֵ
   
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
    //��ʱ
    void count()
    {
        //����ʱ       
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
            Debug.Log("��Ϸ����");
        }

        //����ʱ,��ʼʱ���Ϊ0f
        //currenttime += Time.deltaTime;
        //showtime(currenttime);
        //if ((currenttime >= roundlength[i])&&i<=2 ) 
        //{   
        //     i++;           
        //    ResetTime();
        //    Debug.Log("��ʼ��һ�ּ�ʱ"+i);
        //}
        //else if (i > 2)
        //{
        //    Debug.Log("��Ϸ����");
        //}
    }
    //��ʾʱ��
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
    //ʱ��ﵽһ��ʱ�������ü�ʱ�������¿�ʼ��ʱ
    private void ResetTime()
    {
        currenttime = 0;
    }

}
