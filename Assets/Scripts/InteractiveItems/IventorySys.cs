//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using XLua;
//public class IventorySys : MonoBehaviour
//{
//    public LuaEnv luaEnv;
//    private LuaTable LuaItem;
//    public TextAsset config;
//    private void Start()
//    {
//        luaEnv.DoString(config.text);
//        LuaItem = luaEnv.Global.Get<LuaTable>("items");
//    }
//    public LuaTable GetById(string id)
//    {
//        return LuaItem.Get<LuaTable>("by_id").Get<LuaTable>(id);
//    }
//}
