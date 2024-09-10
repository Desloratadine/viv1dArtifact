using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class attacksound : MonoBehaviour
{
    public AudioSource soundSource;
    public bool atk = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        atk = Input.GetMouseButton(0);
        play();
    }
    private void play()
    {
        if (atk)
        {
            if (!soundSource.isPlaying)
                soundSource.Play();
        }
        else
            soundSource.Stop();
    }
}
