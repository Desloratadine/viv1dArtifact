using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//加载场景（暂时）
public class asyLoad : MonoBehaviour
{
    bool flag = true;
    public string CubeName;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //GenerateCube.instance.ReachEdge.Invoke();
        if (flag)
        {
            SceneManager.LoadScene(CubeName);
            //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(CubeName,LoadSceneMode.Additive);
            flag = false;
        }

        //        回调
        //asyncLoad.completed += (AsyncOperation) =>
        //{

        //}
    }
}
