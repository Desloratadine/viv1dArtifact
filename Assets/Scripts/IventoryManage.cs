using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//����һ����grid layout group�������� ����/��ձ���
public class IventoryMannage : MonoBehaviour
{
    public InventoryBag bag;//�����嵥
    public GameObject slotUI;
    public List<GameObject> slots;//������,���濨Ƭʵ��
    public int maxIvento;//�������������

    public TextMeshProUGUI cardInfo;
    public GameObject blank;


    //private void GetSlots(GameObject ui)
    //{
    //    Transform[] tr = ui.GetComponentsInChildren<Transform>();
    //    foreach (var transform in tr)
    //    {
    //        if (transform == tr[0]) continue;
    //        slots.Add(transform.gameObject);
    //    }
    //}
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        if(bag)
        UpdateBag(bag);
    }
    public void CleanBag()
    {
        if (slots.Count != 0)
        {
            for(int i = slots.Count - 1; i >= 0; i--)
            {
                GameObject item = slots[i];
                slots.Remove(item);
                Destroy(item);
            }
        }
        slots.Clear();

    }
    public void UpdateBag(InventoryBag inventoryBag)//��ȡ����е���Ϣ���±���
    {
        CleanBag();
        for (int i=0; i < inventoryBag.bagList.Count; i++)//���������嵥
        {
//�����߱������嵥����ʵ���Ƿ�󶨣���ʵ���ϵ����Ը�ֵ���������ط���Ҫ�������ʱֻ���ʵ��
            GameObject card = Instantiate(inventoryBag.bagList[i].Card.ItemPrefab);
            card.GetComponentInChildren<AddInventory>().CanDrop= !inventoryBag.bagList[i].Bind;//�Ƴ����۵�ʱ���Ƿ�����
            card.GetComponentInChildren<AddInventory>().CanFetch = inventoryBag.bagList[i].Exchangeable;//�Ƿ����ڱ����佻��
            card.GetComponentInChildren<AddInventory>().CanMove = !inventoryBag.bagList[i].Lock;
            card.transform.parent = transform;
            slots.Add(card);

        }
        bag = inventoryBag;
    }
    public void FillSlot(Card card)
    {
        GameObject NewCard = Instantiate(card.ItemPrefab);
        NewCard.transform.parent = transform;
        slots.Add(NewCard);
        UpdateBag(bag);
    }
    ////��̬�޸ı����嵥������ӵ�ʵ��
    //public void AddCard(InventoryBag inventoryBag,Card card,bool canmove)
    //{
    //    inventoryBag.bagList.Add(new BagList { Card=card,Bind=canmove});

    //    GameObject NewCard = Instantiate(card.ItemPrefab);
    //    NewCard.transform.parent = transform;

    //    FillSlot(NewCard);
    //}
    //public void RemoveCard(InventoryBag inventoryBag,Card card)
    //{
    //    for (int i = inventoryBag.bagList.Count - 1; i >= 0; i--)
    //    {
    //        if (inventoryBag.bagList[i].Card == card)
    //        {
    //            inventoryBag.bagList.RemoveAt(i);
    //        }
    //    }

    //}
}
