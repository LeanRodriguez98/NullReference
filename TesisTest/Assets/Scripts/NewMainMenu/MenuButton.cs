using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {

    protected AudioSource audioSource;
    private float defaultVolume;
    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultVolume = audioSource.volume;
    }

    public void SetButtonsVolume(float _volume)
    {
        audioSource.volume = _volume * defaultVolume;
    }
}
