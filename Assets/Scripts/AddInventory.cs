using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//�ýű��󶨵���Ƭ/ֲ���ʵ��
public class AddInventory : MonoBehaviour
{
    public Item thisItem;//����Ʒע�����Ʒ����һ�����Ʒ
    public Card card;
    //public TextMeshProUGUI cardInfo;
    public GameObject blank;
    //[Header("��Դ��")]public InventoryBag thisBag;
    public bool isattach = false;

    //��ʼ��ʱ���ݱ����嵥��ֵ����Ƭ�໥����Ч������Ҳ������
    [Header("�Ƴ����۵�ʱ���Ƿ�����")] public bool CanDrop;//Ŀǰ����һ�������ϵ�����
    [Header("�Ƿ����ڱ����佻��")] public bool CanFetch;//������Կ�׽���
    [Header("�Ƿ����ڱ������ƶ�λ��")] public bool CanMove;//����ק

    //����¼�,չʾ��Ƭ��Ϣ������ĳ������ͣ
    public void OnClick()
    {
        //cardInfo.transform.SetParent(blank.transform);  
        //cardInfo.text= card.itemInfo;
        //cardInfo.transform.position = blank.transform.GetChild(0).position;
        //Debug.Log(card.itemInfo+card.itemName);
        
    }
    
  
    public string GetName()
    {
        return card.itemName;
    }
    public string GetInfo()
    {
        return card.itemInfo;
    }
}

