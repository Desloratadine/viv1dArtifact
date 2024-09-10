using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreesEvent : MonoBehaviour
{
    [SerializeField, Header("树的文字")] public TextMeshProUGUI text;
    [SerializeField, Header("树的事件")] public string[] trees_event;

    public string eventtext;

    void Start()
    {
        eventtext = trees_event[Random.Range(0, trees_event.Length)];
    }

}
