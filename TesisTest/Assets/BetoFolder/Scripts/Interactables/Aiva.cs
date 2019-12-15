﻿using System;
using UnityEngine;

public class Aiva : Interactable
{
	public static event Action OnRestartEvent; 

	public GameObject playerVoiceline;
    public AudioSource pcClip;

	private Animator animator;
	private bool hasRestarted;

	public override void Start ()
	{
		base.Start();

		hasRestarted = false;
		animator = GetComponent<Animator>();
        pcClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");
	}

	public override void Interact()
	{
		base.Interact();

		if (!hasRestarted)
		{
			if (GlitchEffect.glitchEffectInstance != null)
			{
				animator.SetTrigger("AivaRestarted");
				CanInteract = false;

				OnRestartEvent();

				hasRestarted = true;

				playerVoiceline.SetActive(true);
			}
		}
	}
}
