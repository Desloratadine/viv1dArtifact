using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings.Switch;
//�໥���ã����д���
public enum CardFunc
{
    ����Ʒ,�ϳɹ���, Կ��, ����,��
}
public static class KeyTypeExtensions//ƥ�书��
{
    public static CardFunc GetMatchingLock(this CardFunc key)
    {
        switch (key)
        {
            case CardFunc.�ϳɹ���: return CardFunc.����Ʒ;
            case CardFunc.Կ��: return CardFunc.����;
            case CardFunc.��: return CardFunc.��;
            default: return CardFunc.��;
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
                    // ���û����̬����
                    GameObject obj = new GameObject("CardEffect");
                    _instance = obj.AddComponent<CardEffect>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }
    public void CheckFunc(Transform Sender,PointerEventData ReceiverData)//���ͷ��ǹ��߿�Ƭʵ�������շ��ǿ����ö���
    {
            if (ReceiverData.pointerCurrentRaycast.gameObject.CompareTag("slot"))//����ǿտ��ۣ���ɾ�嵥�����ʵ��
            {
                Debug.Log("���жϿտ���");
                GameObject Receiver = ReceiverData.pointerCurrentRaycast.gameObject;
                InventoryBag SenderBag = Sender.GetComponentInParent<IventoryMannage>().bag;
                Card SendCard = Sender.GetComponent<AddInventory>().card;
                InventoryBag ReceiverBag = Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>().bag;
                //��ɾ�����嵥
                AddCard(ReceiverBag, SendCard, true);
                RemoveCard(SenderBag, SendCard);
            //���ʵ��
            CardChangeBag(SendCard,Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>());
            //Receiver.transform.parent.parent.GetChild(1).GetComponentInParent<IventoryMannage>().FillSlot(SendCard);
            //ɾ���ɿ�Ƭ
            Destroy(Sender.parent.gameObject);
            }
        else if (ReceiverData.pointerCurrentRaycast.gameObject.GetComponent<AddInventory>().card is Card slot)//��Ƭ���
        {
            //Debug.Log("��Ƭ��������ͷ���"+Sender.transform.name+"  ���շ���"+slot.name);
            Debug.Log(transform.gameObject.name);
            if (Sender.GetComponent<AddInventory>().card.CardFunc.GetMatchingLock() == slot.CardFunc)
            {
                switch (slot.CardFunc)
                {
                    case CardFunc.����:
                        CardUnLock(ReceiverData.pointerCurrentRaycast.gameObject.transform);
                        break;
                    case CardFunc.����Ʒ:
                        CardCompositing(slot, 
                            Sender.GetComponent<AddInventory>().card.BondCard, 
                            Sender.GetComponentInParent<IventoryMannage>().bag, ReceiverData.pointerCurrentRaycast.gameObject.GetComponentInParent<IventoryMannage>().bag);//�������ڵı�����һ������ұ�����
                        //���׻�ȡ��ʲô����
                        //ʵ������ӵ��������ڱ���
                        CardChangeBag(Sender.GetComponent<AddInventory>().card.BondCard, Sender.GetComponentInParent<IventoryMannage>());
                        Destroy(ReceiverData.pointerCurrentRaycast.gameObject);
                        break;
                    default:
                        Debug.Log("δ֪����");
                        break;
                }
                   
                    
            }

        }
        //return CardFunc.��; 
    }
    //����ö�����õ�Ч��
    //public void FinalAction(CardFunc key,Card Sender, Card Receiver,InventoryBag bag)//
    //{
    //    if (key == CardFunc.����Ʒ)
    //    {
    //        //CardCompositing(Receiver, bag);
    //    }
    //    else if (key == CardFunc.����)
    //    {
    //        //CardUnLock()
    //    }
    //}
    public void CardUnLock(Transform card)//��Ҫ�����Ŀ�Ƭʵ������Ƭ���ڵı���
    {
        Debug.Log("����");
        card.GetComponentInChildren<AddInventory>().CanFetch = true;
    }
    public void CardLock(Transform card)//��Ҫ�����Ŀ�Ƭʵ������Ƭ���ڵı���
    {
        card.GetComponentInChildren<AddInventory>().CanFetch = false;
    }
    public void CardCompositing(Card c_1,Card result, InventoryBag  MyBag,InventoryBag OtherBag)//�ϳɵ�ԭ����(����Ʒ)�����
    {
        Debug.Log("����" + c_1.name+"�ϳ���"+result.name);
        IventoryMannage iventoryMannage = new IventoryMannage();
        RemoveCard(OtherBag, c_1);//�Ƴ�����Ʒ
        AddCard(MyBag, result, true);//��ӽ��

    }
    public void CardChangeBag(Card SendCard,IventoryMannage iventoryMannage)
    {
        iventoryMannage.FillSlot(SendCard);
    }
    //��̬�޸ı����嵥������ӵ�ʵ��
    public BagList AddCard(InventoryBag inventoryBag, Card card, bool canmove)
    {
        BagList bagList = new BagList { Card = card, Bind = canmove, Exchangeable = true, Lock = false };
        inventoryBag.bagList.Add(bagList);
        return bagList;
        //GameObject NewCard = Instantiate(card.ItemPrefab);
        //NewCard.transform.parent = transform;//���������transform
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
