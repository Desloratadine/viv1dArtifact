using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//视差通用挂在对象上
public class camera : MonoBehaviour
{
    public Transform cameratr;//相机坐标
    [SerializeField] float objectx,objecty;//目标物体坐标
    [SerializeField] float moverateX;//X移动速度
    [SerializeField] float moverateY;//X移动速度
    public bool ylock;
    public bool xlock;
    void Start()
    {
        objectx=this.transform.position.x;
        objecty=this.transform.position.y;
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (ylock)//锁定y轴
        {
            transform.position = new Vector2((objectx + moverateX*cameratr.position.x),objecty);
        }
        else if (xlock) //锁定x轴
        {
             transform.position = new Vector2( objectx,(objecty + moverateY * cameratr.position.y));
        }
        else
        {
            transform.position = new Vector2((objectx + moverateX * cameratr.position.x), (objecty + moverateY * cameratr.position.y));
        }
            
    }
}
