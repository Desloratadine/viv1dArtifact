using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//绑在持有宝箱的npc/物体/商店上
public class AddBag : MonoBehaviour
{
    [Header("资源箱")]public InventoryBag thisBag;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public InventoryBag GetBag()
    {
        return thisBag;
    }
}
