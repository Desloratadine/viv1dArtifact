using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    /// <summary>
    /// UI��ָ���λ��ƫ����
    /// </summary>
    private Vector3 offset;

    private RectTransform rt;//
    private Vector3 pos;
    private Vector2 original_size;

    float minWidth;             //ˮƽ��С��ק��Χ
    float maxWidth;            //ˮƽ�����ק��Χ
    float minHeight;            //��ֱ��С��ק��Χ  
    float maxHeight;            //��ֱ�����ק��Χ
    float rangeX;
    float rangeY;

    Transform parent;
    Vector2 originalPos;
    public GameObject last_slot;

    public UnityEvent poschange;    //��λ�õ��¼�
    public UnityEvent posback;      //�������˵��¼�

    public UnityEvent<Transform,PointerEventData> CardInteraction;
    private void Awake()
    {
        if (CardInteraction == null)
            CardInteraction = new UnityEvent<Transform, PointerEventData>();
        CardInteraction.AddListener(CardEffect.Instance.CheckFunc);

    }
    void Start()
    {
        originalPos = GetComponent<RectTransform>().position;
        rt = GetComponent<RectTransform>();
        original_size = rt.sizeDelta;
        pos = rt.position;
        minWidth = rt.rect.width / 2;
        maxWidth = Screen.width - (rt.rect.width / 2);
        minHeight = rt.sizeDelta.y / 2;
        maxHeight = Screen.height - (rt.sizeDelta.y / 2);

    }
    private void OnEnable()
    {
        
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
    public void OnPointerClick(PointerEventData eventData)
    {
        Card card = transform.GetComponentInChildren<AddInventory>().card;
        DialogManager dialogManager = UIElementManager._instance.GetUIElement("�Ի���").GetComponentInChildren<DialogManager>();
        dialogManager.Name.text = card.itemName;
        Printer.instance.StartPrintText(card.itemInfo,dialogManager.Text);
        Debug.Log(card.itemName + card.itemInfo);
    }
    /// <summary>
    /// ��ʼ��ק
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPointerClick(eventData);

        originalPos = rt.position;
        Vector3 globalMousePos;
        // �ѵ�ǰѡ�е���ק����ʾ����ǰ��
        transform.parent.parent.parent.parent.GetComponent<Canvas>().sortingOrder = 1;
        Debug.Log(this.transform.GetSiblingIndex());
        //����Ļ����ת������������
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            //����UI��ָ��֮���λ��ƫ����
            offset = rt.position - globalMousePos;
        }

    }

    /// <summary>
    /// ��ק��
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {

        
        SetDraggedPosition(eventData);
        DragRangeLimit();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        CardPhysic.Instance.CardDrop.Invoke(transform, false);
    }

    /// <summary>
    /// ������ק
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        //������һ������װ����װ������֪������ֻ����ק�����жϵ���Ч��
        if (!transform.GetComponentInChildren<AddInventory>().CanFetch&& !transform.GetComponentInChildren<AddInventory>().CanMove)
        {
            Debug.Log("�ÿ�Ƭ������״̬");
            StartCoroutine(move(originalPos));
        }

  //ui�⣺����/�ص�ԭλ  
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (transform.GetComponentInChildren<AddInventory>().CanDrop)
            {
                Debug.Log("��Ƭ����");
                CardPhysic.Instance.CardDrop.Invoke(transform, transform.GetComponentInChildren<AddInventory>().CanDrop);
            
            }
            else
                StartCoroutine(move(originalPos));
        }

        else if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject slot = eventData.pointerCurrentRaycast.gameObject;


//�����˿տ���,
        if (slot.CompareTag("slot")/*&&transform.GetComponentInChildren<AddInventory>().CanMove*/)
        {

            if (!(transform.GetComponentInParent<AddInventory>().CanFetch) && slot.transform.parent.parent != transform.parent.parent.parent)
            {
                Debug.Log("�����Ƭ��������������������");
                StartCoroutine(move(originalPos));
            }
            //�Ҳ��ǰ󶨿���npc����ȣ�����������λ��,���±���
            //Debug.Log("��Ч����");
            else if(slot.transform.parent.parent != transform.parent.parent.parent)//ֻ����������������ж�
            {
                    Debug.Log("��Ч����"+slot.transform.parent.parent.name);
                    CardInteraction.Invoke(transform, eventData);
            }
            //ʣ�µľ��Ǳ���������
            AttachSlot(slot.transform.position);
            CardPhysic.Instance.CardDrop.Invoke(transform, false);

        }
//�������/û��������Ч����/�ǰ󶨿����ص�ԭλ
        else
        {
            if (slot.GetComponent<AddInventory>().card)
            {
                    Debug.Log(transform.name+"Receiver:"+eventData.pointerCurrentRaycast.gameObject.GetComponent<AddInventory>().card.name);
                CardInteraction.Invoke(transform, eventData);//���͸�������⣬������ֺϳ�/�����¼��� ��Ҫ���±���
                //transform.GetComponentInParent<IventoryMannage>().UpdateBag(transform.GetComponentInParent<IventoryMannage>().bag);//��������嵥�䶯�����±���
            }
            Debug.Log("�������");
            StartCoroutine(move(originalPos));
            CardPhysic.Instance.CardDrop.Invoke(transform, false);
        }


        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.parent.parent.parent.parent.GetComponent<Canvas>().sortingOrder = 0;
    }
    /// <summary>
    /// �ɿ�����Ƭ��λ��
    /// </summary>
    /// <param name="SlotPos"></param>
    public void AttachSlot(Vector3 SlotPos)
    {
        if (rt == null) Debug.Log(transform.parent.name+"no rt");
        
            rt.position = SlotPos;
            originalPos = rt.position;
    }
    /// <summary>
    /// ��קʱ����UI��λ��
    /// </summary>
    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;

        if (RectTransformUtility.ScreenPointToWorldPointInRectangle
            (rt, eventData.position, null, out globalMousePos))
        {
            rt.position = offset + globalMousePos;
        }
    }

    /// <summary>
    /// ƽ������
    /// </summary>
    public IEnumerator move(Vector3 target)
    {
        float duration = 1f;
        float timer = 0f;
        //�����������֮��̫���϶���Э�̻�������
        while ((target != rt.position) && (timer <= duration))
        {
            timer += Time.deltaTime;
            rt.position = Vector3.Lerp(rt.position, target, timer / duration);

            yield return null;
            if (target == rt.position) yield break;
        }

    }


}
