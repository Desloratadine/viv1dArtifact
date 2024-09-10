using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showdialog : MonoBehaviour
{
    public GameObject dialog;
    public bool space;
    public bool ischaracter;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        specialDialog();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (ischaracter)
            {
                 space = true;

            }
            else
            {
                dialog.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
 
                dialog.SetActive(false);
            
        }
    }

    void specialDialog()
    {
        if (space)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialog.SetActive(true);
            }

        }
    }
}
