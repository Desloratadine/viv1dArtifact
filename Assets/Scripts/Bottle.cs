using System.Collections;

using UnityEngine;

public class Bottle : MonoBehaviour
{
    public GameObject character;
    public GameObject bottle;
    private float edge = 0.3f;
    private bool flag = false;
    public ParticleSystem Break; //瓶子破碎的效果
    public AudioSource broken;
    private bool GetEnemy = false;
    public GameObject poision;
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
            Vector3 _positon = this.transform.position;
            GameObject newpoision =  Instantiate(poision);
            newpoision.transform.position = _positon;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);  //投掷出瓶子之后，瓶子是直线运动的，所以在鼠标点击的时候储存瓶子的坐标，然后计算瓶子运动的时长，如果达到了极限值，瓶子自动销毁
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetEnemy = true;
    }

}
