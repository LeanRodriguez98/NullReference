using UnityEngine;

public class GlitchEffectEnabler : MonoBehaviour 
{
	[SerializeField] float enableAfterSeconds;
	[SerializeField] float glitchDuration;

	void Start()
	{
		Invoke("EnableGlitchEffect", enableAfterSeconds);
	}

	void EnableGlitchEffect()
	{
		GlitchEffect.glitchEffectInstance.DisplayGlitchOn(glitchDuration);
	}
}
