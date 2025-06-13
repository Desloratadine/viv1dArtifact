using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CubeEvent : MonoBehaviour
{
    public static CubeEvent instance;
    // Start is called before the first frame update
    public List<string> CubeEventList = new List<string>()
    {
        "好的预感",
        "坏的预感",
        "幸运",
        "不幸",

    };
    public List<string> mEvent;
    public UnityEvent OnGenerating;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        if (OnGenerating == null)
        {
            OnGenerating = new UnityEvent();
        }
        OnGenerating.AddListener(GetRandomEvent);

    }
    void Start()
    {


    }

    void Update()
    {
        
    }
    public void GetRandomEvent()
    {
        mEvent.Clear();
        for (int i = 0; i < 7; i++)//随机生成事件
        {
        int index = Random.Range(0, CubeEventList.Count);
            mEvent.Add(CubeEventList[index]);
        }
    }
}
