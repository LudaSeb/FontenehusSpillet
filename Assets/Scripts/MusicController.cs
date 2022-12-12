using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    Slider volumeControl;
    
    void Start()
    {
        volumeControl = GetComponent<Slider>();
    }

    public void ChangeVolume()
    {
        audioMixer.SetFloat("MasterVolume", volumeControl.value);
    }
}
