using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerationManager : MonoBehaviour
{


    public static GenerationManager instance;
    [Header("地块预制体")] public GameObject[] cube;
    //public List<GameObject> ItemList = new List<GameObject>();
    [Header("所有可能生成在地块中心的物体")]public List<plants> plants;

    private void Awake()
    {
        instance = this;


    }
    int RandomGenerate()
    {
        return Random.Range(0, 50);
    }
    //用随机数生成列表里的一个物体并且为其设置父物体和位置  
    public void GenerateRandomItem(Transform pos)
    {
        //检查所有可以出现在这个层级的列表成员
        int index;
        int ra = RandomGenerate();
        if (ra < 25) return;
            while (true)
            {
                index = Random.Range(0, plants.Count);
                if (plants[index].CubeName.ToString() == LevelChecker.instance.CurrentLevelName)
                {
                    GameObject newitem = Instantiate(plants[index].Item);
                    newitem.transform.SetParent(pos);
                    newitem.transform.position = pos.position;
                    break;
                }


            }

    }
}
