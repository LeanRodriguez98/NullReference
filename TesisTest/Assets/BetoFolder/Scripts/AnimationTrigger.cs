using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
	public string animationTrigger;
	public Animator targetAnimator;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			targetAnimator.SetTrigger(animationTrigger);
	}
}
