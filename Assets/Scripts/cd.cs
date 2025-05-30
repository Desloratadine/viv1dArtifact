using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class cd : MonoBehaviour
{
    public GameObject image;
    public float coolTime;
    private GameObject tool;
    public bool _refresh = false;
    public bool _clean = false;
    public bool _fill = false;
    public bool _minus = false;
    void Start()
    {
        //tool = gameObject.GetComponentInParent<CharacterState>().tools.CurrentTool;
        //coolTime = tool.GetComponent<CombatSystem>()._tool.cooltme;

    }

     void Update()
    {
        if (_fill)  fill();
        if (_minus) minus();

        if (gameObject.GetComponentInParent<CharacterState>().tools.CurrentTool != tool)
        {
            tool = gameObject.GetComponentInParent<CharacterState>().tools.CurrentTool;
            coolTime = tool.GetComponent<CombatSystem>()._tool.cooltme;
        }

    }
    /// <summary>
    /// 重置
    /// </summary>
    public void refresh()
    {
        image.GetComponentInChildren<Image>().fillAmount = 1;

        
    }


    /// <summary>
    /// 归零
    /// </summary>
    public void clean()
    {
        image.GetComponentInChildren<Image>().fillAmount = 0;

        
    }


    /// <summary>
    /// 填充技能条
    /// </summary>
    public void fill()
    {
        coolTime = tool.GetComponent<CombatSystem>()._tool.cooltme;
        image.GetComponentInChildren<Image>().fillAmount += (2f*Time.deltaTime) / coolTime;
        _refresh = false;
        _minus = false;
        _clean=false;
    }


    /// <summary>
    /// 减少技能条
    /// </summary>
    public void minus()
    {
        //coolTime = tool.GetComponent<CombatSystem>()._tool.cooltme;
        //if (image.GetComponentInChildren<Image>().fillAmount == 0) refresh();
        image.GetComponentInChildren<Image>().fillAmount -= Time.deltaTime / coolTime;
        _refresh = false;
        _clean = false;
        _fill=false;
        
    }
}
