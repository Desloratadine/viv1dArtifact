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
    //Ѫ��
    public float health;
    public float maxHP;
    //�ܻ��͹���
    public bool gethit;
    public bool onattack;
    //�ƶ��ٶ�
    public float speed;
    //������
    public Animator animator;
    //�ܻ�����
    public Material HitMaterial;
    //Ŀ��
    public Transform target;
    //������Χ
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
        //ִ�е�ǰ״̬������
        currentstate.onUpdate();
        if (Parameter.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void TransState(StateType stateType)
    {
        //��ǰ����״̬���˳�
        if (currentstate != null)
        {
            currentstate.onExit();
        }
        //�����µ�״̬
        currentstate = states[stateType];
        //���빤��
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
    public bool IsInScreen()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        //��ָ�����������ת��Ϊ��������
        Vector3 dir = (Camera.main.transform.position - viewPos).normalized;
        //���������������ľ���
        float dot = Vector3.Dot(Camera.main.transform.forward, dir);
        //������
        return dot > 0 && viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1;
    }
    public bool canFollow()
    {
        //���߼�����ͽ�ɫ֮����û�вݴ�
        bool result;
        result = Physics.Raycast(transform.position, character.transform.position, 1000f, LayerMask.NameToLayer("trees"));
        return result;
    }
}


