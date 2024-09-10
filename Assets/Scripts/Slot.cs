using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI description;

    public void SlotOnClick()//注册物品点击事件
    {
        Name.text = gameObject.GetComponent<AddInventory>().thisItem.itemName;
        description.text = gameObject.GetComponent<AddInventory>().thisItem.itemInfo;
    }

}

