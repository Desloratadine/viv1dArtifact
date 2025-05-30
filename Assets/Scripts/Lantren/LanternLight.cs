using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//提灯的光效 1.跟随2.根据燃烧时间改变亮度和照明范围 3.根据燃料改变颜色 4.摇曳效果/法线纹理
public class LanternLight : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GameObject.Find("character").transform.position;
    }
}
