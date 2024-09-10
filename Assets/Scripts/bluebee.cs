using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 在dile停留resttime后，进入spwalk
/// </summary>
public class BBidle : Enemy
{
    private FSM manager;
    private Parameter parameter;

    public BBidle(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        parameter.animator.Play("bluebee-idle");
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        if (parameter.target != null)
        {
            manager.TransState(StateType.chase);
        }
    }
}

/// <summary>
/// 随机行走
/// 得到范围的临界xy，用随机数生成一个new vector2，然后用movetowards移动
/// 边缘用摄像机的范围坐标，左下00
/// 移动到点之后，进入idle
/// </summary>
public class BBwalk : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private Vector2 patrol;

    private Vector2 LeftDowm;
    private Vector2 RightUp;

    public BBwalk(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
       
    }

    public void onExit()
    {
        patrol = Vector2.zero;
    }

    public void onUpdate()
    {

    }
}
/// <summary>
/// 调用一次函数,1秒后切换到其他状态
/// </summary>
public class BBatk : Enemy
{
    private FSM manager;
    private Parameter parameter;
    bool flag = true;

    public BBatk(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;

    }

    public void onEnter()
    {
        parameter.animator.Play("bluebee-attack");
        


    }

    public void onExit()
    {

    }

    public void onUpdate()
    {

            FireCircle(10, 9);
        if (parameter.target)
        {        
            manager.TransState(StateType.chase);
        }
        else manager.TransState(StateType.idle);

    }

    /// <summary>
    ///Quaternion.Euler(...) 创建一个表示旋转的四元数，这里使用的是欧拉角。该旋转只在 Z 轴上（二维空间中的旋转）。
    ///i* angle + angleOffset 计算出一个新的旋转角度，这个角度是由索引 i、每个子弹之间的角度 angle 和额外的偏移量 angleOffset 组合而成。
    ///m_transform.right 是当前对象的右方向向量（通常是指向对象前方的方向）。
    ///将旋转的四元数应用于 m_transform.right，得到一个新的方向向量 dir。这个方向是经过指定角度旋转后的右侧方向。
    /// </summary>

    public void FireCircle(int bulletCount,float angleOffset)
    {
  

        float angle = 360 / bulletCount;
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = UnityEngine.Object.Instantiate(parameter.bullet,manager.gameObject.transform);
            bullet.GetComponent<bullet>().m_transform.position = manager.gameObject.transform.position;
            Vector3 dir = Quaternion.Euler(new Vector3(0, 0, i * angle + angleOffset)) * bullet.GetComponent<bullet>().m_transform.right;

            bullet.transform.position = bullet.GetComponent<bullet>().m_transform.position;
            bullet.transform.right = dir;
            UnityEngine.Object.Destroy(bullet, 0.6f);

        }
     
    }
}

public class BBhurt : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private ParticleSystem particleSystem;
    private bool flag = false;
    private AnimatorStateInfo info;
    public BBhurt(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        Debug.Log("蜜蜂受伤了");
        parameter.animator.Play("sp-hurt");
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
        manager.TransState(StateType.idle);
    }
}

public class BBchase : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private float timer = 0f;
    public BBchase(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }
    public void onEnter()
    {
        parameter.animator.Play("bluebee-idle");

    }
    public void onExit()
    {
        timer = 0f;
    }

    public void onUpdate()
    {
        timer += Time.deltaTime;
        if (parameter.health <= 0)
        {
            manager.TransState(StateType.die);
        }

        if (parameter.target == null)
        {
            manager.transform.position = new Vector2(manager.transform.position.x, manager.transform.position.y);
            manager.TransState(StateType.idle);

        }
        else if (timer>=1f&&parameter.target)
        {
            manager.TransState(StateType.attack);
        }
        else
            manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.target.position, parameter.speed * Time.deltaTime);


    }

}