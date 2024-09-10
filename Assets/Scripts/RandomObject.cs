using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    public RectTransform spawnArea; // �������ɷ�Χ�� RectTransform
    public PrefabPool prefabPool;
    public float spawnInterval = 0.2f;

    public GameObject[] prefabs; // ��Ҫ���ɵ�Ԥ��������

    public LayerMask spawnLayerMask;
    private bool flag = true;
    public float distance;

    private void Start()
    {
        InvokeRepeating("SpawnRandomObject", 0.5f, spawnInterval);    //���÷���������һ�ε��õ�ʱ�䣬���õļ��
    }

    private void SpawnRandomObject()
    {
        // ���ѡ��һ��Ԥ����
        GameObject prefab = prefabs[Random.Range(0, prefabs.Length)];
        GameObject obj = prefabPool.GetObject(prefab);

        // ��ȡ RectTransform �ı߽�
        Vector2 min = spawnArea.rect.min;
        Vector2 max = spawnArea.rect.max;
        Vector2 size = spawnArea.rect.size;

        // ת������������ϵ
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
        // �������λ��
        Vector3 Position = new Vector3(
            Random.Range(minWorld.x, maxWorld.x),
            Random.Range(minWorld.y, maxWorld.y),
            0f
        );

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Position,distance,spawnLayerMask);
        if (colliders.Length == 0)
        {
            return Position; // û�������ص���������������
        }
        else return Vector3.zero;
    }


 
}

