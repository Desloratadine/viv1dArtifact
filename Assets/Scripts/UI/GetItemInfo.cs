using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    //����ʱ��ȡ��Ϣ��ֵ����Ϣ��
public class GetItemInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Info;
    
    private void OnCollisionEnter2D(Collision2D collision)//����ײ�ĵ���
    {
        if(collision.gameObject.CompareTag("slot"))
        printinfo(collision);
    }

    void printinfo(Collision2D collision)
    {
        if (!Printer.instance.IsOnPrinting)
        {
            ShowInfo(collision.gameObject.GetComponentInChildren<AddInventory>().thisItem.itemName,Name);
        PrintInfo(collision.gameObject.GetComponentInChildren<AddInventory>().thisItem.itemInfo,Info);
        }
    }


    /// <summary>
    /// ֱ��չʾ����
    /// </summary>
    void ShowInfo(string info, TextMeshProUGUI infoBlank)
    {
        if (infoBlank.text == info) return;
        infoBlank.text = info;
    }
    /// <summary>
    /// ���ֻ�Ч��
    /// </summary>
    void PrintInfo(string info,TextMeshProUGUI infoBlank)
    {
        if(infoBlank.text==info)    return;
       
        infoBlank.text = info;
        Printer.instance.StartPrintText(info, infoBlank);
    }
}
