using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
    [HideInInspector] public bool isGrabbed = false;
    private AudioSource audioSource;
    public AudioClip cubeHit;
    public AudioClip cubeHitSructure;
    public AudioClip cubeThrow;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;
    }

    public void PlayThrowSound()
    {
        audioSource.clip = cubeThrow;
        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Structure"))
            audioSource.clip = cubeHitSructure;
        else
            audioSource.clip = cubeHit;

        audioSource.Play();
    }

    public void SetIsGrabbed(bool state)
    {
        isGrabbed = state;
    }
}
