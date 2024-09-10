using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TreesEvent : MonoBehaviour
{
    [SerializeField, Header("��������")] public TextMeshProUGUI text;
    [SerializeField, Header("�����¼�")] public string[] trees_event;

    public string eventtext;

    void Start()
    {
        eventtext = trees_event[Random.Range(0, trees_event.Length)];
    }

}
