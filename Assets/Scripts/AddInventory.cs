using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

//该脚本绑定到卡片/植物的实例
public class AddInventory : MonoBehaviour
{
    public Item thisItem;//给物品注册该物品是哪一类的物品
    public Card card;
    //public TextMeshProUGUI cardInfo;
    public GameObject blank;
    //[Header("资源箱")]public InventoryBag thisBag;
    public bool isattach = false;

    //初始化时根据背包清单赋值，卡片相互作用效果看的也是这里
    [Header("移出卡槽的时候是否会掉落")] public bool CanDrop;//目前就起到一个造型上的作用
    [Header("是否能在背包间交换")] public bool CanFetch;//可以用钥匙解锁
    [Header("是否能在背包内移动位置")] public bool CanMove;//仅拖拽

    //点击事件,展示卡片信息，后面改成鼠标悬停
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

