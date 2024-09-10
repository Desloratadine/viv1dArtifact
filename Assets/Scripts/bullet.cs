using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
   
    public float Speed = 1;
    //public GameObject ExpFX;//特效
    public Rigidbody2D rb;
    public Transform m_transform;

    private void Awake()
    {
        rb.gravityScale = 0;
        rb.drag = 0;
        rb.freezeRotation = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        m_transform = transform;//子弹本身的位置
    }

    //子弹一直朝前进方向移动
    protected virtual void Update()
    {
        m_transform.position = m_transform.position + m_transform.right * Speed * Time.deltaTime;
        //m_tansform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    //销毁
    public void Explode()
    {
        //Destroy(Instantiate(ExpFX, m_transform.position, Quaternion.identity), 2f);//创建特效，2秒后销毁
        Destroy(gameObject,0.5f);
    }
}