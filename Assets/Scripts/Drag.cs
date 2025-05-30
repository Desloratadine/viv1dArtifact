using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary>
    /// UI��ָ���λ��ƫ����
    /// </summary>
    Vector3 offset;

    RectTransform rt;
    Vector3 pos;

    float minWidth;             //ˮƽ��С��ק��Χ
    float maxWidth;            //ˮƽ�����ק��Χ
    float minHeight;            //��ֱ��С��ק��Χ  
    float maxHeight;            //��ֱ�����ק��Χ
    float rangeX;
    float rangeY;

    void Update()
    {
       
    }

    void Start()
    {
        rt = GetComponent<RectTransform>();
        pos = rt.position;
        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        //minHeight = rt.rect.height / 2;
        minHeight = rt.sizeDelta.y/2;
        maxHeight = Screen.height - (rt.sizeDelta.y/2);

     
    }

    /// <summary>
    /// ��ק��Χ����
    /// </summary>
   public void DragRangeLimit()
    {
        //����ˮƽ/��ֱ��ק��Χ����С/���ֵ��
        rangeX = Mathf.Clamp(rt.position.x, minWidth, maxWidth);
        rangeY = Mathf.Clamp(rt.position.y, minHeight, maxHeight);
        //����λ��
        rt.position = new Vector3(rangeX, rangeY, 0);
    }
    /// <summary>
    /// ��ʼ��ק
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        //����Ļ����ת������������
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            //����UI��ָ��֮���λ��ƫ����
            offset = rt.position - globalMousePos;
        }
        // �ѵ�ǰѡ�е���ק����ʾ����ǰ��
        rt.SetAsLastSibling();

    }

    /// <summary>
    /// ��ק��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        SetDraggedPosition(eventData);
        DragRangeLimit();
    }

    /// <summary>
    /// ������ק
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {

    }

    /// <summary>
    /// ����UI��λ��
    /// </summary>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            rt.position = offset + globalMousePos;
        }
    }
}
