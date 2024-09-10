using System;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 挂在玩家身上。碰到道具之后自动添加到背包里
/// </summary>

public class bag : MonoBehaviour
{
    //背包的每个栏位
    public GameObject[] images;

    public TextMeshProUGUI Name;

    public TextMeshProUGUI description;

    public int num;

    public bool get = false;

    public TextMeshProUGUI preview;
    public TextMeshProUGUI current;
    public static int currentmoney;

    public GameObject slecet;//最后一次点击的物品。

    public static int allmoney = 0;

    public GameObject fruitEvent;   //使用水果的ui
    public TextMeshProUGUI fruiteft;
    public float usepoision = 1f;

    public bool special = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        current.text = Convert.ToString(allmoney);
        if (slecet)
        {
            clean();
        }
        if (special) return;
         if(allmoney >= 1000)
            {
                if (!special)
                {
            SceneManager.LoadScene("he");
            allmoney = 0;
                }

            }
            


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("slot"))
        {          
            Debug.Log("道具");
            collision.GetComponent<AddInventory>().thisItem.itemHeld++;
            get = true;
            Sprite sprite =  collision.GetComponent<AddInventory>().thisItem.ItemSprite;
            AddItem(sprite,collision.gameObject);
            Destroy(collision.gameObject);
        }
        
    }



    /// <summary>
    /// 把物品槽的image组件赋值为捡到的物品，按钮上的库存组件赋值为拾取到的这个物品的item
    /// </summary>
    private void AddItem(Sprite sprite,GameObject toolInWorld)
    {
  
        for (int i = 0; i < 15; i++)
        {
            if (images[i].GetComponent<Image>().sprite != null) //比对栏位的图片，如果栏位不为空，并且这个栏位的item 等于toolinworld的item，那么物品数值＋1，离开循环
            {
                if(images[i].GetComponent<AddInventory>().thisItem == toolInWorld.GetComponent<AddInventory>().thisItem)
                {
                    images[i].GetComponent<AddInventory>().thisItem.itemHeld++;

                    images[i].GetComponentInChildren<TextMeshProUGUI>().text = images[i].GetComponent<AddInventory>().thisItem.itemHeld.ToString();

                    break;
                }

            }
            else if (images[i].GetComponent<Image>().sprite == null)
            {
                images[i].gameObject.SetActive(true);

                images[i].GetComponent<AddInventory>().thisItem = toolInWorld.GetComponent<AddInventory>().thisItem;

                images[i].GetComponent<Image>().sprite = images[i].GetComponent<AddInventory>().thisItem.ItemSprite;

                images[i].GetComponentInChildren<TextMeshProUGUI>().text = images[i].GetComponent<AddInventory>().thisItem.itemHeld.ToString();

                break;
             
            }



        }
    }


    //点击按钮会获取同一个物体的组件
    public void SlotOnClick()//注册物品点击事件
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        slecet = thisButton;

        Name.text = thisButton.GetComponent<AddInventory>().thisItem.itemName;

        description.text = thisButton.GetComponent<AddInventory>().thisItem.itemInfo;

        //如果选择的是水果
        if(thisButton.GetComponent<AddInventory>().thisItem.itemName == "能吃的果" || 
            thisButton.GetComponent<AddInventory>().thisItem.itemName == "毒果" ||
            thisButton.GetComponent<AddInventory>().thisItem.itemName == "金钱果")
        {
            fruitEvent.SetActive(true);
        }
        else fruitEvent.SetActive(false);

    }

    /// <summary>
    /// 点击按钮后随机生成物品的价值并将该数值直接加入总金额。
    /// 根据点击按钮的item获得基础值basicprice，n D50
    /// </summary>
    public void Preview()
    {
        int floatprice = UnityEngine.Random.Range(1,50);
        int finalprice = slecet.GetComponent<AddInventory>().thisItem.basicPrice + floatprice;
        slecet.GetComponent<AddInventory>().thisItem.itemHeld--;

        allmoney += finalprice;
        preview.text = Convert.ToString(finalprice) ;
            slecet.GetComponentInChildren<TextMeshProUGUI>().text = slecet.GetComponent<AddInventory>().thisItem.itemHeld.ToString();
    }
    public void clean()
    {
        if(slecet.GetComponent<AddInventory>().thisItem.itemHeld ==0)
        {
            
            slecet.GetComponent<Image>().sprite = null;
            slecet.SetActive(false);
            slecet = null;
        }
    }

    //最后一次点击的物品 健康果生命值加10；毒果让currenttool的hurt加百分之五；
    public void fruiteffect()
    {


        if (slecet.GetComponent<AddInventory>().thisItem.itemName == "能吃的果")
        {
            slecet.GetComponent<AddInventory>().thisItem.itemHeld--;
            fruiteft.text = "体力增加了！！！";
            float health = GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.health;
            if (health >= 90f)
            {
                GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.health = 100f;
                GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.Image.fillAmount =
                    GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.health /
                    GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.maxHP;
            }
            else 
            { 
                health += 10f;
                GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.Image.fillAmount =
    GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.health /
    GameObject.FindWithTag("Player").GetComponent<CharacterController>().hp.maxHP;
            }
        }

        else if (slecet.GetComponent<AddInventory>().thisItem.itemName == "毒果")
        {
            slecet.GetComponent<AddInventory>().thisItem.itemHeld--;
            fruiteft.text = "攻击力增加了！！！";
            GameObject.FindWithTag("Player").GetComponent<bag>().usepoision +=0.5f;


        }

        else if (slecet.GetComponent<AddInventory>().thisItem.itemName == "金钱果")
        {
            fruiteft.text = "还是拿来卖钱吧...";

        }
        
    }
}
