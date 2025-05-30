using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum effect
{
    ��������,
    ȼ��ʱ������,
    ��������,
    ����������
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{

    [Header("������Ϣ")] public string itemName;
    [TextArea] public string itemInfo;//
    [Header("����")] public Sprite ItemSprite;

    [Header("������ֵ")] public int basicPrice;

    [Header("���ڵĽ׶�")]public StatesInfo[] StatesInfo;

    public int itemHeld = 1;//��Ʒ��������Ĭ����һ������Ϊʰȡ��һ����ֱ��Ϊ1����ʰȡ��ֱ��+1����
}
//�����л���ֵ�����Ļ�������
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
    [Header("���ֵĲ㼶")] public CubeName CubeName;

}