using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static UnityEngine.UIElements.UxmlAttributeDescription;

//队列管理地块 ，单例
public class GenerateCube : MonoBehaviour
{
   
    [Header("场景中的地块")]public List<GameObject> CubeList = new List<GameObject>();
    public static GenerateCube instance;
    public UnityEvent<Transform> ReachEdge;//CubeTrigger 触发检测
    private bool OnGenerating;//判断协程状态的bool
    public int Order = 0;//地块的精灵
    public int GenerateTriggerCount;

    private void Awake()
    {
        instance = this;

    }

    void Start()
    {
        GenerateTriggerCount = 0;//每局开始时都初始化

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
        //记录步数（事件响应次数）
    void UpdateTriggerCount()
    {
        GenerateTriggerCount++;
        LevelChecker.instance.NormalCheck(GenerateTriggerCount);
    }

    public void genetare(Transform CubePos)
    {
        
        UpdateTriggerCount();//已走步数+

        Order = CubePos.parent.parent.parent.GetComponentInChildren<SpriteRenderer>().sortingOrder;

        GameObject newcube = 
            Instantiate(LevelChecker.instance.CubesDic[LevelChecker.instance.CurrentLevelName]);//层级检查脚本储存的当前层级的参数

        newcube.transform.position = CubePos.position ;   
        
        CubeList.Add(newcube);      

        RunFadeIn(newcube);
        GenerationManager.instance.GenerateRandomItem(newcube.transform);
    }



    #region 关于淡入淡出的方法
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
