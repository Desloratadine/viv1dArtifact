using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
//���ڱ�Ե��ײ���� ���ɵ���
public class CubeTrigger : MonoBehaviour
{

    private Transform CubePos;

    void Start()
    {
        //UpdateTriggerState();
    }
    private void Update()
    {
        //if (!checkExist()) ChangeTriggerState(false);   
        //����ɫ�ڳ�̣�������������⣬���ý�ɫͨ����
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isDashing)
        {
            ChangeTriggerState(true);
        }
    }

    //�������д��ڵؿ鷵�ظõؿ�:���ڼ���Ƿ���Ҫ�����µؿ鲢�Ҹ�����ײ��Ĵ�����״̬
    public bool Neighbour_is_Exist()
    {
        CubePos = transform.GetChild(0);
        foreach (GameObject cube in GenerateCube.instance.CubeList)
        {
            if (Mathf.Abs(cube.transform.position.x - CubePos.position.x) <= 2f &&
                Mathf.Abs(cube.transform.position.y - CubePos.position.y) <= 2f)
                return true;      
        }
        //Debug.Log("null neighbour");
        return false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            UIElementManager._instance.GetUIElement("�����¼�").SetActive(true);        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            UIElementManager._instance.GetUIElement("�����¼�").SetActive(false);
    }
    //������ײ�����Ƿ���Ҫ���ɵؿ�
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isDashing && !Neighbour_is_Exist())
        {
            
            GenerateCube.instance.ReachEdge.Invoke(CubePos);
        }
        UpdateTriggerState();//��̵����·�����bug
        
    }

    //�뿪��ײ��ʱ֪ͨ��ʱ����ʼ��ʱ
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (GenerateCube.instance.CubeList.Count != 1)
        {
            int index = GenerateCube.instance.CubeList.Count - 2;
            GameObject cube = GenerateCube.instance.CubeList[index];

            
            cube.GetComponent<CubeTimer>().StartCountDown();
        }

        //���´�����״̬
        UpdateTriggerState();
            
        //ChangeTriggerState(true);
    }

    //��������ؿ��6����ײ�壬����һ�μ���Ƿ����ھӵĺ���������1
    public void UpdateTriggerState()
    {
        //������ܱ����˿ն�����÷�������
        
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (!transform.parent.GetChild(i).GetComponent<CubeTrigger>().Neighbour_is_Exist())
                transform.parent.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = false;
            else
                transform.parent.GetChild(i).GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public void ChangeTriggerState(bool state)
    {
        GetComponent<BoxCollider2D>().isTrigger = state;
    }

}
