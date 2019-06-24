using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiva : Interactable
{
	private Animator animator;
	private bool hasRestarted;

	void Start ()
	{
		hasRestarted = false;
		animator = GetComponent<Animator>();
	}

	public override void Interact()
	{
		base.Interact();

		if (!hasRestarted)
		{
			if (GlitchEffect.glitchEffectInstance != null)
			{
				GlitchEffect.glitchEffectInstance.DisplayGlitchOn();
				animator.SetTrigger("AivaRestarted");
				hasRestarted = true;
			}
		}
	}
}
