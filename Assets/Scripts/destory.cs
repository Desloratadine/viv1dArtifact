using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destory : MonoBehaviour
{
    public bool dontDestory;

    void Start()
    {
    }


    void Update()
    {
        
        if(dontDestory) DontDestroyOnLoad(gameObject);
        else if (!dontDestory&&Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
