using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XLua;
using System.IO;
using UnityEngine.UIElements;

[CreateNodeMenu("DialogContent")]
public class Content : Node //�Ի��ڵ�
{
    [TextArea] public string Info = "��������ڶԻ����ݵ�ע��";
    public TextAsset DialogueFile;//�Ի��ĵ�����ɫ�����������ȡ
    //private LuaTable Dialogs;//�����������

    [Input] public float In;
    [Output] public float Out;

    public LuaEnv luaEnv;
    public List<string> Dialogues = new List<string>();

    public void ReadFile()
    {
        if (luaEnv == null) luaEnv = new LuaEnv();

        try
        {
            object[] result = luaEnv.DoString(DialogueFile.text, "Dialogue");
            LuaTable dialogueTable = result[0] as LuaTable;
            if (dialogueTable == null)
            {
                Debug.LogError("����ֵ����LuaTable����");
                return;
            }

            Dialogues.Clear();//�����������
            int count = dialogueTable.Length;
            for (int i = 1; i <= count; i++)
            {
                LuaTable line = dialogueTable.Get<int, LuaTable>(i);
                string name = line.Get<string, string>("name");
                string img = line.Get<string, string>("img");
                string imgPos=line.Get<string, string>("pos");
                string text = line.Get<string, string>("text");
                Dialogues.Add($"{name}:{img}:{imgPos}:{text}");
            }

            Debug.Log($"�ɹ�����{count}���Ի�");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"����ʧ��: {e.Message}\n{e.StackTrace}");
        }
    }

}
