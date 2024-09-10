using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;//��Ʒ����
    public Sprite ItemSprite;//��Ʒ����Ƭ
    public int basicPrice;//������ֵ
    public int itemHeld = 1;//��Ʒ��������Ĭ����һ������Ϊʰȡ��һ����ֱ��Ϊ1����ʰȡ��ֱ��+1����
    [TextArea]//ʹtext���Ը��ı����ж�����д
    public string itemInfo;//��Ʒ�Ľ�������

}
//�ýű���һ����Ʒ�Ļ����ű�

