using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings.Switch;
//相互作用，集中处理
public enum CardFunc
{
    消耗品,合成工具, 钥匙, 锁孔,无
}
public static class KeyTypeExtensions//匹配功能
{
    public static CardFunc GetMatchingLock(this CardFunc key)
    {
        switch (key)
        {
            case CardFunc.合成工具: return CardFunc.消耗品;
            case CardFunc.钥匙: return CardFunc.锁孔;
            case CardFunc.无: return CardFunc.无;
            default: return CardFunc.无;
        }
    }
}
public class CardEffect : MonoBehaviour
{
    public CardFunc CardFunc;
    private static CardEffect _instance;

    private void Awake()
    {
    }
    public static CardEffect Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    // 如果没有则动态创建
                    GameObject obj = new GameObject("CardEffect");
                    _instance = obj.AddComponent<CardEffect>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    public void CheckFunc(Transform Sender,PointerEventData ReceiverData)//发送方是工具卡片实例，接收方是可作用对象
    {
            if (ReceiverData.pointerCurrentRaycast.gameObject.CompareTag("slot"))//如果是空卡槽，增删清单，添加实例
            {
                Debug.Log("在判断空卡槽");
                GameObject Receiver = ReceiverData.pointerCurrentRaycast.gameObject;
                InventoryBag SenderBag = Sender.GetComponentInParent<IventoryMannage>().bag;
                Card SendCard = Sender.GetComponent<AddInventory>().card;
                InventoryBag ReceiverBag = Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>().bag;
                //增删背包清单
                AddCard(ReceiverBag, SendCard, true);
                RemoveCard(SenderBag, SendCard);
            //添加实例
            CardChangeBag(SendCard,Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>());
            //Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>().FillSlot(SendCard);
            //删除旧卡片
            Destroy(Sender.parent.gameObject);
            }
        else if (ReceiverData.pointerCurrentRaycast.gameObject.GetComponent<AddInventory>().card is Card slot)//卡片相叠
        {
            //Debug.Log("卡片相叠，发送方："+Sender.transform.name+"  接收方："+slot.name);
            Debug.Log(transform.gameObject.name);
            if (Sender.GetComponent<AddInventory>().card.CardFunc.GetMatchingLock() == slot.CardFunc)
            {
                switch (slot.CardFunc)
                {
                    case CardFunc.锁孔:
                        CardUnLock(ReceiverData.pointerCurrentRaycast.gameObject.transform);
                        break;
                    case CardFunc.消耗品:
                        CardCompositing(slot, 
                            Sender.GetComponent<AddInventory>().card.BondCard, 
                            Sender.GetComponentInParent<IventoryMannage>().bag, ReceiverData.pointerCurrentRaycast.gameObject.GetComponentInParent<IventoryMannage>().bag);//工具所在的背包（一般是玩家背包）
                        //到底获取了什么背包
                        //实例化添加到工具所在背包
                        CardChangeBag(Sender.GetComponent<AddInventory>().card.BondCard, Sender.GetComponentInParent<IventoryMannage>());
                        Destroy(ReceiverData.pointerCurrentRaycast.gameObject);
                        break;
                    default:
                        Debug.Log("未知功能");
                        break;
                }
                   
                    
            }

        }
        //return CardFunc.无; 
    }
    //定制枚举作用的效果
    //public void FinalAction(CardFunc key,Card Sender, Card Receiver,InventoryBag bag)//
    //{
    //    if (key == CardFunc.消耗品)
    //    {
    //        //CardCompositing(Receiver, bag);
    //    }
    //    else if (key == CardFunc.锁孔)
    //    {
    //        //CardUnLock()
    //    }
    //}
    public void CardUnLock(Transform card)//需要解锁的卡片实例，卡片所在的背包
    {
        Debug.Log("解锁");
        card.GetComponentInChildren<AddInventory>().CanFetch = true;
    }
    public void CardLock(Transform card)//需要解锁的卡片实例，卡片所在的背包
    {
        card.GetComponentInChildren<AddInventory>().CanFetch = false;
    }
    public void CardCompositing(Card c_1,Card result, InventoryBag  MyBag,InventoryBag OtherBag)//合成的原材料(消耗品)，结果
    {
        Debug.Log("消耗" + c_1.name+"合成了"+result.name);
        IventoryMannage iventoryMannage = new IventoryMannage();
        RemoveCard(OtherBag, c_1);//移除消耗品
        AddCard(MyBag, result, true);//添加结果

    }
    public void CardChangeBag(Card SendCard,IventoryMannage iventoryMannage)
    {
        iventoryMannage.FillSlot(SendCard);
    }
    //动态修改背包清单，并添加到实例
    public BagList AddCard(InventoryBag inventoryBag, Card card, bool canmove)
    {
        BagList bagList = new BagList { Card = card, Bind = canmove, Exchangeable = true, Lock = false };
        inventoryBag.bagList.Add(bagList);
        return bagList;
        //GameObject NewCard = Instantiate(card.ItemPrefab);
        //NewCard.transform.parent = transform;//不能用这个transform
        //UpdateSlot.Invoke(NewCard);
        //FillSlot(NewCard);
    }
    public void RemoveCard(InventoryBag inventoryBag, Card card)
    {
        for (int i = inventoryBag.bagList.Count - 1; i >= 0; i--)
        {
            if (inventoryBag.bagList[i].Card == card)
            {
                inventoryBag.bagList.RemoveAt(i);
            }
        }

    }
}
