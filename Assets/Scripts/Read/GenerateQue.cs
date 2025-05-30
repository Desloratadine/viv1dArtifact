using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Ķ������������к�ʵ��������

/// </summary>
public class GenerateQue : MonoBehaviour
{
    [SerializeField] private int exp = 0;
    [SerializeField] private string Level;
    [SerializeField] private Queue<GameObject> NoteQueue = new Queue<GameObject>();
    public GameObject[] notes;//��noteԤ����
    [SerializeField] List<Transform> line = new List<Transform>();//��
    public bool RoundStart = false;



    void Start()
    {

        exp = RoundSummarize.instance.exp;
        Level =  RoundSummarize.instance.UpdateLevel(RoundSummarize.VocabularyDic);  //��ʼ���ȼ�

        
        //Debug.Log("base�����������_" +  transform.childCount);
        int lineCount = transform.childCount;
        for (int i = 0; i < lineCount; i++)
        {
            line.Add(transform.GetChild(i));
            //Debug.Log("��ȡbase��������_"+line[i].name);
        }
    }


    void Update()
    {
        if(RoundStart)
        {
            Level =  RoundSummarize.instance.UpdateLevel(RoundSummarize.VocabularyDic);  //���µȼ�
            gene(Level);

            StartCoroutine(NoteOut());          
            RoundSummarize.instance.exculateEXP();//***�ĳ������������ٺ��ٽ��� ���¼���Ӧ
            //NoteQueue.Clear();//���������       
        }
        //if(!TopNote)checkUpdate = true;
    }

// ����һ�ֵ�note����������������������ȼ��й�
    void gene(string VocabularyLevel)
    {
        //note�� = ������+���ȼ�������ϵ��������exp/����ϵ����
        int max = 0;
        foreach (var level in RoundSummarize.VocabularyDic)
        {
            if (VocabularyLevel == level.Value)
            {
                max = 20+(level.Key/10) + (exp/10); // 0��note����60 ̫���ˣ���Ҫ��һ��
                Debug.Log("����note������ "+max);
                break;
            }
        }

        for (int i = 0; i < max; i++)
        {
           //Debug.Log("���-"+i);
           int key = UnityEngine.Random.Range(0, notes.Length);
            NoteQueue.Enqueue(notes[key]);
        }
        RoundStart = false;
    }
    //����read_base���� ������ ���������һ�� note�ٶ�Ҳ���ȼ�����
     private Transform RandomLine()
    {
        int l = UnityEngine.Random.Range(0, line.Count);
        //Debug.Log("note��������_" + line[l].name);
        return line[l];
    }
    //���ӣ�
    void dequeNote(Queue<GameObject> que)
    {

       GameObject note =  que.Dequeue();

        Instantiate(note,RandomLine());

        //Instantiate( que.Dequeue().transform, RandomLine());
    }
    //ѭ����Э�� ���ó���
    private IEnumerator NoteOut()
    {
        while (NoteQueue.Count > 0)
        {

        dequeNote(NoteQueue);

        yield return new WaitForSeconds(1f);
        }
        if (NoteQueue.Count == 0)
        {
            Debug.Log("noteȫ������");
            NoteQueue.Clear();//���������
            RoundStart = false;
            yield break;

        }

    }



}
