using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ���ƶ����������������ͷ����ʡ��Դȫ��Ψһ���¼���������reach_show֪ͨ
/// ��R����������������ͷ���ٰ�һ���뿪
/// </summary>
public class SwitchTo2ndUI : MonoBehaviour
{
    public static SwitchTo2ndUI instance;
    [Header("�۽�������������")]public GameObject TargetCamera;
    [Header("���淿���л��������")] public GameObject RoomCamera;
    public UnityEvent<Vector2> canSwitch;
    public bool canSwitchTo2nd;
    public GameObject[] UI;
    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {
        //TargetCamera = GameObject.Find("2ndCamera");
        if (canSwitch == null) canSwitch = new UnityEvent<Vector2>();
        canSwitch.AddListener(SwitchActivity);

    }

    void Update()
    {
        //��R����
        if (Input.GetKeyDown(KeyCode.R)&&canSwitchTo2nd)
        {
            //�л���������
            TargetCamera.SetActive(!TargetCamera.activeSelf);
            RoomCamera.SetActive(!TargetCamera.activeSelf);
            for(int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(TargetCamera.activeSelf);
                if (UI[i].GetComponent<Animator>())
                UI[i].GetComponent<Animator>().Play("2ndUI_OUT");
            }

            //���ý�ɫ���ƶ���������bug ��ͷ�ޣ�
            GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled
            = !TargetCamera.activeSelf;

        }      
    }

    //��ǰ�ı����������λ��
    public void SwitchActivity(Vector2 pos)
    {
        if(TargetCamera)
        TargetCamera.transform.position = new Vector3(pos.x,pos.y,TargetCamera.transform.position.z);
    }

}
