using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
//挂在边缘碰撞箱上 生成地形
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
        //当角色在冲刺，开启触发器检测，并让角色通过。
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isDashing)
        {
            ChangeTriggerState(true);
        }
    }

    //检查队列中存在地块返回该地块:用于检查是否需要生成新地块并且更新碰撞体的触发器状态
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
            UIElementManager._instance.GetUIElement("方向事件").SetActive(true);        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            UIElementManager._instance.GetUIElement("方向事件").SetActive(false);
    }
    //进入碰撞箱检查是否需要生成地块
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().isDashing && !Neighbour_is_Exist())
        {
            
            GenerateCube.instance.ReachEdge.Invoke(CubePos);
        }
        UpdateTriggerState();//冲刺但无事发生的bug
        
    }

    //离开碰撞箱时通知计时器开始计时
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (GenerateCube.instance.CubeList.Count != 1)
        {
            int index = GenerateCube.instance.CubeList.Count - 2;
            GameObject cube = GenerateCube.instance.CubeList[index];

            
            cube.GetComponent<CubeTimer>().StartCountDown();
        }

        //更新触发器状态
        UpdateTriggerState();
            
        //ChangeTriggerState(true);
    }

    //遍历这个地块的6个碰撞体，调用一次检查是否有邻居的函数，返回1
    public void UpdateTriggerState()
    {
        //这里可能报错了空对象调用方法，看
        
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
