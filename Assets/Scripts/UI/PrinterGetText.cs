using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PrinterGetText : MonoBehaviour
{
    public string info;
    public bool GetFromInspecter;
    public bool GetFromTMP;
    private void OnEnable()
    {
        //Debug.Log("1");
        if (GetFromInspecter)
            Printer.instance.StartPrintText(info, GetComponentInChildren<TextMeshProUGUI>());
        else if (GetFromTMP)
            Printer.instance.StartPrintText(GetComponentInChildren<TextMeshProUGUI>().text,
                GetComponentInChildren<TextMeshProUGUI>());
        else Debug.Log("Î´Ñ¡¶¨");
    }
    void Start()
    {
        //info = GetComponentInChildren<TextMeshProUGUI>().text;
      
    }

    void Update()
    {

    }

}


