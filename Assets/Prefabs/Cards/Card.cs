using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new card", menuName = "Inventory/New Card")]
public class Card : ScriptableObject
{
    [Header("������Ϣ")] public string itemName;//����
    [TextArea] public string itemInfo;//��ͣ��Ϣ
    [Header("������Ϣ")][TextArea] public string extraInfo;//�������ק����ֵĶ�����Ϣ
    [Header("Ԥ����")] public GameObject ItemPrefab;
    public buff[] buffs;
    [Header("�")] public Card BondCard;
    [Header("����")] public CardFunc CardFunc;
    [Header("������")] public int Held;
    [System.Serializable]
    public class buff
    {
        public effect basicPrice;
        public float plus;
    }

}
