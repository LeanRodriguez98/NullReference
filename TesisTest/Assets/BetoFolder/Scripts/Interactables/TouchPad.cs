using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : Interactable
{
	public Animator animator;

	public override void Interact()
	{
		base.Interact();
		animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
	}
}
