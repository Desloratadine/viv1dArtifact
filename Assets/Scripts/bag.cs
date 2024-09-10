using System;

using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ����������ϡ���������֮���Զ���ӵ�������
/// </summary>

public class bag : MonoBehaviour
{
    //������ÿ����λ
    public GameObject[] images;

    public TextMeshProUGUI Name;

    public TextMeshProUGUI description;

    public int num;

    public bool get = false;

    public TextMeshProUGUI preview;
    public TextMeshProUGUI current;
    public static int currentmoney;

    public GameObject slecet;//���һ�ε������Ʒ��

    public static int allmoney = 0;

    public GameObject fruitEvent;   //ʹ��ˮ����ui
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
            Debug.Log("����");
            collision.GetComponent<AddInventory>().thisItem.itemHeld++;
            get = true;
            Sprite sprite =  collision.GetComponent<AddInventory>().thisItem.ItemSprite;
            AddItem(sprite,collision.gameObject);
            Destroy(collision.gameObject);
        }
        
    }



    /// <summary>
    /// ����Ʒ�۵�image�����ֵΪ�񵽵���Ʒ����ť�ϵĿ�������ֵΪʰȡ���������Ʒ��item
    /// </summary>
    private void AddItem(Sprite sprite,GameObject toolInWorld)
    {
  
        for (int i = 0; i < 15; i++)
        {
            if (images[i].GetComponent<Image>().sprite != null) //�ȶ���λ��ͼƬ�������λ��Ϊ�գ����������λ��item ����toolinworld��item����ô��Ʒ��ֵ��1���뿪ѭ��
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


    //�����ť���ȡͬһ����������
    public void SlotOnClick()//ע����Ʒ����¼�
    {
        GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        slecet = thisButton;

        Name.text = thisButton.GetComponent<AddInventory>().thisItem.itemName;

        description.text = thisButton.GetComponent<AddInventory>().thisItem.itemInfo;

        //���ѡ�����ˮ��
        if(thisButton.GetComponent<AddInventory>().thisItem.itemName == "�ܳԵĹ�" || 
            thisButton.GetComponent<AddInventory>().thisItem.itemName == "����" ||
            thisButton.GetComponent<AddInventory>().thisItem.itemName == "��Ǯ��")
        {
            fruitEvent.SetActive(true);
        }
        else fruitEvent.SetActive(false);

    }

    /// <summary>
    /// �����ť�����������Ʒ�ļ�ֵ��������ֱֵ�Ӽ����ܽ�
    /// ���ݵ����ť��item��û���ֵbasicprice��n D50
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

    //���һ�ε������Ʒ ����������ֵ��10��������currenttool��hurt�Ӱٷ�֮�壻
    public void fruiteffect()
    {


        if (slecet.GetComponent<AddInventory>().thisItem.itemName == "�ܳԵĹ�")
        {
            slecet.GetComponent<AddInventory>().thisItem.itemHeld--;
            fruiteft.text = "���������ˣ�����";
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

        else if (slecet.GetComponent<AddInventory>().thisItem.itemName == "����")
        {
            slecet.GetComponent<AddInventory>().thisItem.itemHeld--;
            fruiteft.text = "�����������ˣ�����";
            GameObject.FindWithTag("Player").GetComponent<bag>().usepoision +=0.5f;


        }

        else if (slecet.GetComponent<AddInventory>().thisItem.itemName == "��Ǯ��")
        {
            fruiteft.text = "����������Ǯ��...";

        }
        
    }
}
