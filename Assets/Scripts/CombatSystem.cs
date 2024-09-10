using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// �����Ĺ�����
/// </summary>
[Serializable]
public class tools
{
    private GameObject tool;
    public float hurt;
}

/// <summary>
/// ��������Ԥ������
/// </summary>
public class CombatSystem : MonoBehaviour
{
    public tools _tool;
    public Tools parameter; //��ȡ���ڵ�����

    public FSM FSM;
    public GameObject target;

    public bool constant = false;   //��������������,
    public bool flag=false;

    public GameObject cat;
    public bag bag;
    public float bonus = 1f;    //����ʵ�ֶ�����Ч��

    void Start()
    {
        bag = GameObject.FindWithTag("Player").GetComponent<bag>();
        _tool.hurt *= bag.usepoision;
        Debug.Log("���ڵĹ�������"+_tool.hurt);
 
    }

    void Update()
    {

        
        if(target!=null && FSM != null)
        {
            if (gameObject.CompareTag("chainsaw"))
            {
               FSM.Parameter.health -= _tool.hurt * Time.deltaTime;
               FSM.Parameter.hp.fillAmount = FSM.Parameter.health / FSM.Parameter.maxHP;
                FSM.TransState(StateType.hit);
            }
 
        }


    }

    

    //�����������˺��ȡ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FSM = collision.gameObject.GetComponent<FSM>();

        target = collision.gameObject;

        flag = true;

        if (gameObject.CompareTag("knife"))
        {
            knife();           hurt(target,FSM);
        }
        if (gameObject.CompareTag("bullet"))
        {
            hurt(target, FSM);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        FSM = null;

        target = null;


    }


    private void hurt(GameObject target,FSM fSM)
    {

        FSM.Parameter.health -= _tool.hurt;
        FSM.Parameter.hp.fillAmount = FSM.Parameter.health / FSM.Parameter.maxHP;        
        FSM.TransState(StateType.hit);
    }


    private void knife()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }



}
