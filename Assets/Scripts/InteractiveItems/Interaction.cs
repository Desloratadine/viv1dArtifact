using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//地块中可互动的资源功能的模拟 挂在物体上
public class Interaction : MonoBehaviour
{

    public bool CanCheck;
    Ifsm ThisIfsm;
    private void Awake()
    {

    }
    void Start()
    {
        //Change.AddListener(changeState);

    }


    void Update()
    {
        if (CanCheck&&Input.GetKeyDown(KeyCode.R))
        {
            CallChangeState(transform.gameObject);
        }
           
    }
    void CallChangeState(GameObject c)
    {
        c.GetComponent<Ifsm>().SetState();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CanCheck = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
         CanCheck = false;       
    }
    public void action()
    {
        Debug.Log("角色做出了行动");
        
    }
    public void fankui()
    {
        Debug.Log("玩家获得了反馈");

    }
    public void changeState()
    {
        Debug.Log("物体的阶段改变了！");
    }
}
