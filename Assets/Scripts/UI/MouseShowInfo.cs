using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// �����ͣչʾ��Ϣ
/// </summary>
public class MouseShowInfo : MonoBehaviour
{
    bool can_show_info = false;
    public GameObject infoBlank;    //��ʾ���ֵ�

    void Start()
    {
        
    }

    void Update()
    {
        MouseGetInfo();
    }
    void MouseGetInfo() //�����ͣ��ȡ��ײ������֣���ʾ��text��
    {
        // ��ȡ��ǰ����λ��
        Vector3 mousePosition = Input.mousePosition;

        // ���� UI ����λ��
        infoBlank.SetActive(can_show_info);
        infoBlank.transform.position = new Vector3(mousePosition.x, mousePosition.y - 100, mousePosition.z);

        // ���� PointerEventData ���� UI ���߼��
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // �洢���߼����
        var raycastResults = new System.Collections.Generic.List<RaycastResult>();

        // ʹ�� EventSystem ������ UI ���� Raycast
        EventSystem.current.RaycastAll(pointerData, raycastResults);

        // ����Ƿ��� UI ������
        if (raycastResults.Count > 0)
        {
            // ��ȡ�����ͣ�� UI ����
            GameObject hitObject = raycastResults[0].gameObject;
            can_show_info = true;

            // ������Ϣ��������Ϊ�������������
            infoBlank.GetComponentInChildren<TextMeshProUGUI>().text = hitObject.transform.parent.name;
        }
        else
        {
            can_show_info = false; // ���û������ UI���������
        }
    }
}
