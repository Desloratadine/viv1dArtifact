using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class knifeState : characterAttack
{

    private CharacterState manager;
    private Tools patameters;
    private float speed = 30f;
    private bool onatk = false;
    private bool canatk = true;
    private Vector2 origion = Vector2.zero;
    private Vector2 mouse = Vector2.zero;

    public knifeState(CharacterState manager)
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

        if (onatk)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && canatk)
        {
            
            origion = manager.GetComponent<Transform>().position;
            mouse = manager.getmouseposition();
            manager.StartCoroutine(attack(origion,mouse));//用协程调用方法
        }
        if (!onatk && canatk)
        {
           patameters.CurrentTool.transform.up = manager.getmouseposition();
            patameters.CurrentTool.transform.position = patameters.character.transform.position;
        }
 
        
    }
    public IEnumerator attack(Vector2 origion, Vector2 mouse)
    {
        canatk = false;
        onatk = true;
        float flytime = 1f;
        patameters.CurrentTool.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //飞出去
        patameters.CurrentTool.GetComponent<Rigidbody2D>().velocity = mouse * speed;
        //飞行了足够的时间之后
        yield return new WaitForSeconds(flytime);
            onatk = false;
            canatk = true;
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

public class chainsawState : characterAttack
{
    private CharacterState manager;
    private Tools patameters;

    public chainsawState(CharacterState manager)
    {
        this.manager = manager;
        patameters = manager.tools;
    }
    public void onEnter()
    {
        manager.instantination(2);
    }

    public void onExit()
    {
        
    }

    public void onUpdate()
    {
        patameters.CurrentTool.transform.up = manager.getmouseposition();

        if (Input.GetMouseButton(0)) patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = true;
        else patameters.CurrentTool.GetComponent<BoxCollider2D>().enabled = false;

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
        //鼠标点击后，朝鼠标方向扔出，给预制体一个鼠标方向的力

        if (Input.GetMouseButtonDown(0))
        {
            BottlePosition = manager.getmouseposition();
            newbottle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            if (manager.GetComponentInParent<Rigidbody2D>().velocity != Vector2.zero)        // 如果角色正在运动，再增加一个运动方向的加速度
            {
                newbottle.GetComponent<Rigidbody2D>().AddForce(manager.GetComponentInParent<Rigidbody2D>().velocity * 30f);
            }

            newbottle.GetComponent<Rigidbody2D>().velocity = BottlePosition * 15f;

            newbottle = null;//把瓶子丢出去之后重新生成

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
        //清除武器
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