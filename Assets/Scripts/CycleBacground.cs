using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleBacground : MonoBehaviour
{
    //��ɫλ��Ҳ���������λ��
    public Transform character;
    //��ͼ��Ե��x����
    public float endPos;
    public GameObject image1;
    public GameObject image2;

    //����
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
        

        //���ұ�
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
        //�����
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
       if (character.position.x - endPos <= distance)//��Ե�ж���������ͷ�ﵽǰ��ͼ�ı�Ե
        {
            //����ͼx�����ᣫ22f
            imageb.transform.position = new Vector2(imagef.transform.position.x + 22f, 2.27f);
        }
    }
    public void cycleLf(GameObject imagef, GameObject imageb)
    {
        if (character.position.x - endPos <= distance)
        {
            //x�����ᣫ22f
            imageb.transform.position = new Vector2(imagef.transform.position.x - 22f, 2.27f);
        }
    }
}
