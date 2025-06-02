using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// ����������Ľ�ɫ�� ����ʱ��ʾ�ɻ�������ı�ʶ
/// </summary>
public class Reach_show : MonoBehaviour
{
    public GameObject image;//��ʾ��Ϣ�Ŀ��
    public string NPCname;
    //Ray ray;
    //RaycastHit2D hit;
    //public GameObject infoBlank;    //��ʾ���ֵ�
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))        
        {
            GameObject blank = UIElementManager._instance.GetUIElement("�Ի���");
            blank.GetComponentInChildren<DialogManager>().ReceiveNPC(NPCname);//�Ի��¼����
            ChangeImagestate(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
            GameObject blank = UIElementManager._instance.GetUIElement("�Ի���");
            blank.GetComponentInChildren<DialogManager>().EnterChapter(blank.GetComponentInChildren<DialogManager>().FindGraph("default"));
        if (collision.gameObject.CompareTag("Player"))
        {

            ChangeImagestate(false);

        }
    }

    void ChangeImagestate(bool flag)//��ʾ���+֪ͨ����չʾ��������
    {

        image = UIElementManager._instance.GetUIElement("ð��");
        image.transform.position = transform.position;
        image.SetActive(flag);
        if (transform.GetComponent<AddBag>() is AddBag Bag)//���ж�һ����û��Bag
        {
            GameObject bag = UIElementManager._instance.GetUIElement("���䱳��");
            bag.SetActive(flag);
            bag.transform.Find("slot/empty_slot").GetComponent<IventoryMannage>().UpdateBag(Bag.GetBag());
        }

        SwitchTo2ndUI.instance.canSwitchTo2nd = flag;
        SwitchTo2ndUI.instance.canSwitch.Invoke(transform.position);

    }
}
