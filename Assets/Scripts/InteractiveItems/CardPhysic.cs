using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardPhysic : MonoBehaviour
{
    public UnityEvent<Transform,bool> CardDrop;
    private static CardPhysic _instance;

    public static CardPhysic Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    // ���û����̬����
                    GameObject obj = new GameObject("CardPhysic");
                    _instance = obj.AddComponent<CardPhysic>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
           // DontDestroyOnLoad(gameObject); // ��ѡ���糡��������
        }
        else
        {
            Destroy(gameObject); // �����ظ�ʵ��
        }

        if (CardDrop == null)
            CardDrop = new UnityEvent<Transform, bool>();
    }
    void Start()
    {
        if (CardDrop == null)
            CardDrop = new UnityEvent<Transform,bool>();
        CardDrop.AddListener(ChangePhysicState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePhysicState(Transform card,bool CanDrop)
    {
        if(!CanDrop)
        card.GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        else
        {
            card.GetComponentInChildren<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            card.GetComponentInChildren<Rigidbody2D>().gravityScale = 300;
        }
            
    }
}
