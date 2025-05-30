using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Blank : MonoBehaviour
{
    public UnityEvent<string> blank;
    GameObject iblank;
    void Start()
    {
        iblank = UIElementManager._instance.GetUIElement("ÎÄ±¾±³¾°¿ò");
        iblank.transform.SetParent(transform);
        iblank.transform.position = transform.position;
        
    }

}
