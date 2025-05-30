using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 鼠标悬停展示信息
/// </summary>
public class MouseShowInfo : MonoBehaviour
{
    bool can_show_info = false;
    public GameObject infoBlank;    //显示名字的

    void Start()
    {
        
    }

    void Update()
    {
        MouseGetInfo();
    }
    void MouseGetInfo() //鼠标悬停获取碰撞箱的名字，显示在text上
    {
        // 获取当前鼠标的位置
        Vector3 mousePosition = Input.mousePosition;

        // 更新 UI 面板的位置
        infoBlank.SetActive(can_show_info);
        infoBlank.transform.position = new Vector3(mousePosition.x, mousePosition.y - 100, mousePosition.z);

        // 创建 PointerEventData 用于 UI 射线检测
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // 存储射线检测结果
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();

        // 使用 EventSystem 对所有 UI 进行 Raycast
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        // 检查是否有 UI 被命中
        if (raycastResults.Count > 0)
        {
            // 获取鼠标悬停的 UI 物体
            GameObject hitObject = raycastResults[0].gameObject;
            can_show_info = true;

            // 更新信息面板的内容为命中物体的名称
            infoBlank.GetComponentInChildren<TextMeshProUGUI>().text = hitObject.transform.parent.name;
        }
        else
        {
            can_show_info = false; // 如果没有命中 UI，隐藏面板
        }
    }
}
