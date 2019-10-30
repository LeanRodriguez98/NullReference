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

    private bool soundPlayed = false;
    

    public override void Start()
    {
        base.Start();
        CanInteract = false;
        
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;
    }

    private void Update()
    {
        if (!CanInteract)
        {
            if (GameManager.GetInstance().RestartedAIVA)
            {
                CanInteract = true;
            }
        }
    }

    public override void Interact()
    {
        if (GameManager.GetInstance().RestartedAIVA)
        {
            base.Interact();
            if (!soundPlayed)
            {
                playerVoiceline.SetActive(true);
                GameManager.GetInstance().CoffeeMugFound = true;
                soundPlayed = true;
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

}
