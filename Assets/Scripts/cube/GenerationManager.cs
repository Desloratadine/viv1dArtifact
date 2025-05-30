using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenerationManager : MonoBehaviour
{


    public static GenerationManager instance;
    [Header("�ؿ�Ԥ����")] public GameObject[] cube;
    //public List<GameObject> ItemList = new List<GameObject>();
    [Header("���п��������ڵؿ����ĵ�����")]public List<plants> plants;

    private void Awake()
    {
        instance = this;


    }
    int RandomGenerate()
    {
        return Random.Range(0, 50);
    }
    //������������б����һ�����岢��Ϊ�����ø������λ��  
    public void GenerateRandomItem(Transform pos)
    {
        //������п��Գ���������㼶���б��Ա
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
