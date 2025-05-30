using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum stateName
{
    生长期,
    茂盛期,
    衰败期,
    死亡期
}
[CreateAssetMenu]
public class TestItem : ScriptableObject
{
    public List<plants> plants;
}
