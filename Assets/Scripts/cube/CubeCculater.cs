using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CubeCculater : MonoBehaviour
{
    public Transform test;
    public float distance;
    GameObject text;
    void Start()
    {

        test = GameObject.FindGameObjectWithTag("Player").transform;
        //给六个边的坐标赋值
        for(int i = 0; i < 6; i++)
        {
            Transform pos = transform.GetChild(i).GetChild(0);
            pos.position = EndPoint(distance, (1+i) * 60,transform.position);
        }
        //Debug.Log(order());
        transform.parent.GetComponentInChildren<SpriteRenderer>().sortingOrder = order()+GenerateCube.instance.Order;

        CubeEvent.instance.OnGenerating.Invoke();
        text = UIElementManager._instance.GetUIElement("方向事件");
        text.gameObject.SetActive(false);
    }

    void Update()
    {
        text.GetComponentInChildren<TextMeshProUGUI>().text=showCubeEvent();
        //Angle(transform.position, test.position);
    }
    //根据角色所处的位置改变地块图片的层级
    public int order()
    {
        float degree = Angle(transform.position, test.position);
        if (30 <= degree && degree <= 150)
        {
            return 5;
        }
        else if (degree > 210 && degree <= 320)
        {
            return -5;
        }
        else
        {
            return 0;
        }
    }
    public string showCubeEvent()
    {
        float degree = Angle(transform.position, test.position);
        for(int i = 0; i < 7; i++)
        {
            if (degree >= (i * 60 + 30) && degree < (i * 60 + 90))
            {
                return CubeEvent.instance.mEvent[i];
            }
        }
        return null;
    }
    //角色与地块中心的夹角
    public float Angle(Vector2 CenterTransform,Vector2 TargetTransform)
    {
        Vector2 v =  TargetTransform - CenterTransform;

        float Radians = Mathf.Atan2(v.y, v.x);
        float Degree = Radians * Mathf.Rad2Deg;

        if (Degree < 0)
        {
            Degree += 360;
        }

        return Degree;

    }

    public Vector2 EndPoint(float dis,float degree,Vector2 CenterTransform)
    {
        float radians = degree * Mathf.Deg2Rad;
        float x = CenterTransform.x + dis * Mathf.Cos(radians);
        float y = CenterTransform.y + dis * Mathf.Sin(radians);
         Vector2 endp = new Vector2(x, y);
        return endp;
    }
}
