using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiva : Interactable
{
	private Animator animator;
	private bool hasRestarted;

	public override void Start ()
	{
		base.Start();

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
				CanInteract = false;
				GameManager.GetInstance().RestartedAIVA = true;
				hasRestarted = true;
			}
		}
	}
}
