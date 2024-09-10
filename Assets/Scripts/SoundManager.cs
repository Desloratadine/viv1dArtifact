using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÷ª  ∫œ∂Ã“Ù–ß
/// </summary>
[Serializable]
public class sound
{
    public AudioClip pakage;
    public AudioClip gethurt;
    public AudioClip gettool;
    public AudioClip walk;
}
public class SoundManager : MonoBehaviour
{
    public sound sound ;
    public hp hp;
    public AudioSource soundSource;
    private GameObject cat;
    void Start()
    {
        cat = GameObject.FindWithTag("Player");
        hp = cat.GetComponent<CharacterController>().hp;
    }

    // Update is called once per frame
    void Update()
    {
        getsound();
    }
    private void playsound()
    {
        soundSource.Play();
    }
    private void getsound()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            soundSource.clip = sound.pakage;
            playsound();
        }
        if (hp.hurt)
        {
            soundSource.clip = sound.gethurt;
            playsound();
            hp.hurt = !hp.hurt;
        }
        if (cat.GetComponent<bag>().get)
        {
            soundSource.clip = sound.gettool;
            playsound();
            cat.GetComponent<bag>().get = false;
        }

    }
}
