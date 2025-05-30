using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XLua;
using System.IO;
using UnityEngine.UIElements;

[CreateNodeMenu("DialogOption")]
public class Options : Node   //Ñ¡Ôñ
{
    [Output(dynamicPortList = true)] public List<string> options;
    [Input] public float In;
}