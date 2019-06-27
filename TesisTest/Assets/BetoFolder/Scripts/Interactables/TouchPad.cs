using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : Interactable
{
	public Animator animator;
	public Material disabledMaterial;
	public Material enabledMaterial;

	private MeshRenderer meshRenderer;
	private bool doorIsOpen;

	public override void Start()
	{
		base.Start();

		doorIsOpen = false;
		meshRenderer = GetComponent<MeshRenderer>();
		SetMaterial();
	}

	public override void Interact()
	{
		base.Interact();

		doorIsOpen = !doorIsOpen;
		SetMaterial();
		animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
	}

	public void SetMaterial()
	{
		if (doorIsOpen)
			meshRenderer.material = enabledMaterial;
		else
			meshRenderer.material = disabledMaterial;
	}

}
