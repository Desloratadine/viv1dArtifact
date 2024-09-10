using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ��dileͣ��resttime�󣬽���spwalk
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
/// �������
/// �õ���Χ���ٽ�xy�������������һ��new vector2��Ȼ����movetowards�ƶ�
/// ��Ե��������ķ�Χ���꣬����00
/// �ƶ�����֮�󣬽���idle
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
/// ����һ�κ���,1����л�������״̬
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
    ///Quaternion.Euler(...) ����һ����ʾ��ת����Ԫ��������ʹ�õ���ŷ���ǡ�����תֻ�� Z ���ϣ���ά�ռ��е���ת����
    ///i* angle + angleOffset �����һ���µ���ת�Ƕȣ�����Ƕ��������� i��ÿ���ӵ�֮��ĽǶ� angle �Ͷ����ƫ���� angleOffset ��϶��ɡ�
    ///m_transform.right �ǵ�ǰ������ҷ���������ͨ����ָ�����ǰ���ķ��򣩡�
    ///����ת����Ԫ��Ӧ���� m_transform.right���õ�һ���µķ������� dir����������Ǿ���ָ���Ƕ���ת����Ҳ෽��
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
        Debug.Log("�۷�������");
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