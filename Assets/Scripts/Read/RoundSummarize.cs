using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 阅读模块的结算，单例模式让NotesMove调用，计数在这里累加
/// 已有经验和等级
/// 存放经验-等级的字典
/// 结算经验
/// </summary>
public class RoundSummarize : MonoBehaviour
{

    public static RoundSummarize instance;
     public int exp = 0;
     public string Level;

    private void Awake()
    {
        instance = this;
    }

    private int perfect;

    public UnityEvent PerfectHit;

    //存放经验-等级的字典
    public static readonly Dictionary<int, string> VocabularyDic = new Dictionary<int, string>
    {
        {0,"Rank1 大字不识"},
        {300,"Rank2 词海漂浮"},
        {800,"Rank3 博文强识"},
        {1800,"Rank4 言灵大师"}

    };
    void Start()
    {
        perfect = 0;
        if(PerfectHit == null) {
            PerfectHit = new UnityEvent();
        }
        PerfectHit.AddListener(CountOn);
    }
    public void CountOn()
    {
        Debug.Log("计数累加");
        perfect++;
    }
    public string UpdateLevel(Dictionary<int, string> dic)
    {
        foreach (var level in dic)
        {
            if (exp >= level.Key)
            {
                Level = level.Value;
                
            }
        }
        return Level;
    }
    //结算经验值
    public void exculateEXP()
    {

    }
}
