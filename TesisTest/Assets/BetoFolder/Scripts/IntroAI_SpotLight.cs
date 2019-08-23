using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAI_SpotLight : RaycastTriggerEvent
{
	public GameObject playerAivaDialogue;
	public GameObject confettiParticles;
	public GameObject exitDoorCollider;
	public GameObject leaverSpotlight;
	public GameObject leaverSideBound;
    public Vector3 rotationTarget;
    public float interpolationTime;

	private Transform playerTransform;
	private Animator animator;
    private bool playerLookedAtLight;

	private void Start()
	{
		playerTransform = GameManager.GetInstance().player.transform;

		animator = GetComponent<Animator>();
		animator.enabled = false;
        playerLookedAtLight = false;
	}

	private void Update()
	{
		if (!animator.enabled && !playerLookedAtLight)
        {
			LookAtPlayer();
        }
        else if (playerLookedAtLight)
        {
            Quaternion targetRotation = Quaternion.Euler(rotationTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, interpolationTime);
            if (transform.eulerAngles == targetRotation.eulerAngles)
            {
                animator.enabled = true;
                animator.SetTrigger("LookAtWall");
            }
        }
        else
        {
			playerAivaDialogue.SetActive(true);
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
