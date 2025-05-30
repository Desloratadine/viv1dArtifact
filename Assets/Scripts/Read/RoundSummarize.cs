using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �Ķ�ģ��Ľ��㣬����ģʽ��NotesMove���ã������������ۼ�
/// ���о���͵ȼ�
/// ��ž���-�ȼ����ֵ�
/// ���㾭��
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

    //��ž���-�ȼ����ֵ�
    public static readonly Dictionary<int, string> VocabularyDic = new Dictionary<int, string>
    {
        {0,"Rank1 ���ֲ�ʶ"},
        {300,"Rank2 �ʺ�Ư��"},
        {800,"Rank3 ����ǿʶ"},
        {1800,"Rank4 �����ʦ"}

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
        Debug.Log("�����ۼ�");
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
    //���㾭��ֵ
    public void exculateEXP()
    {

    }
}
