using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMug : Interactable
{
	public override void Interact()
	{
		base.Interact();
		GameManager.GetInstance().CoffeeMugFound = true;
	}
}
