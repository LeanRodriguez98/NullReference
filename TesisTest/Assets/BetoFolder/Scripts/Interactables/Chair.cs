using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Interactable
{
	public Animator animator;
    private AudioSource audioClip;

    public override void Start()
    {
        base.Start();
        audioClip = GetComponent<AudioSource>();
        audioClip.volume *= PlayerPrefs.GetFloat("VolumeLevel");
    }

    public override void Interact()
	{
		base.Interact();
		animator.SetTrigger("Spin");
        if(!audioClip.isPlaying)
            audioClip.Play();
	}
}
