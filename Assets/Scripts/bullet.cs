using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   
    public float Speed = 1;
    //public GameObject ExpFX;//��Ч
    public Rigidbody2D rb;
    public Transform m_transform;

    private void Awake()
    {
        rb.gravityScale = 0;
        rb.drag = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_transform = transform;//�ӵ������λ��
    }

    //�ӵ�һֱ��ǰ�������ƶ�
    protected virtual void Update()
    {
        m_transform.position = m_transform.position + m_transform.right * Speed * Time.deltaTime;
        //m_tansform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    //����
    public void Explode()
    {
        //Destroy(Instantiate(ExpFX, m_transform.position, Quaternion.identity), 2f);//������Ч��2�������
        Destroy(gameObject,0.5f);
    }
}