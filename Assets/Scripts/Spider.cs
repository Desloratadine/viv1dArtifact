using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ��dileͣ��resttime�󣬽���spwalk
/// </summary>
public class SPidle : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private float timer = 0f;
    private float rest = 3f;

    public SPidle(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        parameter.animator.Play("sp-idle");
    }

    public void onExit()
    {
       timer = 0f;
    }

    public void onUpdate()
    {
        timer += Time.deltaTime;
        if(timer >= rest)
        {
            manager.TransState(StateType.walk);
        }
    }
}

/// <summary>
/// �������
/// �õ���Χ���ٽ�xy�������������һ��new vector2��Ȼ����movetowards�ƶ�
/// ��Ե��������ķ�Χ���꣬����00
/// �ƶ�����֮�󣬽���idle
/// </summary>
public class SPwalk : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private Vector2 patrol ;

    private Vector2 LeftDowm;
    private Vector2 RightUp;

    public SPwalk(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        parameter.animator.Play("sp-walk");
        LeftDowm = Camera.main.ViewportToWorldPoint(new Vector2(0.1f, 0.8f));//����
        RightUp = Camera.main.ViewportToWorldPoint(new Vector2(0.9f, 0.95f));//����
        patrol = new Vector2(Random.Range(LeftDowm.x, RightUp.x), Random.Range(LeftDowm.y, RightUp.y));
    }

    public void onExit()
    {
        patrol = Vector2.zero;
    }

    public void onUpdate()
    {
       
        
        manager.transform.position = Vector2.MoveTowards(manager.transform.position, new Vector2(patrol.x,patrol.y), parameter.speed * Time.deltaTime);
        if((manager.transform.position.x == patrol.x)&&(manager.transform.position.y==patrol.y))
        manager.TransState(StateType.attack);
    }
}
/// <summary>
/// ���Ŷ���������ˮ
/// �ҵ���ң��ҵ�����
/// </summary>
public class SPatk : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private ParticleSystem particleSystem;
    private bool flag = false;
    GameObject poision;
    public SPatk(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
      
    }

    public void onEnter()
    {
        particleSystem = manager.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
   
            manager.TransState(StateType.idle);

    }

    
}

public class SPhurt : Enemy
{
    private FSM manager;
    private Parameter parameter;
    private ParticleSystem particleSystem;
    private bool flag = false;
    private AnimatorStateInfo info;
    public SPhurt(FSM manager)
    {
        this.manager = manager;
        parameter = manager.Parameter;
    }

    public void onEnter()
    {
        Debug.Log("֩��������");
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
        manager.TransState(StateType.walk);
    }
} 
