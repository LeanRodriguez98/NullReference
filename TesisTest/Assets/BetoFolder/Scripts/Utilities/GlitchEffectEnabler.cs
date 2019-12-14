using UnityEngine;

public class GlitchEffectEnabler : MonoBehaviour 
{
	[SerializeField] float enableAfterSeconds;
	[SerializeField] float glitchDuration;
	[SerializeField] bool playOnAwake;

	void Start()
	{
		if (playOnAwake)
			Invoke("EnableGlitchEffect", enableAfterSeconds);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Player"))
			Invoke("EnableGlitchEffect", enableAfterSeconds);
	}

	void EnableGlitchEffect()
	{
		GlitchEffect.glitchEffectInstance.DisplayGlitchOn(glitchDuration);
	}
}
