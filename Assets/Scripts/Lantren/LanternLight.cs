using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

//��ƵĹ�Ч 1.����2.����ȼ��ʱ��ı����Ⱥ�������Χ 3.����ȼ�ϸı���ɫ 4.ҡҷЧ��/��������
public class LanternLight : MonoBehaviour
{
    public static LanternLight instance;
    public UnityEvent ChangeLight;
    Light2D Light;
    float StepLight;//ÿ�μ��ٵ�����
    int stepCount = 20;//���ʵ���ϸı�������۲��������۲���Խ�࣬��������Խ�����ߵ�ԽԶ
    public float LightIntensityValue; //�ƹ��ʼ����
    bool CanGoHome = true; //�Ƿ���Իؼ�

    GameObject character;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        if(ChangeLight == null)
        {
            ChangeLight = new UnityEvent();
        }
        ChangeLight.AddListener(LightIntensity);
        Light = GetComponent<Light2D>();
        character = GameObject.Find("character");
        CaculateStep();
    }
    void goAdventure()
    {
        CaculateStep();//ÿ��̽�տ�ʼǰ��Ҫ���㲽��

    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, character.transform.position, Time.deltaTime * 5f);
        //transform.position =character.transform.position;
    }
    public void CaculateStep()
    {

        //int step = 0;
        //step = Light.intensity > 0 ? (int)(Light.intensity * 20) : 0;
        LightIntensityValue = Light.intensity; 
        StepLight = Light.intensity/stepCount;
    }
    public void LightIntensity()//���������𽥼�����Э�̣�
    {
        if (Light.intensity > 0)
        {
            Light.intensity -= StepLight;
            if(Light.intensity < LightIntensityValue*.3f)
            {
                CanGoHome = false; //����С��30%���ܻؼ�
            }
        }
        StartCoroutine(HarmonicMotion());
    }
    public IEnumerator  HarmonicMotion()//�ƹ��г�˶�
    {
        float amp=.2f;
        float fre = 5f;
        Vector3 axis = Vector3.right;
        float decay = 2f; // ˥��ϵ����Խ��˥��Խ��
        Vector3 pos=transform.position;
        float time = 0f;
        while (time<=1f)
        {
            time += Time.deltaTime;
            float currentAmp = amp * Mathf.Exp(-decay * time);
            float offset = currentAmp * Mathf.Sin(2 * Mathf.PI * fre * Time.time);
            transform.position = transform.position + axis * offset;
            yield return null;
        }
        yield return null;
    }
}
