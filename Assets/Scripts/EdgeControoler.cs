using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//±ßÔµÅÐ¶¨
public class EdgeControoler : MonoBehaviour
{
    public Transform character;
    public bool vertical;
    public bool horizontal;
    void Start()
    {

    }

    void Update()
    {
        if(vertical)
            transform.position = new Vector2(character.position.x, transform.position.y);
        else if(horizontal)
            transform.position = new Vector2(transform.position.x, character.position.y);
        else if(vertical&&horizontal)
            transform.position = new Vector2(character.position.x, character.position.y);
    }
}
