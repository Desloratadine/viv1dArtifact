using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 武器的攻击力
/// </summary>
[Serializable]
public class tools
{
    private GameObject tool;
    public float hurt;
}

/// <summary>
/// 挂在武器预制体上
/// </summary>
public class CombatSystem : MonoBehaviour
{
    public tools _tool;
    public Tools parameter; //获取现在的武器

    public FSM FSM;
    public GameObject target;

    public bool constant = false;   //持续攻击的武器,
    public bool flag=false;

    public GameObject cat;
    public bag bag;
    public float bonus = 1f;    //用来实现毒果的效果

    void Start()
    {
        bag = GameObject.FindWithTag("Player").GetComponent<bag>();
        _tool.hurt *= bag.usepoision;
        Debug.Log("现在的攻击力是"+_tool.hurt);
 
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

    

    //武器碰到敌人后获取敌人组件
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
