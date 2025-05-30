using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new card", menuName = "Inventory/New Card")]
public class Card : ScriptableObject
{
    [Header("基础信息")] public string itemName;//名字
    [TextArea] public string itemInfo;//悬停信息
    [Header("特殊信息")][TextArea] public string extraInfo;//点击、拖拽会出现的额外信息
    [Header("预制体")] public GameObject ItemPrefab;
    public buff[] buffs;
    [Header("羁绊卡")] public Card BondCard;
    [Header("作用")] public CardFunc CardFunc;
    [Header("持有数")] public int Held;
    [System.Serializable]
    public class buff
    {
        public effect basicPrice;
        public float plus;
    }

}
