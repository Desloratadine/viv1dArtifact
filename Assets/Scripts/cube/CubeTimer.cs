using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//每个草坪的计时器
public class CubeTimer : MonoBehaviour
{
    public readonly float time = 5f;
    public bool onCounting = false;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartCountDown()
    {
        if (onCounting) return;
        else
        {
            Debug.Log("开始计时");
            StartCoroutine(showCounting());
            StartCoroutine(CountDowm(time));
        }

    }
    /// <summary>
    /// 计时完之后-》移除列表-》更新触发器-》销毁
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator CountDowm(float time)
    {
        onCounting = true;

        
        yield return new WaitForSeconds(time);
           // Debug.Log("计时结束");
        RemoveCube(transform);
        foreach (GameObject cube in GenerateCube.instance.CubeList)
        {
                //这里有重复的地方，改
                cube.GetComponentInChildren<CubeTrigger>().UpdateTriggerState();
            yield return null;
 
        }

        yield return new WaitForSeconds(.1f);

            Destroy(gameObject);
        
        //}
    }
    IEnumerator showCounting()
    {
        float timer = time;
        float CurrentTime = 0f;
        while (timer-CurrentTime >= 0f)
        {
            CurrentTime += Time.deltaTime;
            GetComponentInChildren<Image>().fillAmount = (timer - CurrentTime) / timer;

        yield return null;
        }
    }
    /// <summary>
    /// 地块出队
    /// </summary>
    /// <param name="cube"></param>
    public void RemoveCube(Transform cube)
    {

        GenerateCube.instance.CubeList.Remove(cube.gameObject);
        //这里需要的是让其他还没有被销毁的地块进行更新，所以放在这里没有用。应该在销毁的时候通知队列里所有的地块调用一次这个方法。
        //UpdateTriggerState();

    }
}
