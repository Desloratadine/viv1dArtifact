using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ƵĹ�Ч 1.����2.����ȼ��ʱ��ı����Ⱥ�������Χ 3.����ȼ�ϸı���ɫ 4.ҡҷЧ��/��������
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
