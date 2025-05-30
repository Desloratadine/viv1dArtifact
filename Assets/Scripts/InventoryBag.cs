using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryBag", menuName = "Inventory/New IventoryBag")]
public class InventoryBag : ScriptableObject
{
    /// <summary>
    /// 玩家背包，因为储存多个物品，所以是一个集合
    /// </summary>
    public List<BagList> bagList = new List<BagList>();

}
[System.Serializable]
public class BagList
{
    public Card Card;
    //这个清单需要提前配置
    [Header("掉落效果")] public bool Bind = false;//绑定，不可掉落
    [Header("需开锁")] public bool Exchangeable = true;//可以在背包之间交换
    [Header("在背包内移动位置")] public bool Lock = false;
    //private void Start()
    //{
    //    Card.CanMove = CanMove;
    //}
}
//基于这样的情况我们还可以进行添加商店背包等等.......
