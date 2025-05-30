using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hide : MonoBehaviour
{
    private bool isin;
    public BoxCollider2D cat;
    public float area;
    private void Awake()
    {
        cat = GameObject.FindWithTag("Player").GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (isin)            cat.enabled = false;
        else    cat.enabled = true;

        float distance = Mathf.Abs(Vector2.Distance(transform.position, cat.transform.position));
        if (distance <= area)
        {

            isin = true;
        }

        else
        {
 
            isin = false;
        }

        HideInGrass();
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(gameObject.GetComponent<Animation>() != null) gameObject.GetComponent<Animation>().Play();
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (gameObject.GetComponent<Animation>() != null) gameObject.GetComponent<Animation>().Play();
    //}
    void HideInGrass()
    {
        
        if (isin)
        {

            cat.enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
        }
        else if(!isin)
        {

            cat.enabled = true;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, area);

    }
}
