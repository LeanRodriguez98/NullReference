using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour {

    public static PlayerSounds instance;
    private AudioSource audioSource;
    private void Awake()
    {
        instance = this;
    }
    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
	}
	
	public void PlayPlayerSound(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
}
