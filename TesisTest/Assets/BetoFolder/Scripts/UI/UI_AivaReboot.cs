using UnityEngine;

public class UI_AivaReboot : MonoBehaviour
{
	[SerializeField] TouchPad touchPad;

	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
		Aiva.OnRestartEvent += OnAivaRebooted;
	}

	void OnAivaRebooted()
	{
		animator.SetTrigger("AivaRebooted");
	}

	public void EnableGlitchEffect()
	{
		GlitchEffect.glitchEffectInstance.DisplayGlitchOn();
	}

	public void CloseSlidingDoor()
	{
		touchPad.Interact();
	}
}
