using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterproperty : MonoBehaviour
{
    public float BloodVolume;   //Ѫ��
    private float MaxBlood;
    public float def;   //���������ٷ��Ƽ���

    public float hunger;    //������
    public float MaxHunger; 

    public float hungerSpeed;   //�����ȵ������ٶ�
    public float foodEnergy;    //ʳ�������
    public float Enemyatk ; //���˵Ĺ�����
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //�ܵ�������Ѫ
    private void Gethurt(float Enemyatk, float BloodVolume)
    {
        if (Enemyatk * (1f - def) >= BloodVolume)
            {
                BloodVolume = 0f;
            }

        else if (Enemyatk * (1f - def) < BloodVolume)
            {
            BloodVolume -= Enemyatk * (1f - def);
            }
    }
    
    //����
    private void GetHeal(float HealVolume,float BloodVolume)
    {

        if(BloodVolume+HealVolume > MaxBlood)
        {
            BloodVolume = MaxBlood;
        }
        else if(BloodVolume + HealVolume < MaxBlood)
        {
            BloodVolume += HealVolume;
        }
    }
    
    //�𽥼���
    private void GetHungry(float hunger,float speed)
    {
        while (hunger > 0f)
        {
            hunger -= speed*Time.deltaTime;
        }
    }
   
    //��ʳ���伢����
    private void eat(float foodEnergy,float hunger)
    {
        if (foodEnergy + hunger >= MaxHunger)
            hunger = MaxHunger;
        else if (foodEnergy + hunger < MaxHunger)
        {
            hunger += foodEnergy;
        }
    }
}
