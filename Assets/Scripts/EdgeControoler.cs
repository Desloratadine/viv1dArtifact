using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//±ßÔµÅÐ¶¨
public class EdgeControoler : MonoBehaviour
{
    public Transform target;
    public Transform character;

    public bool vertical;
    public bool horizontal;
    public bool ControlledByPlayer;
    public bool ControlledByOthers;
    public string TargetName;
    public string CharacterName;

    public float distanceX;
    public float distanceY;

    public float rateX = 1f;
    public float rateY = 1f;


    void Start()
    {
        if (ControlledByPlayer)
        {
            target = GameObject.FindWithTag(TargetName).transform;
           
        }
        if (ControlledByOthers)
            character = GameObject.FindWithTag(CharacterName).transform;
    }

    void Update()
    {


    }
    private void FixedUpdate()
    {
        if (vertical && !horizontal)
            transform.position = new Vector2(rateX*character.position.x + distanceX, transform.position.y);

        if (!vertical && horizontal)
            transform.position = new Vector2(transform.position.x, rateY*character.position.y + distanceY);

        if (vertical && horizontal)
            transform.position = new Vector2(rateX*character.position.x + distanceX, rateY*character.position.y + distanceY);

        if(ControlledByPlayer)
        {
            if (target.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =target.GetComponent<Rigidbody2D>().velocity;
            }
            else gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;    
        }
    }
}
