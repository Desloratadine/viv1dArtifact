using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCculater : MonoBehaviour
{
    public Transform test;
    public float distance;
    void Start()
    {
        test = GameObject.FindGameObjectWithTag("Player").transform;
        //给六个边的坐标赋值
        for(int i = 0; i < 6; i++)
        {
            Transform pos = transform.GetChild(i).GetChild(0);
            pos.position = EndPoint(distance, (1+i) * 60,transform.position);
        }
        Debug.Log(order());
        transform.parent.GetComponentInChildren<SpriteRenderer>().sortingOrder = order()+GenerateCube.instance.Order;
    }

    void Update()
    {
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
        else if (degree >210 && degree <= 320)
        {
            return -5;
        }
        else
        {
            return 0;
        }
    }
    public float Angle(Vector2 CenterTransform,Vector2 TargetTransform)
    {
        Vector2 v =  TargetTransform - CenterTransform;

        float Radians = Mathf.Atan2(v.y, v.x);
        float Degree = Radians * Mathf.Rad2Deg;
        if (Degree < 0)
        {
            Degree += 360;
        }
       // Debug.Log("夹角为" + Degree);
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
