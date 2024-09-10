using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueChange : MonoBehaviour
{
    public FSM FSM;
    public Parameter Parameter;
    public float hurt;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FSM = collision.GetComponent<FSM>();
        collision.gameObject.GetComponent<FSM>().Parameter.health -= hurt;
        collision.gameObject.GetComponentInChildren<Image>().fillAmount = Parameter.health/Parameter.maxHP;
    }
}
