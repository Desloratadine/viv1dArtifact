using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XLua;
using System.IO;
using UnityEngine.UIElements;

[CreateNodeMenu("DialogContent")]
public class Content : Node //对话节点
{
    [TextArea] public string Info = "这里填关于对话内容的注释";
    public TextAsset DialogueFile;//对话文档，角色名称在里面读取
    //private LuaTable Dialogs;//解析后的数据

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
                Debug.LogError("返回值不是LuaTable类型");
                return;
            }

            Dialogues.Clear();//先清除再载入
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

            Debug.Log($"成功加载{count}条对话");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"解析失败: {e.Message}\n{e.StackTrace}");
        }
    }

}
