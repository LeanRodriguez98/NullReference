using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAI_SpotLight : RaycastTriggerEvent
{
	public GameObject confettiParticles;
	public GameObject exitDoorCollider;
	public GameObject leaverSpotlight;
	public GameObject leaverSideBound;

	private Transform playerTransform;
	private Animator animator;

	private void Start()
	{
		playerTransform = GameManager.GetInstance().player.transform;

		animator = GetComponent<Animator>();
		animator.enabled = false;
	}

	private void Update()
	{
		if (!animator.enabled)
			LookAtPlayer();
	}

	private void LookAtPlayer()
	{
		transform.forward = playerTransform.position - transform.position;
	}

	public override void TriggerEvent()
	{
		base.TriggerEvent();

		animator.enabled = true;
		animator.SetTrigger("LookAtWall");
	}

	public void EnableConfetti()
	{
		confettiParticles.SetActive(true);
	}

	public void DisableExitCollider()
	{
		exitDoorCollider.SetActive(false);
	}

	public void EnableLeaverSpotlight()
	{
		leaverSpotlight.SetActive(true);
	}

	public void DisableLeaverBound()
	{
		leaverSideBound.SetActive(false);
	}
}
