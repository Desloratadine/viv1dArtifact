using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundvolume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource soundSource;
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        soundSource.volume = volumeSlider.value;
    }
}
