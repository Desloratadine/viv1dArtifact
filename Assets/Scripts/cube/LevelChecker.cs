using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CubeName
{
    �ݵ�,
    ɭ��1,
    ɭ��2,
};
public class LevelChecker : MonoBehaviour
{
    public static LevelChecker instance;
    public string CurrentLevelName;
    public Dictionary<string, GameObject> CubesDic;//����-��ӦԤ����
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

    public void CreatCubeDic()//���滹���ٸ�
    {

        for (int i = 0; i < GenerationManager.instance.cube.Length; i++)
        {
            CubesDic.Add(CubeName.GetName(typeof(CubeName), i), GenerationManager.instance.cube[i]);
        }
    }
    private void CreateStepDic()
    {
        StepsDic.Add(CubeName.�ݵ�, 5);
        StepsDic.Add(CubeName.ɭ��1,10);
        StepsDic.Add(CubeName.ɭ��2, 15);
    }
    //�����߲�����������һ��Ĳ���
    public string NormalCheck(int CubeCount)
    {
        LanternLight.instance.ChangeLight.Invoke();//��ƹ�Ч�¼�
        foreach ( var step in StepsDic)
        {
            CurrentLevelName = step.Key.ToString();

            if (step.Value > CubeCount)
            {
                //Debug.Log("����㼶��"+step.Key +"��ʣ������"+ (step.Value - CubeCount));
                break;
            }

        }

        return CurrentLevelName;
    }
}
