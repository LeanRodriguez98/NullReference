using System;
using UnityEngine;

public class CoffeeMug : Interactable
{
	public static event Action OnMugFound; 

    public GameObject playerVoiceline;

    public AudioClip grabSound;
    public AudioClip tossSound;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private bool grabState = false;

    private bool soundPlayed = false;
    

    public override void Start()
    {
        base.Start();
        CanInteract = false;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;

		Aiva.OnRestartEvent += SetMugAsInteractable;
    }

	private void SetMugAsInteractable()
	{
        CanInteract = true;
	}

    public override void Interact()
    {
        if (GameManager.GetInstance().RestartedAIVA)
        {
            base.Interact();
            if (!soundPlayed)
            {
                playerVoiceline.SetActive(true);
                soundPlayed = true;

				OnMugFound();
            }
            InteractSound();
        }
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

	private void OnDestroy() 
	{
		Aiva.OnRestartEvent -= SetMugAsInteractable;
	}
}
