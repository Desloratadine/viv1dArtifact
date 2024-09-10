using System.Collections;

using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject character;
    public GameObject bottle;
    private float edge = 0.3f;
    private bool flag = false;
    public ParticleSystem Break; //ƿ�������Ч��
    public AudioSource broken;
    private bool GetEnemy = false;
    void Start()
    {
       
    }

 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LimitTime());
        }

        if(flag)
        {
            broken.Play();
            Break.Play();
            flag = false;
        }
    }

    private IEnumerator LimitTime() 
    { 
        if(GetEnemy)
        {
            flag = true;
          yield break;
        }

        else
        {
        yield return new WaitForSeconds(edge);
        flag = true;
        bottle.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);  //Ͷ����ƿ��֮��ƿ����ֱ���˶��ģ��������������ʱ�򴢴�ƿ�ӵ����꣬Ȼ�����ƿ���˶���ʱ��������ﵽ�˼���ֵ��ƿ���Զ�����
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetEnemy = true;
    }

}
