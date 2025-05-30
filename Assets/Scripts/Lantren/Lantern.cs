using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Lantern : MonoBehaviour
{
    public int FillBar = 10;
    public int Amount=0;
    public UnityEvent Add;
    public Image Bar;
    private void Awake()
    {
        if (Add == null)
            Add = new UnityEvent();
    }
    void Start()
    {
        Add.AddListener(AddMaterial);
        Add.AddListener(UpdateUIBar);
    }

    void Update()
    {
        
    }
    public void AddMaterial()
    {
        if (Amount < FillBar)
            Amount++;
        else Debug.Log("ย๚มห");

    }
    public void CleanMaterial()
    {
        Amount = 0;
    }
    public void RemoveMaterial()
    {
        if (Amount > 0)
            Amount--;
    }
    public void UpdateUIBar()
    {
        Bar.fillAmount = Amount / FillBar;
    }
}
