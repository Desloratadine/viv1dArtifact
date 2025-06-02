using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPos : MonoBehaviour
{
   //加载场景后初始化主角的坐标，使用后销毁
    void Update()
    {
        if(GameObject.FindWithTag("Player")is GameObject player)
        {
            player.transform.position=this.transform.position;
            Destroy(transform.gameObject);
        }
    }
}
