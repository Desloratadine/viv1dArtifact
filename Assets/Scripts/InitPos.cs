using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPos : MonoBehaviour
{
   //���س������ʼ�����ǵ����꣬ʹ�ú�����
    void Update()
    {
        if(GameObject.FindWithTag("Player")is GameObject player)
        {
            player.transform.position=this.transform.position;
            Destroy(transform.gameObject);
        }
    }
}
