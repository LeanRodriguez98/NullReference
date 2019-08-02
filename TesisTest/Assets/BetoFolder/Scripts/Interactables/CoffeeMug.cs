using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMug : Interactable
{
	public GameObject playerVoiceline;
	public override void Interact()
	{
		base.Interact();
		playerVoiceline.SetActive(true);
		GameManager.GetInstance().CoffeeMugFound = true;
	}
}
