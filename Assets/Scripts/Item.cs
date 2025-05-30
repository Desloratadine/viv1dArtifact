using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum effect
{
    体力提升,
    燃烧时间增加,
    机遇增加,
    吸引力增加
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{

    [Header("基础信息")] public string itemName;
    [TextArea] public string itemInfo;//
    [Header("精灵")] public Sprite ItemSprite;

    [Header("基础价值")] public int basicPrice;

    [Header("存在的阶段")]public StatesInfo[] StatesInfo;

    public int itemHeld = 1;//物品的数量，默认是一个，因为拾取第一个后直接为1，再拾取就直接+1即可
}
//场景中会出现的物体的基础属性
[System.Serializable]
public class StatesInfo
{
     public stateName stateName;
    public Sprite stateSprite;

}

[System.Serializable]
public class plants
{
    public GameObject Item;
    public string name
    {
        set { name = Item.GetComponent<AddInventory>().thisItem.name; }
    }
    [Header("出现的层级")] public CubeName CubeName;

}