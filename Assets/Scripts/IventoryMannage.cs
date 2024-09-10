//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class InventoryMannage : MonoBehaviour
//{
//    private static InventoryMannage instance;//设置成单例模式

//    public InventoryBag playerBag;//玩家背包
//    public GameObject slotGrid;//装备栏
//    //public Slot slotPrefab;
//    public GameObject emptslot;//物品预制体
//    public TextMeshProUGUI itemInfomation;//物品描述的text

//    public List<GameObject> slots = new List<GameObject>();//因为直接在装备栏生成18个装备，用一个集合来存储，并标记序号
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
//    /// 每一次刷新背包里面的内容都要先删除原先所有的物品，然后再重新生成添加
//    /// 因为这样的操作所以是性能大大的降低......
//    /// 根据玩家背包的数量来进行遍历操作
//    /// </summary>
//    public static void RestItem()
//    {
//        //删除所有的
//        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
//        {
//            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
//            instance.slots.Clear();
//        }

//        //添加
//        for (int i = 0; i < instance.playerBag.bagList.Count; i++)
//        {
//            //CreatNewItem(instance.playerBag.bagList[i]);
//            instance.slots.Add(Instantiate(instance.emptslot));//物品集合添加物品
//            instance.slots[i].transform.SetParent(instance.slotGrid.transform);//让物品成为物品栏得到子集
//            instance.slots[i].GetComponent<Slot>().slotId = i;//物品栏的物品序号初始化
//            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.playerBag.bagList[i]);//物品信息的初始化
//        }
//    }

//    /// <summary>
//    /// 进行物品描述信息的赋值
//    /// </summary>
//    /// <param name="info"></param>
//    public static void UpItemInfomation(string info)
//    {
//        instance.itemInfomation.text = info;
//    }
//}

