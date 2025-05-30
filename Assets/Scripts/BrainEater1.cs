using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;


/// <summary>
/// ������Ѫ����ɫ����Ѳ�߷�Χ�ṥ��
/// </summary>
/// 
public class idleState : Enemy
{
    private FSM manager;
    private Parameter parameter;
    
    public idleState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        //����
        parameter.animator.Play("be1-idle");
        
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        if ((parameter.target != null))
        {
            manager.TransState(StateType.chase);
        }
    }
}

/// <summary>
/// ������
/// ���򣬱���������ɫ�뿪��Χ��׷��
/// </summary>
public class attackState : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private AnimatorStateInfo info;

    public attackState (FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        parameter.animator.Play("be1-atk");
    }

    public void onExit()
    {

    }

    public void onUpdate()
    {

        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (parameter.gethit)
        {
            manager.TransState(StateType.hit);
        }
        if (info.normalizedTime >= .95f)
        {
           
            manager.TransState(StateType.chase);
        }
    }

}

/// <summary>
/// �������
/// </summary>
public class restState : Enemy
{
    private FSM manager;
    private Parameter parameter;

    public restState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        parameter.animator.Play("be1-idle");
    }

    public void onExit()
    {

    }

    public void onUpdate()
    {
        manager.StopCoroutine(play());
    }

    public IEnumerator play()
    {
        yield return new WaitForSeconds(1f);
        manager.TransState(StateType.idle);

    }
}


/// <summary>
/// ���Ŷ�������������
/// </summary>
public class dieState : Enemy
{
    private FSM manager;
    private Parameter parameter;

    public dieState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        manager.StartCoroutine(play());
        manager.gameObject.SetActive(false);  

    }

    public void onExit()
    {
        throw new NotImplementedException();
    }

    public void onUpdate()
    {
        throw new NotImplementedException();
    }
    public IEnumerator play()
    {
        parameter.animator.Play("be1-hit");
        yield return new WaitForSeconds(0.3f);
    }
}
/// <summary>
/// �ܵ�����
/// </summary>
public class hitState : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private float playtime = 0.5f;
    public hitState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
     


    }

    public void onExit()
    {
        parameter.gethit = false;
    }

    public void onUpdate()
    {
        //��˸
         manager.StartCoroutine(hitflash());
    }
    public IEnumerator hitflash()
    {
        parameter.animator.Play("be1-hit");
        yield return new WaitForSeconds(playtime);
if (parameter.health <= 0)
        {
            manager.TransState(StateType.die);
        }
        else if (parameter.target != null)
        {
            manager.TransState(StateType.chase);
        }
        else manager.TransState(StateType.idle);

    }

}

/// <summary>
/// ����
/// ��ɫ�߽���Χ֮��׷���߳��ص�idle״̬���߽�������Χ֮����빥��
/// </summary>
public class chaseState : Enemy
{
    private FSM manager;
    private Parameter parameter;
    public chaseState(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }
    public void onEnter()
    {
        parameter.animator.Play("be1-vert");
        //parameter.speed += 1f;
    }
    public void onExit()
    {
        //parameter.speed -= 1f;
       // manager.transform.position = new Vector2(manager.transform.position.x, manager.transform.position.y);
    }

    public void onUpdate()
    {
        
        if (parameter.health <= 0)
        {
            manager.TransState(StateType.die);
        }

         if (parameter.target == null)
        {
             manager.transform.position = new Vector2(manager.transform.position.x, manager.transform.position.y);
            manager.TransState(StateType.idle);

        }
        else if (Physics2D.OverlapCircle(manager.transform.position, parameter.attackArea, parameter.targetLayer))
        {
            manager.TransState(StateType.attack);
        }
         else
       manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.target.position, parameter.speed * Time.deltaTime);

        
    }
 
}


