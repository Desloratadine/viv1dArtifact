using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// ���ڶԻ�ϵͳ�ĵ���¼�������ѡ�ť��
/// </summary>
public class OptionEvent : MonoBehaviour
{
    public void OnClick()
    {

    }
    void ShowText(string text)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
