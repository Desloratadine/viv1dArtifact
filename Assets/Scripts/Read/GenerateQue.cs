using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 阅读生成音符队列和实例化音符

/// </summary>
public class GenerateQue : MonoBehaviour
{
    [SerializeField] private int exp = 0;
    [SerializeField] private string Level;
    [SerializeField] private Queue<GameObject> NoteQueue = new Queue<GameObject>();
    public GameObject[] notes;//放note预制体
    [SerializeField] List<Transform> line = new List<Transform>();//行
    public bool RoundStart = false;



    void Start()
    {

        exp = RoundSummarize.instance.exp;
        Level =  RoundSummarize.instance.UpdateLevel(RoundSummarize.VocabularyDic);  //初始化等级

        
        //Debug.Log("base下子物体个数_" +  transform.childCount);
        int lineCount = transform.childCount;
        for (int i = 0; i < lineCount; i++)
        {
            line.Add(transform.GetChild(i));
            //Debug.Log("获取base下子物体_"+line[i].name);
        }
    }


    void Update()
    {
        if(RoundStart)
        {
            Level =  RoundSummarize.instance.UpdateLevel(RoundSummarize.VocabularyDic);  //更新等级
            gene(Level);

            StartCoroutine(NoteOut());          
            RoundSummarize.instance.exculateEXP();//***改成所有音符销毁后再结算 用事件响应
            //NoteQueue.Clear();//结束后清除       
        }
        //if(!TopNote)checkUpdate = true;
    }

// 生成一局的note数量，种类随机，个数跟等级有关
    void gene(string VocabularyLevel)
    {
        //note数 = 基础数+（等级×增量系数）＋（exp/增量系数）
        int max = 0;
        foreach (var level in RoundSummarize.VocabularyDic)
        {
            if (VocabularyLevel == level.Value)
            {
                max = 20+(level.Key/10) + (exp/10); // 0级note数：60 太多了，需要改一下
                Debug.Log("生成note数量： "+max);
                break;
            }
        }

        for (int i = 0; i < max; i++)
        {
           //Debug.Log("入队-"+i);
           int key = UnityEngine.Random.Range(0, notes.Length);
            NoteQueue.Enqueue(notes[key]);
        }
        RoundStart = false;
    }
    //挂在read_base下面 子物体 随机出现在一行 note速度也按等级决定
     private Transform RandomLine()
    {
        int l = UnityEngine.Random.Range(0, line.Count);
        //Debug.Log("note生成在行_" + line[l].name);
        return line[l];
    }
    //出队，
    void dequeNote(Queue<GameObject> que)
    {

       GameObject note =  que.Dequeue();

        Instantiate(note,RandomLine());

        //Instantiate( que.Dequeue().transform, RandomLine());
    }
    //循环＋协程 调用出队
    private IEnumerator NoteOut()
    {
        while (NoteQueue.Count > 0)
        {

        dequeNote(NoteQueue);

        yield return new WaitForSeconds(1f);
        }
        if (NoteQueue.Count == 0)
        {
            Debug.Log("note全部出队");
            NoteQueue.Clear();//结束后清除
            RoundStart = false;
            yield break;

        }

    }



}
