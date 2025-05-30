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
                // ���������û��ʵ�����Զ�����һ���µ� GameObject �����ӽű�
                GameObject obj = new GameObject("UIElementManager");
                _instance = obj.AddComponent<UIElementManager>();
                DontDestroyOnLoad(obj); // ��ѡ���糡��������
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); // �����ظ�ʵ��
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
    /// �������ֻ�ȡUIʵ��
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject GetUIElement(string name)
    {
        if (uiControlConfig == null || uiControlConfig.UIList == null)
        {
            Debug.LogError("UIControl ����δ��ֵ��Ϊ�գ�");
            return null;
        }

        foreach (var item in uiControlConfig.UIList)
        {
            if (item.name == name)
            {
                return item.instance;
            }
        }

        Debug.LogError($"δ�ҵ���Ϊ {name} �� UI �ؼ���");
        return null;
    }
}
