using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
//绑定在一个有grid layout group的物体下 更新/清空背包
public class IventoryMannage : MonoBehaviour
{
    public InventoryBag bag;//背包清单
    public GameObject slotUI;
    public List<GameObject> slots;//子物体,储存卡片实例
    public int maxIvento;//背包的最大容量

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
    public void UpdateBag(InventoryBag inventoryBag)//获取库存中的信息更新背包
    {
        CleanBag();
        for (int i=0; i < inventoryBag.bagList.Count; i++)//遍历背包清单
        {
//持有者背包的清单决定实例是否绑定，给实例上的属性赋值，在其他地方需要检查属性时只检查实例
            GameObject card = Instantiate(inventoryBag.bagList[i].Card.ItemPrefab);
            card.GetComponentInChildren<AddInventory>().CanDrop= !inventoryBag.bagList[i].Bind;//移出卡槽的时候是否会掉落
            card.GetComponentInChildren<AddInventory>().CanFetch = inventoryBag.bagList[i].Exchangeable;//是否能在背包间交换
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
    ////动态修改背包清单，并添加到实例
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
