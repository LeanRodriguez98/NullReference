using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class PlayerSoundTrigger : MonoBehaviour
{
    public AudioClip clip;
    [Range(0.0f, 1.0f)] public float volume;
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (clip != null)
            {
                PlayerSounds.instance.PlayPlayerSound(clip, volume);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogError("Not Asigned a Clip in " + gameObject.name, gameObject);
            }
        }
    }
}
