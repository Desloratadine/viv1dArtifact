using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//״̬������
public class Character
{
    //��ʼ״̬Ϊ��
    CharacterState state = null;
   
    public void SetState(CharacterState state)
    {
        if (state == null)
        {
            return;
        }
        this.state = state; //�������浱ǰ��״̬����״̬�ӿ��Ǳߴ����
    }
    public CharacterState GetState()
    {
        return state;
    }
}
