﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : Interactable
{
	public Animator m_doorAnimator;
	public string m_animationTriggerName;
	public Material m_buttonPressedMaterial;

	public override void Interact()
	{
		Debug.Log("Button Pressed");
		MeshRenderer mr = GetComponent<MeshRenderer>();
		mr.material = m_buttonPressedMaterial;
		m_doorAnimator.SetTrigger(m_animationTriggerName);
	}
}
