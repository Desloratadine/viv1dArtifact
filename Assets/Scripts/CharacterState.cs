using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public enum AttackType
{
    knife,chainsaw,handsaw,bottle,exit
}

[Serializable]
public class Tools
{
    public Animator animator;
    //���ڵ�����
    public GameObject CurrentTool;
    //����
    public GameObject[] toolPools;
    //����λ��
    public Transform bottleposition;
    //�����
    public GameObject MainCamera;
    public Camera camera;
    //�����Ĺ�����Χ
    public float attackArea;
    public GameObject character;
    //
    public GameObject CD;
}

public class CharacterState : MonoBehaviour
{
    
    private characterAttack currentstate;
    private Dictionary<AttackType, characterAttack> ATKstates = new Dictionary<AttackType, characterAttack>();
    public Tools tools;
    
    void Start()
    {
        
            ATKstates.Add(AttackType.chainsaw, new ChainsawState(this));
        ATKstates.Add(AttackType.knife, new KnifeState(this));
        ATKstates.Add(AttackType.handsaw, new handsawState(this));
        ATKstates.Add(AttackType.bottle, new bottleState(this));
        ATKstates.Add(AttackType.exit, new exitState(this));
        tools.animator = transform.GetComponent<Animator>();

        tools.MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        tools.camera = tools.MainCamera.GetComponent<Camera>();
        tools.character = GameObject.FindGameObjectWithTag("Player");

        tools.CurrentTool = null;
        TransState(AttackType.exit);

    }
    void Update()
    {
        //ִ�е�ǰ״̬������
        currentstate.onUpdate();
        
    }

       //��ǰ����״̬���˳�
    public void TransState(AttackType stateType)
    {
 
        if (currentstate != null)
        {
            currentstate.onExit();
        }
        if (tools.CurrentTool != null)
        {
            Destroy(this.tools.CurrentTool);
            
        }
        //�����µ�״̬
        currentstate = ATKstates[stateType];
        //���빤��
        currentstate.onEnter();
    }

    public void instantination(int index)
    {

        tools.CurrentTool = Instantiate(tools.toolPools[index]);
        tools.CurrentTool.transform.SetParent(transform);
        tools.CurrentTool.transform.position = transform.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,tools.attackArea);

    }

   
    //������귽λ
    public Vector3 getmouseposition()
    {
        Vector3 MouseScreenPosition = Input.mousePosition;
        Vector3 MouseWorldPosition = tools.camera.ScreenToWorldPoint(MouseScreenPosition);
        MouseWorldPosition.z = 0;   //����Ҫz��

        Vector3 forcePosition = (MouseWorldPosition - tools.CurrentTool.transform.position).normalized;//�������򣨱�׼���ģ�
        return forcePosition;
    }

}
