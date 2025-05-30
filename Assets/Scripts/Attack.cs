using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KnifeState : characterAttack
{

    private CharacterState manager;
    private Tools patameters;
    private float speed = 30f;
    private bool onatk = false;
    private bool canatk = true;
    private Vector2 origion = Vector2.zero;
    private Vector2 mouse = Vector2.zero;
    private float flytime = 0.6f;

    public KnifeState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;
    }
    public void onEnter()
    {
       
        manager.instantination(0);




    }

    public void onExit()
    {
        patameters.CD.SetActive(false);
    }

    public void onUpdate()
    {

        if (Input.GetMouseButton(0)) patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = true;


        if (Input.GetKeyDown(KeyCode.H))
        {
            manager.TransState(AttackType.handsaw);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            manager.TransState(AttackType.chainsaw);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            manager.TransState(AttackType.bottle);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.TransState(AttackType.exit);
        }

        //if (onatk)
        //{
        //    return;
        //}

        if (Input.GetMouseButtonDown(0) && canatk)
        {

            origion = manager.GetComponent<Transform>().position;
            mouse = manager.getmouseposition();
            manager.StartCoroutine(attack(origion,mouse));//��Э�̵��÷���
        }
        else if (!onatk && canatk)
        {
            patameters.CD.GetComponent<cd>().refresh();
            patameters.CD.GetComponent<cd>()._minus = false;
            patameters.CD.SetActive(false);
            patameters.CurrentTool.transform.up = manager.getmouseposition();
            patameters.CurrentTool.transform.position = patameters.character.transform.position;
        }

        if (onatk)
        {
            patameters.CD.SetActive(true);
            patameters.CD.GetComponent<cd>()._minus = true;
        }

        
 
        
    }

    public IEnumerator attack(Vector2 origion, Vector2 mouse)
    {
       
        canatk = false;
        onatk = true;


        patameters.CurrentTool.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //�ɳ�ȥ
        patameters.CurrentTool.GetComponent<Rigidbody2D>().velocity = mouse * speed;
        //�������㹻��ʱ��֮��
        yield return new WaitForSeconds(flytime);
            onatk = false;
            canatk = true;
        if(patameters.CurrentTool != null)
        UnityEngine.Object.Destroy(patameters.CurrentTool);
        manager.instantination(0);
        }

    }   
        
public class handsawState : characterAttack
{
    private CharacterState manager;
    private Tools patameters;

    public handsawState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;
    }
    public void onEnter()
    {
        manager.instantination(1);
    }

    public void onExit()
    {
    }

    public void onUpdate()
    {
        if (Input.GetMouseButton(0)) manager.GetComponent<BoxCollider2D>().enabled = true;
        patameters.CurrentTool.transform.up = manager.getmouseposition();
        if (Input.GetKeyDown(KeyCode.K))
        {
            manager.TransState(AttackType.knife);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            manager.TransState(AttackType.chainsaw);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            manager.TransState(AttackType.bottle);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.TransState(AttackType.exit);
        }
    }
}

public class ChainsawState : characterAttack
{
    private CharacterState manager;
    private Tools patameters;
    private float worktime = 10f;
    private bool canatk = true;
    public ChainsawState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;
    }
    public void onEnter()
    {
        patameters.CD.SetActive(true);
        patameters.CD.GetComponent<cd>().refresh();
        manager.instantination(2);
    }

    public void onExit()
    {
        patameters.CD.SetActive(!patameters.CD.activeSelf);
    }

    public void onUpdate()
    {
        
        patameters.CurrentTool.transform.up = manager.getmouseposition();

        //����Ϊ0ʱ��ʼ��䣬������ǰ�����ܹ���
        if (patameters.CD.GetComponentInChildren<Image>().fillAmount == 0)
           patameters.CD.GetComponent<cd>()._fill = true;

        if (patameters.CD.GetComponentInChildren<Image>().fillAmount == 1)
            patameters.CD.GetComponent<cd>()._fill = false;

     
        //������ȴ��ʱ���ܹ���
        if (patameters.CD.GetComponent<cd>()._fill == false) canatk = true;
        else if (patameters.CD.GetComponent<cd>()._fill == true) canatk = false;

        //��������������������
        if (canatk)
        {

            if (Input.GetMouseButton(0))
            {
                patameters.CD.GetComponent<cd>()._minus = true;
                patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = true;
            }
            else
            {
                patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = false;
                patameters.CD.GetComponent<cd>()._minus = false;
            }

        }
        else
        {
            patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = false;
        }





        if (Input.GetKeyDown(KeyCode.K))
        {
            manager.TransState(AttackType.knife);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            manager.TransState(AttackType.handsaw);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            manager.TransState(AttackType.bottle);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.TransState(AttackType.exit);
        }
    }

}

public class bottleState : characterAttack
{
    private CharacterState manager;
    private Tools patameters;
    private Camera Camera;
    private GameObject newbottle;
    private Vector3 BottlePosition = Vector3.zero;
    public bottleState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;

    }
    public void onEnter()
    {
        Camera = patameters.camera;
       
    }

    public void onExit()
    {
       
        UnityEngine.Object.Destroy(newbottle);
    }

    public void onUpdate()
    {
        patameters.CurrentTool = newbottle;
        if (Input.GetKeyDown(KeyCode.K))
        {
            manager.TransState(AttackType.knife);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            manager.TransState(AttackType.handsaw);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            manager.TransState(AttackType.chainsaw);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.TransState(AttackType.exit);
        }

        if (newbottle == null)
        {
            newbottle = UnityEngine.Object.Instantiate(patameters.toolPools[3],manager.transform);
            newbottle.transform.position=manager.transform.position;

        }
        //������󣬳���귽���ӳ�����Ԥ����һ����귽�����

        if (Input.GetMouseButtonDown(0))
        {
            BottlePosition = manager.getmouseposition();
            newbottle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if (manager.GetComponentInParent<Rigidbody2D>().velocity != Vector2.zero)        // �����ɫ�����˶���������һ���˶�����ļ��ٶ�
            {
                newbottle.GetComponent<Rigidbody2D>().AddForce(manager.GetComponentInParent<Rigidbody2D>().velocity * 30f);
            }

            newbottle.GetComponent<Rigidbody2D>().velocity = BottlePosition * 15f;

            newbottle = null;//��ƿ�Ӷ���ȥ֮����������

        }
    }
}

public class exitState : characterAttack
{
    private CharacterState manager;
    private Tools patameters;

    public exitState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;
    }
    public void onEnter()
    {
        //�������
        patameters.CurrentTool = null;
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            manager.TransState(AttackType.knife);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            manager.TransState(AttackType.handsaw);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            manager.TransState(AttackType.chainsaw);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            manager.TransState(AttackType.bottle);
        }
    }
}