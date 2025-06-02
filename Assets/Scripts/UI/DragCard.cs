using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DragCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    /// <summary>
    /// UI和指针的位置偏移量
    /// </summary>
    private Vector3 offset;

    private RectTransform rt;//
    private Vector3 pos;
    private Vector2 original_size;

    float minWidth;             //水平最小拖拽范围
    float maxWidth;            //水平最大拖拽范围
    float minHeight;            //垂直最小拖拽范围  
    float maxHeight;            //垂直最大拖拽范围
    float rangeX;
    float rangeY;

    Transform parent;
    Vector2 originalPos;
    public GameObject last_slot;

    public UnityEvent poschange;    //新位置的事件
    public UnityEvent posback;      //步数回退的事件

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
    /// 拖拽范围限制
    /// </summary>
    public void DragRangeLimit()
    {
        //限制水平/垂直拖拽范围在最小/最大值内
        rangeX = Mathf.Clamp(rt.position.x, minWidth, maxWidth);
        rangeY = Mathf.Clamp(rt.position.y, minHeight, maxHeight);
        //更新位置
        rt.position = new Vector3(rangeX, rangeY, 0);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Card card = transform.GetComponentInChildren<AddInventory>().card;
        DialogManager dialogManager = UIElementManager._instance.GetUIElement("对话框").GetComponentInChildren<DialogManager>();
        dialogManager.Name.text = card.itemName;
        Printer.instance.StartPrintText(card.itemInfo,dialogManager.Text);
        Debug.Log(card.itemName + card.itemInfo);
    }
    /// <summary>
    /// 开始拖拽
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnPointerClick(eventData);

        originalPos = rt.position;
        Vector3 globalMousePos;
        // 把当前选中的拖拽框显示在最前面
        transform.parent.parent.parent.parent.GetComponent<Canvas>().sortingOrder = 1;
        Debug.Log(this.transform.GetSiblingIndex());
        //将屏幕坐标转换成世界坐标
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, null, out globalMousePos))
        {
            //计算UI和指针之间的位置偏移量
            offset = rt.position - globalMousePos;
        }

    }

    /// <summary>
    /// 拖拽中
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {

        
        SetDraggedPosition(eventData);
        DragRangeLimit();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        CardPhysic.Instance.CardDrop.Invoke(transform, false);
    }

    /// <summary>
    /// 结束拖拽
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        //锁定，一般用于装饰性装备或者知觉卡，只能拖拽不能判断叠加效果
        if (!transform.GetComponentInChildren<AddInventory>().CanFetch&& !transform.GetComponentInChildren<AddInventory>().CanMove)
        {
            Debug.Log("该卡片是锁定状态");
            StartCoroutine(move(originalPos));
        }

  //ui外：掉落/回到原位  
        else if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (transform.GetComponentInChildren<AddInventory>().CanDrop)
            {
                Debug.Log("卡片掉落");
                CardPhysic.Instance.CardDrop.Invoke(transform, transform.GetComponentInChildren<AddInventory>().CanDrop);
            
            }
            else
                StartCoroutine(move(originalPos));
        }

        else if (EventSystem.current.IsPointerOverGameObject())
        {
            GameObject slot = eventData.pointerCurrentRaycast.gameObject;


//碰到了空卡槽,
        if (slot.CompareTag("slot")/*&&transform.GetComponentInChildren<AddInventory>().CanMove*/)
        {

            if (!(transform.GetComponentInParent<AddInventory>().CanFetch) && slot.transform.parent.parent != transform.parent.parent.parent)
            {
                Debug.Log("这个卡片不能与其他背包交换！");
                StartCoroutine(move(originalPos));
            }
            //且不是绑定卡（npc衣物等），吸附到新位置,更新背包
            //Debug.Log("有效卡槽");
            else if(slot.transform.parent.parent != transform.parent.parent.parent)//只会进到背包交换的判断
            {
                    Debug.Log("有效卡槽"+slot.transform.parent.parent.name);
                    CardInteraction.Invoke(transform, eventData);
            }
            //剩下的就是背包内整理
            AttachSlot(slot.transform.position);
            CardPhysic.Instance.CardDrop.Invoke(transform, false);

        }
//不会掉落/没有碰到有效卡槽/非绑定卡。回到原位
        else
        {
            if (slot.GetComponent<AddInventory>().card)
            {
                    Debug.Log(transform.name+"Receiver:"+eventData.pointerCurrentRaycast.gameObject.GetComponent<AddInventory>().card.name);
                CardInteraction.Invoke(transform, eventData);//发送给互动检测，如果出现合成/交换事件则 需要更新背包
                //transform.GetComponentInParent<IventoryMannage>().UpdateBag(transform.GetComponentInParent<IventoryMannage>().bag);//如果背包清单变动，更新背包
            }
            Debug.Log("其他情况");
            StartCoroutine(move(originalPos));
            CardPhysic.Instance.CardDrop.Invoke(transform, false);
        }


        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.parent.parent.parent.parent.GetComponent<Canvas>().sortingOrder = 0;
    }
    /// <summary>
    /// 松开鼠标后卡片的位置
    /// </summary>
    /// <param name="SlotPos"></param>
    public void AttachSlot(Vector3 SlotPos)
    {
        if (rt == null) Debug.Log(transform.parent.name+"no rt");
        
            rt.position = SlotPos;
            originalPos = rt.position;
    }
    /// <summary>
    /// 拖拽时更新UI的位置
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
    /// 平滑过渡
    /// </summary>
    public IEnumerator move(Vector3 target)
    {
        float duration = 1f;
        float timer = 0f;
        //这里如果放下之后太快拖动，协程还不结束
        while ((target != rt.position) && (timer <= duration))
        {
            timer += Time.deltaTime;
            rt.position = Vector3.Lerp(rt.position, target, timer / duration);

            yield return null;
            if (target == rt.position) yield break;
        }

    }


}
