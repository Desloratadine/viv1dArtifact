using System;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public enum StateType
{
    attack, rest, die, animation, idle, hit, chase, walk
}

[Serializable]
public class Parameter
{
    //血量
    public float health;
    public float maxHP;
    //受击和攻击
    public bool gethit;
    public bool onattack;
    //移动速度
    public float speed;
    //动画器
    public Animator animator;
    //受击材质
    public Material HitMaterial;
    //目标
    public Transform target;
    //攻击范围
    public float attackArea;
    //
    public LayerMask targetLayer;
    //
    public Image hp;
    public float attack;
    //
    public GameObject bullet;
}


public class FSM : MonoBehaviour
{
    private Enemy currentstate;
    private Dictionary<StateType, Enemy> states = new Dictionary<StateType, Enemy>();
    public Parameter Parameter;
    public GameObject character;

    void Start()
    {
        if (this.CompareTag("BE"))
        {
            states.Add(StateType.attack, new attackState(this));
            states.Add(StateType.rest, new restState(this));
            states.Add(StateType.die, new dieState(this));
            states.Add(StateType.idle, new idleState(this));
            states.Add(StateType.hit, new hitState(this));
            states.Add(StateType.chase, new chaseState(this));
        }
        if (this.CompareTag("SP"))
        {
            states.Add(StateType.attack, new SPatk(this));
            states.Add(StateType.idle, new SPidle(this));
            states.Add(StateType.walk, new SPwalk(this));
            states.Add(StateType.hit, new SPhurt(this));
        }
        if (this.CompareTag("SF"))
        {
            states.Add(StateType.idle, new SFidle(this));
            states.Add(StateType.rest, new SFrest(this));
            states.Add(StateType.attack, new SFatk(this));
            states.Add(StateType.hit, new SFhurt(this));
        }
        if (this.CompareTag("bluebee"))
        {
            states.Add(StateType.idle, new BBidle(this));;
            states.Add(StateType.attack, new BBatk(this));
            states.Add(StateType.hit, new BBhurt(this));
            states.Add(StateType.chase, new BBchase(this));
        }
        character = GameObject.FindWithTag("Player");
        Parameter.hp = GetComponentInChildren<Image>();
        Parameter.animator = transform.GetComponent<Animator>();
        TransState(StateType.idle);

    }
    void Update()
    {
        //执行当前状态的内容
        currentstate.onUpdate();
        if (Parameter.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void TransState(StateType stateType)
    {
        //当前已有状态，退出
        if (currentstate != null)
        {
            currentstate.onExit();
        }
        //储存新的状态
        currentstate = states[stateType];
        //进入工作
        currentstate.onEnter();
    }
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Parameter.target = collision.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Parameter.target = null;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
        character.GetComponent<Animator>().SetTrigger("gethit");
            
        }


    }
 


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Parameter.attackArea);

    }
    public void health()
    {
        //GetComponentInChildren<Image>().fillAmount = Parameter.currentHP / Parameter.maxHP;
    }
}


