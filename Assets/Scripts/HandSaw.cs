using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSaw : MonoBehaviour
{
    public bool canAtk;
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAtk)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) canAtk = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) canAtk = false;
    }
}
