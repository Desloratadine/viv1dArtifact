using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextAsset Dialog;
    [SerializeField, Header("ͷ���ͼƬ")] public SpriteRenderer CharacterImage;
    [SerializeField, Header("��ʾ��ɫ�����ı�")] public TMP_Text Name;
    public TMP_Text Text;
    [SerializeField, Header("�ı���ͼƬ")] public GameObject TextObject;

    public string[] DialogRow;

    public int index = 0;
    [SerializeField,Header("next��ť")]    public Button Button;

    public Button OptionButton;

    [SerializeField, Header("������Ԥ����")] public GameObject DialogPrefab;//
    public Transform DialogGroup;
    public GameObject NewDialog = null;

    void Start()
    {
        Text = TextObject.GetComponentInChildren<TMP_Text>();
        ReadText(Dialog);
        ShowDialogs();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void UpdateText(string name,string text)
    {
        Name.text = name;
        Text.text = text;
    }

    public void ReadText(TextAsset textAsset)   //���зָ�
    {
       DialogRow = textAsset.text.Split('\n');

    }

    public void ShowDialogs()
    {

        for (int i=0; i<DialogRow.Length; i++)
        {
            string[] cells = DialogRow[i].Split(',');
            if (int.TryParse(cells[1], out int id))
            {
                if (id == index && cells[0] == "#")
                {
                  UpdateText(cells[2], cells[4]);
                    index = int.Parse(cells[5]);
                   Debug.Log("������ʾ"+id );//����û�����⣬����Ϊʲô�ı�����ʾ
                    Debug.Log("������ʾ" + cells[4]);//�ı�Ҳ�õ���
                    break;
                }
                else if(cells[0] == "END")
                {
                    TextObject.SetActive(false);
                }
            }
               
        }
    }


    public void onClick()
    {
        ShowDialogs();
        
        NewDialog = null;
    }
}
