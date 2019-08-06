using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPad : Interactable
{
	public Animator animator;
	public Material disabledMaterial;
	public Material enabledMaterial;

	private MeshRenderer meshRenderer;
	private bool doorIsOpen;

    private AudioSource audioClip;
    [SerializeField] private AudioSource sliderAudioSource;
	public override void Start()
	{
		base.Start();

		doorIsOpen = false;
		meshRenderer = GetComponent<MeshRenderer>();
		SetMaterial();

        audioClip = GetComponent<AudioSource>();
        audioClip.volume *= PlayerPrefs.GetFloat("VolumeLevel");
        sliderAudioSource.volume *= PlayerPrefs.GetFloat("VolumeLevel");
    }

	public override void Interact()
	{
		base.Interact();

		doorIsOpen = !doorIsOpen;
		SetMaterial();
		animator.SetBool("OpenDoor", !animator.GetBool("OpenDoor"));
        audioClip.Play();
        PlaySliderSound();
	}

	public void SetMaterial()
	{
		if (doorIsOpen)
			meshRenderer.material = enabledMaterial;
		else
			meshRenderer.material = disabledMaterial;
	}

    public void PlaySliderSound()
    {
        if(!sliderAudioSource.isPlaying)
            sliderAudioSource.Play();
    }

}
