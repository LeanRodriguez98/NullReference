using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorTrigger : MonoBehaviour
{
	public GameObject playerVoiceline;

	private Animator kitchenDoorAnimator;
	private GameManager gameManager;
	private bool playerCanOpenDoor;

	private void Start()
	{
		kitchenDoorAnimator = GetComponent<Animator>();
		gameManager = GameManager.GetInstance();
	}

	private void OnTriggerEnter(Collider other)
	{
		playerCanOpenDoor = gameManager.CoffeeMugFound && gameManager.RestartedAIVA;
		if (other.CompareTag("Player") && playerCanOpenDoor)
		{
			kitchenDoorAnimator.SetTrigger("OpenDoor");
			playerVoiceline.SetActive(true);
		}
	}
}
