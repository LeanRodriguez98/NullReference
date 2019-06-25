﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
	public float timeToTrigger;
	public string animationTrigger;
	public Animator targetAnimator;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			Invoke("TriggerAnimation", timeToTrigger);
	}

	private void TriggerAnimation()
	{
		targetAnimator.SetTrigger(animationTrigger);
	}
}
