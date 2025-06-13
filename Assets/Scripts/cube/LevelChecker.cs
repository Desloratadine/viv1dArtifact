using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CubeName
{
    草地,
    森林1,
    森林2,
};
public class LevelChecker : MonoBehaviour
{
    public static LevelChecker instance;
    public string CurrentLevelName;
    public Dictionary<string, GameObject> CubesDic;//名称-对应预制体
    public Dictionary<CubeName,int> StepsDic;
    private void Awake()
    {
        if (CubesDic == null) CubesDic = new Dictionary<string, GameObject> { };
        if (StepsDic == null) StepsDic = new Dictionary<CubeName, int> { };
        instance = this;
        CreatCubeDic();
        CreateStepDic();        
    }
    void Start()
    {

        CurrentLevelName = Enum.GetName(typeof(CubeName), 0);
    }

    public void CreatCubeDic()//后面还用再改
    {

        for (int i = 0; i < GenerationManager.instance.cube.Length; i++)
        {
            CubesDic.Add(CubeName.GetName(typeof(CubeName), i), GenerationManager.instance.cube[i]);
        }
    }
    private void CreateStepDic()
    {
        StepsDic.Add(CubeName.草地, 5);
        StepsDic.Add(CubeName.森林1,10);
        StepsDic.Add(CubeName.森林2, 15);
    }
    //按已走步数检查距离下一层的步数
    public string NormalCheck(int CubeCount)
    {
        LanternLight.instance.ChangeLight.Invoke();//提灯光效事件
        foreach ( var step in StepsDic)
        {
            CurrentLevelName = step.Key.ToString();

            if (step.Value > CubeCount)
            {
                //Debug.Log("距离层级："+step.Key +"还剩步数："+ (step.Value - CubeCount));
                break;
            }

        }

        return CurrentLevelName;
    }
}
