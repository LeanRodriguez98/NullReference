using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMug : Interactable
{
	public GameObject playerVoiceline;

    public AudioClip grabSound;
    public AudioClip tossSound;
    public AudioClip hitSound;


    private AudioSource audioSource;
    private bool grabState = false;
    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;
    }

    public override void Interact()
	{
		base.Interact();
		playerVoiceline.SetActive(true);
		GameManager.GetInstance().CoffeeMugFound = true;
        InteractSound();

    }


    private void InteractSound()
    {
        grabState = !grabState;
        if (grabState)
            audioSource.clip = grabSound;
        else
            audioSource.clip = tossSound;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.clip = hitSound;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

}
