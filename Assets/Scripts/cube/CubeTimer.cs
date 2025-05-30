using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//ÿ����ƺ�ļ�ʱ��
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
            Debug.Log("��ʼ��ʱ");
            StartCoroutine(showCounting());
            StartCoroutine(CountDowm(time));
        }

    }
    /// <summary>
    /// ��ʱ��֮��-���Ƴ��б�-�����´�����-������
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator CountDowm(float time)
    {
        onCounting = true;

        
        yield return new WaitForSeconds(time);
           // Debug.Log("��ʱ����");
        RemoveCube(transform);
        foreach (GameObject cube in GenerateCube.instance.CubeList)
        {
                //�������ظ��ĵط�����
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
    /// �ؿ����
    /// </summary>
    /// <param name="cube"></param>
    public void RemoveCube(Transform cube)
    {

        GenerateCube.instance.CubeList.Remove(cube.gameObject);
        //������Ҫ������������û�б����ٵĵؿ���и��£����Է�������û���á�Ӧ�������ٵ�ʱ��֪ͨ���������еĵؿ����һ�����������
        //UpdateTriggerState();

    }
}
