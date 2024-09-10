using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleBacground : MonoBehaviour
{
    //角色位置也就是摄像机位置
    public Transform character;
    //地图边缘的x坐标
    public float endPos;
    public GameObject image1;
    public GameObject image2;

    //距离
    public float distance;
    [SerializeField]
    private bool right=false;
    [SerializeField]
    private bool left=false;

    private bool MoveOn = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            MoveOn = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            right = true;
            left = false;
            MoveOn = true;
        }
        else if(Input.GetAxisRaw("Horizontal") == -1)
        {
            left = true;
            right = false;
            MoveOn=true;
        }
        

        //向右边
        if (right)
        {
            if ((image1.transform.position.x > image2.transform.position.x)&&MoveOn)
            {
                cycleRt(image1, image2);
            }
        
            else if ((image1.transform.position.x < image2.transform.position.x)&&MoveOn)
            {
               cycleRt(image2,image1);
            }
        }
        //向左边
        else if (left&&MoveOn)
        {
            if ((image1.transform.position.x < image2.transform.position.x)&&MoveOn)
            {
                cycleLf(image1, image2);
            }

            else if ((image1.transform.position.x > image2.transform.position.x)&&MoveOn)
            {
                cycleLf(image2, image1);
            }
        }


 
    }
    public void cycleRt(GameObject imagef,GameObject imageb)
    {
       if (character.position.x - endPos <= distance)//边缘判定，当摄像头达到前面图的边缘
        {
            //后面图x坐标轴＋22f
            imageb.transform.position = new Vector2(imagef.transform.position.x + 22f, 2.27f);
        }
    }
    public void cycleLf(GameObject imagef, GameObject imageb)
    {
        if (character.position.x - endPos <= distance)
        {
            //x坐标轴＋22f
            imageb.transform.position = new Vector2(imagef.transform.position.x - 22f, 2.27f);
        }
    }
}
