using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public TextAsset Dialog;
    [SerializeField, Header("头像的图片")] public SpriteRenderer CharacterImage;
    [SerializeField, Header("显示角色名的文本")] public TMP_Text Name;
    public TMP_Text Text;
    [SerializeField, Header("文本栏图片")] public GameObject TextObject;

    public string[] DialogRow;

    public int index = 0;
    [SerializeField,Header("next按钮")]    public Button Button;

    public Button OptionButton;

    [SerializeField, Header("文字栏预制体")] public GameObject DialogPrefab;//
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

    public void ReadText(TextAsset textAsset)   //换行分割
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
                   Debug.Log("正在显示"+id );//索引没有问题，但是为什么文本不显示
                    Debug.Log("正在显示" + cells[4]);//文本也拿到了
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
