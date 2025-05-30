using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    //碰到时获取信息赋值给信息栏
public class GetItemInfo : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Info;
    
    private void OnCollisionEnter2D(Collision2D collision)//可碰撞的道具
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
    /// 直接展示文字
    /// </summary>
    void ShowInfo(string info, TextMeshProUGUI infoBlank)
    {
        if (infoBlank.text == info) return;
        infoBlank.text = info;
    }
    /// <summary>
    /// 打字机效果
    /// </summary>
    void PrintInfo(string info,TextMeshProUGUI infoBlank)
    {
        if(infoBlank.text==info)    return;
       
        infoBlank.text = info;
        Printer.instance.StartPrintText(info, infoBlank);
    }
}
