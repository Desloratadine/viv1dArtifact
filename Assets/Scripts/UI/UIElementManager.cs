using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementManager : MonoBehaviour
{
    public static UIElementManager _instance;

    public UIcontrol uiControlConfig; 

    public static UIElementManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // 如果场景中没有实例，自动创建一个新的 GameObject 并附加脚本
                GameObject obj = new GameObject("UIElementManager");
                _instance = obj.AddComponent<UIElementManager>();
                DontDestroyOnLoad(obj); // 可选：跨场景不销毁
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // 避免重复实例
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        foreach(var obj in uiControlConfig.UIList)
        {
            if (obj.instance == null) obj.instance = Instantiate(obj.prefab);
            DontDestroyOnLoad(obj.instance);
        }
    }
    /// <summary>
    /// 根据名字获取UI实例
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetUIElement(string name)
    {
        if (uiControlConfig == null || uiControlConfig.UIList == null)
        {
            Debug.LogError("UIControl 配置未赋值或为空！");
            return null;
        }

        foreach (var item in uiControlConfig.UIList)
        {
            if (item.name == name)
            {
                return item.instance;
            }
        }

        Debug.LogError($"未找到名为 {name} 的 UI 控件！");
        return null;
    }
}
