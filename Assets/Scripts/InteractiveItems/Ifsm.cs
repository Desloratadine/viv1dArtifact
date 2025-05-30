using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ɭ�ֳ���������������״̬��
public class Ifsm : MonoBehaviour
{
    Item item;
    private int CurrentState;//��ǰ�׶�
    void Start()
    {
        item = GetComponent<AddInventory>().thisItem;
        initialize();

    }
    //��ʼ������׶�
    void initialize()
    {
        CurrentState = Random.Range(0, item.StatesInfo.Length);
        ChangeState(CurrentState);
        
    }
    int UpdateCurrentState(int NewState)
    {
        if (CurrentState == item.StatesInfo.Length-1) return 0;
        else
            return ++CurrentState;
    }
    void ChangeState(int index)
    {
        transform.GetComponent<SpriteRenderer>().sprite = item.StatesInfo[index].stateSprite;

        //Debug.Log("���ڵ�״̬��" + item.StatesInfo[index].stateName);
    }

    public void SetState()
    {
        CurrentState = UpdateCurrentState(CurrentState);
        ChangeState(CurrentState);

    }


}
