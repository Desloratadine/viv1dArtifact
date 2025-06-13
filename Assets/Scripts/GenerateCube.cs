using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.UIElements.UxmlAttributeDescription;

//���й���ؿ� ������
public class GenerateCube : MonoBehaviour
{
   
    [Header("�����еĵؿ�")]public List<GameObject> CubeList = new List<GameObject>();
    public static GenerateCube instance;
    public UnityEvent<Transform> ReachEdge;//CubeTrigger �������
    private bool OnGenerating;//�ж�Э��״̬��bool
    public int Order = 0;//�ؿ�ľ���
    public int GenerateTriggerCount;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        GenerateTriggerCount = 0;//ÿ�ֿ�ʼʱ����ʼ��

        if (ReachEdge == null) 
        {
            ReachEdge = new UnityEvent<Transform>(); 
        }
        ReachEdge.AddListener(genetare);
        //ReachEdge.AddListener(GenerationManager.instance.generate);

        GameObject cube = GameObject.FindGameObjectWithTag("cube");
        if (cube != null && !CubeList.Contains(cube))
        {
            CubeList.Add(cube);
        }
    }
        //��¼�������¼���Ӧ������
    void UpdateTriggerCount()
    {
        GenerateTriggerCount++;
        LevelChecker.instance.NormalCheck(GenerateTriggerCount);
    }

    public void genetare(Transform CubePos)
    {
        
        UpdateTriggerCount();//���߲���+

        Order = CubePos.parent.parent.parent.GetComponentInChildren<SpriteRenderer>().sortingOrder;

        GameObject newcube = 
            Instantiate(LevelChecker.instance.CubesDic[LevelChecker.instance.CurrentLevelName]);//�㼶���ű�����ĵ�ǰ�㼶�Ĳ���

        newcube.transform.position = CubePos.position ;   
        
        CubeList.Add(newcube);      

        RunFadeIn(newcube);
        GenerationManager.instance.GenerateRandomItem(newcube.transform);
    }



    #region ���ڵ��뵭���ķ���
    public void RunFadeIn(GameObject newcube)
    {
        if (OnGenerating) return;
        StartCoroutine(RoomFadedIn(newcube));
    }

    public void RunFadeOut(GameObject newcube)
    {
        StartCoroutine(RoomFadedOut(newcube));
    }


    private IEnumerator RoomFadedIn(GameObject cube)
    {
        OnGenerating = true;
        float timer = 0f;
        float WaitTime = .5f;
        float alpha = 0f;
        while (timer<WaitTime)
        {
            timer += Time.deltaTime;
            alpha = Mathf.Lerp(0, 1, timer / WaitTime);
            
            cube.GetComponentInChildren<SpriteRenderer>().color = new Color(cube.GetComponentInChildren<SpriteRenderer>().color.r,
                cube.GetComponentInChildren<SpriteRenderer>().color.g, cube.GetComponentInChildren<SpriteRenderer>().color.b,alpha);
            yield return null;
            if (cube.GetComponentInChildren<SpriteRenderer>().color.a == 1){ OnGenerating = false; yield break; }
            //if (Input.GetKeyDown(KeyCode.R)) yield break;

        }

        //yield break;
        //yield return new WaitForEndOfFrame();


    }

    
    private IEnumerator RoomFadedOut(GameObject cube)
    {
        float timer = 0f;
        float WaitTime = .5f;
        float alpha = 0f;
        while (alpha != 1)
        {
            timer += Time.deltaTime;
            alpha = timer / WaitTime;

            cube.GetComponentInChildren<SpriteRenderer>().color = new Color(cube.GetComponentInChildren<SpriteRenderer>().color.r,
               cube.GetComponentInChildren<SpriteRenderer>().color.g, cube.GetComponentInChildren<SpriteRenderer>().color.b,alpha);
            yield return null;
            //if (Input.GetKeyDown(KeyCode.R)) yield break;

        }
        yield break;
        //yield return new WaitForEndOfFrame();


    }
    #endregion
}
