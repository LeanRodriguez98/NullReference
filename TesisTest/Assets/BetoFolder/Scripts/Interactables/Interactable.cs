using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public bool CanInteract { get; set; }

	public virtual void Start()
	{
		CanInteract = true;
	}
	public virtual void Interact() { }

}
