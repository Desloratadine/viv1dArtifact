//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class InventoryMannage : MonoBehaviour
//{
//    private static InventoryMannage instance;//���óɵ���ģʽ

//    public InventoryBag playerBag;//��ұ���
//    public GameObject slotGrid;//װ����
//    //public Slot slotPrefab;
//    public GameObject emptslot;//��ƷԤ����
//    public TextMeshProUGUI itemInfomation;//��Ʒ������text

//    public List<GameObject> slots = new List<GameObject>();//��Ϊֱ����װ��������18��װ������һ���������洢����������
//    private void Awake()
//    {
//        if (instance != null)
//            Destroy(this);
//        instance = this;
//        itemInfomation.text = "";
//    }

//    private void OnEnable()
//    {
//        RestItem();
//    }

//    /// <summary>
//    /// ÿһ��ˢ�±�����������ݶ�Ҫ��ɾ��ԭ�����е���Ʒ��Ȼ���������������
//    /// ��Ϊ�����Ĳ������������ܴ��Ľ���......
//    /// ������ұ��������������б�������
//    /// </summary>
//    public static void RestItem()
//    {
//        //ɾ�����е�
//        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
//        {
//            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
//            instance.slots.Clear();
//        }

//        //���
//        for (int i = 0; i < instance.playerBag.bagList.Count; i++)
//        {
//            //CreatNewItem(instance.playerBag.bagList[i]);
//            instance.slots.Add(Instantiate(instance.emptslot));//��Ʒ���������Ʒ
//            instance.slots[i].transform.SetParent(instance.slotGrid.transform);//����Ʒ��Ϊ��Ʒ���õ��Ӽ�
//            instance.slots[i].GetComponent<Slot>().slotId = i;//��Ʒ������Ʒ��ų�ʼ��
//            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.playerBag.bagList[i]);//��Ʒ��Ϣ�ĳ�ʼ��
//        }
//    }

//    /// <summary>
//    /// ������Ʒ������Ϣ�ĸ�ֵ
//    /// </summary>
//    /// <param name="info"></param>
//    public static void UpItemInfomation(string info)
//    {
//        instance.itemInfomation.text = info;
//    }
//}

