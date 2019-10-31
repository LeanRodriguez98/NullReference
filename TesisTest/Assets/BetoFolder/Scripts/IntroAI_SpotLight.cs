﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAI_SpotLight : RaycastTriggerEvent
{
	public Animator exitDoorWallAnimator;
	public Animator leaverWallAnimator;

	public GameObject playerAivaDialogue;
	public GameObject confettiParticles;
	public GameObject leaverSpotlight;
    public Vector3 rotationTarget;

    [Range(0.01f,2.0f)]
    public float interpolationTime;

	private Transform playerTransform;
	private Animator animator;
    private bool playerLookedAtLight;
    private AudioSource wallsAudioSource;
	private void Start()
	{
		playerTransform = GameManager.GetInstance().player.transform;

		animator = GetComponent<Animator>();
		animator.enabled = false;
        playerLookedAtLight = false;
        wallsAudioSource = GetComponent<AudioSource>();
        wallsAudioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;
	}

    

    private void Update()
	{
        if (!animator.enabled)
        {
            Quaternion targetRotation = Quaternion.Euler(rotationTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, interpolationTime);
           // Vector3Int a = new Vector3Int((int)transform.eulerAngles.x, (int)transform.eulerAngles.y, (int)transform.eulerAngles.z);
           // Vector3Int b = new Vector3Int((int)rotationTarget.x, (int)rotationTarget.y, (int)rotationTarget.z);
            if (transform.rotation == targetRotation)
            {
                animator.enabled = true;
                animator.SetTrigger("LookAtWall");
            }
        }
        else if(!playerAivaDialogue.activeSelf)
        {
			playerAivaDialogue.SetActive(true);
			BetoScripts.UI_Player.GetInstance().SetAivaUIDisplay(true);
		}
	}

	private void LookAtPlayer()
	{
		transform.forward = playerTransform.position - transform.position;
	}

	public override void TriggerEvent()
	{
		base.TriggerEvent();
        playerLookedAtLight = true;
	}

	public void EnableConfetti()
	{
		confettiParticles.SetActive(true);
	}

	public void LowerLeaverWall()
	{
		leaverWallAnimator.SetTrigger("LowerWall");
        wallsAudioSource.Play();

    }

	public void LowerExitDoorWall()
	{
		exitDoorWallAnimator.SetTrigger("LowerWall");
        wallsAudioSource.Play();
    }

    public void EnableLeaverSpotlight()
	{
		leaverSpotlight.SetActive(true);


	    // TODO: Make Aiva UI animation respond to Aiva's audio file length
		Invoke("HideAivaUI", 18.0f);
	}

	void HideAivaUI()
	{
		BetoScripts.UI_Player.GetInstance().SetAivaUIDisplay(false);
	}
}
