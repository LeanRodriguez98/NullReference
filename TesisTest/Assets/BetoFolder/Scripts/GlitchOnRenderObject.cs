using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchOnRenderObject : RaycastTriggerEvent 
{
	public float glitchDuration;

	public override void TriggerEvent()
	{
		GlitchEffect.glitchEffectInstance.DisplayGlitchOn(glitchDuration);
		Destroy(this);
	}
}
