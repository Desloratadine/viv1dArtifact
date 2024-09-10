using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 从地里钻出来后播放动画
/// 自身的生命会逐渐衰减
/// </summary>
public class SFidle : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private AnimatorStateInfo info;

    public SFidle(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }
    public void onEnter()
    {
        parameter.animator.Play("sf-open");
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        info = parameter.animator.GetCurrentAnimatorStateInfo(0);
        if (info.normalizedTime >= .95f)
        {
            
            manager.TransState(StateType.rest);
        }

    }
}
/// <summary>
/// 不会主动攻击，碰到就掉血
/// </summary>
public class SFatk : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private float round;
    public SFatk(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {

        manager.character.GetComponent<Animator>().SetTrigger("gethit");

    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        manager.TransState(StateType.rest);

    }
}
public class SFrest : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private float round;
    private float maxhealth = 100f;
    public SFrest(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        Debug.Log("!!!!");
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        round = parameter.health / maxhealth;
        if (round <= 1f && round > 0.6f)
        {
            parameter.animator.Play("sf-idle1");
        }
        else if (round <= 0.6f && round > 0.3f)
        {
            parameter.animator.Play("sf-idle2");
        }
        else if (round <= 0.3f && round > 0f)
        {
            parameter.animator.Play("sf-idle3");
        }

        if (round <= 0f)
        {
            manager.TransState(StateType.die);
        }
        if (parameter.target)
        {
            manager.TransState(StateType.attack);
        }

    }
}

public class SFhurt: Enemy
{
    private FSM manager;
private Parameter parameter;
private ParticleSystem particleSystem;
private bool flag = false;
private AnimatorStateInfo info;
public SFhurt(FSM manager)
{
    this.manager = manager;
    parameter = manager.Parameter;
}

public void onEnter()
{
    Debug.Log("三文鱼受伤了");
    
}

public void onExit()
{

}

public void onUpdate()
{

    manager.StartCoroutine(play());

}
private IEnumerator play()
{

    yield return new WaitForSeconds(1f);
    manager.TransState(StateType.rest);
}
} 