using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour 
{
    [HideInInspector] public bool isGrabbed = false;
    private AudioSource audioSource;
    [System.Serializable]
    public struct Audio
    {
        public AudioClip clip;
        public float volume;

    }

    public Audio leavingCube;
    public Audio cubeHit;
    public Audio cubeHitSructure;
    public Audio cubeThrow;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume *= GameManager.GetInstance().gameOptions.soundsVolume;
    }

    public void PlayThrowSound()
    {
        audioSource.clip = cubeThrow.clip;
        audioSource.volume = cubeThrow.volume;
        audioSource.Play();
    }

    public void PlayLeavingSound()
    {
        audioSource.clip = leavingCube.clip;
        audioSource.volume = cubeThrow.volume;

        audioSource.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Structure"))
        { 
            audioSource.clip = cubeHitSructure.clip;
            audioSource.volume = cubeHitSructure.volume;
        }
        else
        { 
            audioSource.clip = cubeHit.clip;
            audioSource.volume = cubeHitSructure.volume;
        }
        audioSource.Play();
    }

    public void SetIsGrabbed(bool state)
    {
        isGrabbed = state;
    }
}
