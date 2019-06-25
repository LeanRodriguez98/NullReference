using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratulationsSign : RaycastTriggerEvent
{
	public Animator spotLightAnimator;

	public override void TriggerEvent()
	{
		base.TriggerEvent();
		spotLightAnimator.SetTrigger("LookAtExit");
	}
}
