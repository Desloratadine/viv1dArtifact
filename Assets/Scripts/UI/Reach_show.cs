using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 在主角以外的角色上 靠近时显示可互动物体的标识
/// </summary>
public class Reach_show : MonoBehaviour
{
    public GameObject image;//显示信息的框框
    public string NPCname;
    //Ray ray;
    //RaycastHit2D hit;
    //public GameObject infoBlank;    //显示名字的
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))        
        {
            GameObject blank = UIElementManager._instance.GetUIElement("对话框");
            blank.GetComponentInChildren<DialogManager>().ReceiveNPC(NPCname);//对话事件入口
            ChangeImagestate(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            GameObject blank = UIElementManager._instance.GetUIElement("对话框");
            blank.GetComponentInChildren<DialogManager>().EnterChapter(blank.GetComponentInChildren<DialogManager>().FindGraph("default"));
        if (collision.gameObject.CompareTag("Player"))
        {

            ChangeImagestate(false);

        }
    }

    void ChangeImagestate(bool flag)//显示框框+通知可以展示二级界面
    {

        image = UIElementManager._instance.GetUIElement("冒泡");
        image.transform.position = transform.position;
        image.SetActive(flag);
        if (transform.GetComponent<AddBag>() is AddBag Bag)//先判断一下有没有Bag
        {
            GameObject bag = UIElementManager._instance.GetUIElement("宝箱背包");
            bag.SetActive(flag);
            bag.transform.Find("slot/empty_slot").GetComponent<IventoryMannage>().UpdateBag(Bag.GetBag());
        }

        SwitchTo2ndUI.instance.canSwitchTo2nd = flag;
        SwitchTo2ndUI.instance.canSwitch.Invoke(transform.position);

    }
}
