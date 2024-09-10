using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public RectTransform spawnArea; // 限制生成范围的 RectTransform
    public PrefabPool prefabPool;
    public float spawnInterval = 0.2f;

    public GameObject[] prefabs; // 需要生成的预制体数组

    public LayerMask spawnLayerMask;
    private bool flag = true;
    public float distance;

    private void Start()
    {
        InvokeRepeating("SpawnRandomObject", 0.5f, spawnInterval);    //调用方法名，第一次调用的时间，调用的间隔
    }

    private void SpawnRandomObject()
    {
        // 随机选择一个预制体
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject obj = prefabPool.GetObject(prefab);

        // 获取 RectTransform 的边界
        Vector2 min = spawnArea.rect.min;
        Vector2 max = spawnArea.rect.max;
        Vector2 size = spawnArea.rect.size;

        // 转换到世界坐标系
        Vector3 minWorld = spawnArea.TransformPoint(min);
        Vector3 maxWorld = spawnArea.TransformPoint(max);

       

            Vector3 position = randomPosition(minWorld,maxWorld);
        if (position != Vector3.zero)
        {
            flag = false;
            obj.transform.position = position;


        }
        else
        prefabPool.ReturnObject(prefab,obj);
        



        
    }


    public Vector3 randomPosition(Vector2 minWorld,Vector2 maxWorld)
    {
        // 生成随机位置
        Vector3 Position = new Vector3(
            Random.Range(minWorld.x, maxWorld.x),
            Random.Range(minWorld.y, maxWorld.y),
            0f
        );

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Position,distance,spawnLayerMask);
        if (colliders.Length == 0)
        {
            return Position; // 没有物体重叠，可以生成物体
        }
        else return Vector3.zero;
    }


 
}

