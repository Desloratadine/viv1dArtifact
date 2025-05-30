using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// 关于对话系统的点击事件，绑定在选项按钮上
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
