using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
	public float timeToTrigger;
	public string animationTrigger;
	public bool parameterIsBoolean;
	public bool boolParameter;
	public Animator targetAnimator;
    public AudioSource audioSourceToPlay;


	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (parameterIsBoolean)
				Invoke("SetAnimationBoolean", timeToTrigger);
			else
				Invoke("TriggerAnimation", timeToTrigger);
		}
	}

	private void SetAnimationBoolean()
	{
		targetAnimator.SetBool(animationTrigger, boolParameter);
		if (audioSourceToPlay != null)
		{
			audioSourceToPlay.Play();
		}
	}

	private void TriggerAnimation()
	{
		targetAnimator.SetTrigger(animationTrigger);
        if (audioSourceToPlay != null)
        {
            audioSourceToPlay.Play();
        }
	}
}
