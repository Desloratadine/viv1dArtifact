using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//�ؿ��пɻ�������Դ���ܵ�ģ�� ����������
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
        Debug.Log("��ɫ�������ж�");
        
    }
    public void fankui()
    {
        Debug.Log("��һ���˷���");

    }
    public void changeState()
    {
        Debug.Log("����Ľ׶θı��ˣ�");
    }
}
