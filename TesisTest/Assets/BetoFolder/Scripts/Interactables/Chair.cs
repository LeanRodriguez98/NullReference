using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : Interactable
{
	public Animator animator;
	
	public override void Interact()
	{
		base.Interact();
		animator.SetTrigger("Spin");
	}
}
