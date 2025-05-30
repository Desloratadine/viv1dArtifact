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
    //现在的武器
    public GameObject CurrentTool;
    //武器
    public GameObject[] toolPools;
    //武器位置
    public Transform bottleposition;
    //摄像机
    public GameObject MainCamera;
    public Camera camera;
    //武器的攻击范围
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
        //执行当前状态的内容
        currentstate.onUpdate();
        
    }

       //当前已有状态，退出
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
        //储存新的状态
        currentstate = ATKstates[stateType];
        //进入工作
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

   
    //返回鼠标方位
    public Vector3 getmouseposition()
    {
        Vector3 MouseScreenPosition = Input.mousePosition;
        Vector3 MouseWorldPosition = tools.camera.ScreenToWorldPoint(MouseScreenPosition);
        MouseWorldPosition.z = 0;   //不需要z轴

        Vector3 forcePosition = (MouseWorldPosition - tools.CurrentTool.transform.position).normalized;//受力方向（标准化的）
        return forcePosition;
    }

}
