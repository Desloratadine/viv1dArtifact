using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterproperty : MonoBehaviour
{
    public float BloodVolume;   //血量
    private float MaxBlood;
    public float def;   //防御力，百分制减伤

    public float hunger;    //饥饿度
    public float MaxHunger; 

    public float hungerSpeed;   //饥饿度掉条的速度
    public float foodEnergy;    //食物的能量
    public float Enemyatk ; //敌人的攻击力
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //受到攻击掉血
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
    
    //治疗
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
    
    //逐渐饥饿
    private void GetHungry(float hunger,float speed)
    {
        while (hunger > 0f)
        {
            hunger -= speed*Time.deltaTime;
        }
    }
   
    //进食补充饥饿度
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
