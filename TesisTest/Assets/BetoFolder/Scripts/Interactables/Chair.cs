using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Interactable
{
	public Animator animator;
    private AudioSource audioSource;

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;// PlayerPrefs.GetFloat("VolumeLevel");
    }

    public override void Interact()
	{
		base.Interact();
		animator.SetTrigger("Spin");
        if(!audioSource.isPlaying)
            audioSource.Play();
	}
}