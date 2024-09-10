using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int initialSize;
    }

    public List<PoolItem> poolItems;
    public Dictionary<GameObject, Queue<GameObject>> pools;

    private void Start()
    {
        pools = new Dictionary<GameObject, Queue<GameObject>>();

        foreach (var item in poolItems)
        {
            var pool = new Queue<GameObject>();
            for (int i = 0; i < item.initialSize; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
            pools[item.prefab] = pool;
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out var pool) && pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnObject(GameObject prefab, GameObject obj)
    {
        if (pools.TryGetValue(prefab, out var pool))
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
        else
        {
            Destroy(obj);
        }
    }
}

