using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenDoorTrigger : MonoBehaviour
{
	public GameObject playerVoiceline;
    public AudioSource doorAudioSource;
	private Animator kitchenDoorAnimator;
	private GameManager gameManager;
	private bool playerCanOpenDoor;

	private bool coffeeMugFound;
	private bool aivaHasBeenRestarted;

	private void Start()
	{
		kitchenDoorAnimator = GetComponent<Animator>();
		gameManager = GameManager.GetInstance();
        doorAudioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;

		coffeeMugFound = false;
		aivaHasBeenRestarted = false;

		CoffeeMug.OnMugFound += CoffeeMugWasFound;
    }

	private void OnTriggerEnter(Collider other)
	{
		playerCanOpenDoor = coffeeMugFound && aivaHasBeenRestarted;
		if (other.CompareTag("Player") && playerCanOpenDoor)
		{
			kitchenDoorAnimator.SetTrigger("OpenDoor");
            doorAudioSource.Play();
            playerVoiceline.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
		}
	}

	private void CoffeeMugWasFound()
	{
		coffeeMugFound = true;
	}

	private void AivaHasBeenRestarted()
	{
		aivaHasBeenRestarted = true;
	}

	private void OnDestroy() 
	{
		CoffeeMug.OnMugFound -= CoffeeMugWasFound;
	}
}
