using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAI_SpotLight : RaycastTriggerEvent
{
	public GameObject congratsWallMsg;
	public GameObject exitDoor;

	private Transform playerTransform;
	private BoxCollider boxCollider;

	enum LightStates
	{
		LookingAtPlayer,
		PointingAtCongratsText,
		PointingTowardsExit,
		PointingTowardsLeaver
	}
	private LightStates lightState;

	private void Start()
	{
		lightState = LightStates.LookingAtPlayer;
		playerTransform = GameManager.GetInstance().player.transform;
		boxCollider = GetComponent<BoxCollider>();
	}

	void Update ()
	{
		UpdateState();
	}

	void UpdateState()
	{
		switch (lightState)
		{
			case LightStates.LookingAtPlayer:
				LookAtPlayer();
				break;
			case LightStates.PointingAtCongratsText:
				break;
			case LightStates.PointingTowardsExit:
				break;
			case LightStates.PointingTowardsLeaver:
				break;
		}
	}

	void LookAtPlayer()
	{
		transform.forward = playerTransform.position - transform.position;
	}

	public override void TriggerEvent()
	{
		base.TriggerEvent();

		Debug.Log("Player looked at light");
		boxCollider.enabled = false;
		lightState = LightStates.PointingAtCongratsText;
	}
}
