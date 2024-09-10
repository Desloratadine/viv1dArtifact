using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//状态管理类
public class Character
{
    //初始状态为空
    CharacterState state = null;
   
    public void SetState(CharacterState state)
    {
        if (state == null)
        {
            return;
        }
        this.state = state; //用来储存当前的状态，从状态接口那边传入的
    }
    public CharacterState GetState()
    {
        return state;
    }
}
