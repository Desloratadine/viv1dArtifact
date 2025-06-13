using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

//提灯的光效 1.跟随2.根据燃烧时间改变亮度和照明范围 3.根据燃料改变颜色 4.摇曳效果/法线纹理
public class LanternLight : MonoBehaviour
{
    public static LanternLight instance;
    public UnityEvent ChangeLight;
    Light2D Light;
    float StepLight;//每次减少的亮度
    int stepCount = 20;//玩家实际上改变的是理论步数，理论步数越多，亮度消耗越慢，走得越远
    public float LightIntensityValue; //灯光初始亮度
    bool CanGoHome = true; //是否可以回家

    GameObject character;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if(ChangeLight == null)
        {
            ChangeLight = new UnityEvent();
        }
        ChangeLight.AddListener(LightIntensity);
        Light = GetComponent<Light2D>();
        character = GameObject.Find("character");
        CaculateStep();
    }
    void goAdventure()
    {
        CaculateStep();//每次探险开始前都要计算步数

    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, character.transform.position, Time.deltaTime * 5f);
        //transform.position =character.transform.position;
    }
    public void CaculateStep()
    {

        //int step = 0;
        //step = Light.intensity > 0 ? (int)(Light.intensity * 20) : 0;
        LightIntensityValue = Light.intensity; 
        StepLight = Light.intensity/stepCount;
    }
    public void LightIntensity()//启动光线逐渐减弱的协程，
    {
        if (Light.intensity > 0)
        {
            Light.intensity -= StepLight;
            if(Light.intensity < LightIntensityValue*.3f)
            {
                CanGoHome = false; //亮度小于30%不能回家
            }
        }
        StartCoroutine(HarmonicMotion());
    }
    public IEnumerator  HarmonicMotion()//灯光简谐运动
    {
        float amp=.2f;
        float fre = 5f;
        Vector3 axis = Vector3.right;
        float decay = 2f; // 衰减系数，越大衰减越快
        Vector3 pos=transform.position;
        float time = 0f;
        while (time<=1f)
        {
            time += Time.deltaTime;
            float currentAmp = amp * Mathf.Exp(-decay * time);
            float offset = currentAmp * Mathf.Sin(2 * Mathf.PI * fre * Time.time);
            transform.position = transform.position + axis * offset;
            yield return null;
        }
        yield return null;
    }
}
