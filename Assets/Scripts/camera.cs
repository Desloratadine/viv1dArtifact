using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�Ӳ�ͨ�ù��ڶ�����
public class camera : MonoBehaviour
{
    public Transform cameratr;//�������
    [SerializeField] float objectx,objecty;//Ŀ����������
    [SerializeField] float moverateX;//X�ƶ��ٶ�
    [SerializeField] float moverateY;//X�ƶ��ٶ�
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
        if (ylock)//����y��
        {
            transform.position = new Vector2((objectx + moverateX*cameratr.position.x),objecty);
        }
        else if (xlock) //����x��
        {
             transform.position = new Vector2( objectx,(objecty + moverateY * cameratr.position.y));
        }
        else
        {
            transform.position = new Vector2((objectx + moverateX * cameratr.position.x), (objecty + moverateY * cameratr.position.y));
        }
            
    }
}
