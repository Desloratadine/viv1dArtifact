using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Printer : MonoBehaviour
{
    public static Printer instance;
    [SerializeField] float Speed = 10f;
    string currenttext;
    public bool IsOnPrinting = false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartPrintText(string text, TextMeshProUGUI TextBlank)
    {
        if (IsOnPrinting) return;
        else
            StartCoroutine(PrintText(text, TextBlank));
    }

    IEnumerator PrintText(string text,TextMeshProUGUI TextBlank)
    {
        IsOnPrinting = true;
        for(int i = 0; i <= text.Length; i++)
        {
            //Debug.Log("1");
            currenttext = text.Substring(0, i);
            TextBlank.text = currenttext;
            yield return new WaitForSeconds(.05f);//´ò×Ö¼ä¸ô
        }
        IsOnPrinting = false;
        yield return null;


    }
}
