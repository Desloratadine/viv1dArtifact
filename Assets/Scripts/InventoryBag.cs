using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryBag", menuName = "Inventory/New IventoryBag")]
public class InventoryBag : ScriptableObject
{
    /// <summary>
    /// ��ұ�������Ϊ��������Ʒ��������һ������
    /// </summary>
    public List<Item> bagList = new List<Item>();
}
//����������������ǻ����Խ�������̵걳���ȵ�.......
