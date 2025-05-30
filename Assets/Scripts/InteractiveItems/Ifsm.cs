using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//森林场景中随机生成物的状态机
public class Ifsm : MonoBehaviour
{
    Item item;
    private int CurrentState;//当前阶段
    void Start()
    {
        item = GetComponent<AddInventory>().thisItem;
        initialize();

    }
    //初始化随机阶段
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

        //Debug.Log("现在的状态是" + item.StatesInfo[index].stateName);
    }

    public void SetState()
    {
        CurrentState = UpdateCurrentState(CurrentState);
        ChangeState(CurrentState);

    }


}
