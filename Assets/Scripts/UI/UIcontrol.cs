using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIControl", menuName = "UI/UIControl Config")]
public class UIcontrol : ScriptableObject
{
    public UIControl[] UIList;
}
[System.Serializable]
public class UIControl
{
    public string name;
    public GameObject prefab;
    [System.NonSerialized] public GameObject instance;

}