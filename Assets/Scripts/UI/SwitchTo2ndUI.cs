using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 控制二级界面的虚拟摄像头，节省资源全局唯一，事件监听，用reach_show通知
/// 按R互动启用虚拟摄像头，再按一次离开
/// </summary>
public class SwitchTo2ndUI : MonoBehaviour
{
    public static SwitchTo2ndUI instance;
    [Header("聚焦到物体的摄像机")]public GameObject TargetCamera;
    [Header("跟随房间切换的摄像机")] public GameObject RoomCamera;
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
        //按R互动
        if (Input.GetKeyDown(KeyCode.R)&&canSwitchTo2nd)
        {
            //切换激活的相机
            TargetCamera.SetActive(!TargetCamera.activeSelf);
            RoomCamera.SetActive(!TargetCamera.activeSelf);
            for(int i = 0; i < UI.Length; i++)
            {
                UI[i].SetActive(TargetCamera.activeSelf);
                if (UI[i].GetComponent<Animator>())
                UI[i].GetComponent<Animator>().Play("2ndUI_OUT");
            }

            //禁用角色的移动（动画有bug 回头修）
            GameObject.FindWithTag("Player").GetComponent<CharacterController>().enabled
            = !TargetCamera.activeSelf;

        }      
    }

    //提前改变虚拟相机的位置
    public void SwitchActivity(Vector2 pos)
    {
        if(TargetCamera)
        TargetCamera.transform.position = new Vector3(pos.x,pos.y,TargetCamera.transform.position.z);
    }

}
