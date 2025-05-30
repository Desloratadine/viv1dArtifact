using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InventoryBag", menuName = "Inventory/New IventoryBag")]
public class InventoryBag : ScriptableObject
{
    /// <summary>
    /// ��ұ�������Ϊ��������Ʒ��������һ������
    /// </summary>
    public List<BagList> bagList = new List<BagList>();

}
[System.Serializable]
public class BagList
{
    public Card Card;
    //����嵥��Ҫ��ǰ����
    [Header("����Ч��")] public bool Bind = false;//�󶨣����ɵ���
    [Header("�迪��")] public bool Exchangeable = true;//�����ڱ���֮�佻��
    [Header("�ڱ������ƶ�λ��")] public bool Lock = false;
    //private void Start()
    //{
    //    Card.CanMove = CanMove;
    //}
}
//����������������ǻ����Խ�������̵걳���ȵ�.......
