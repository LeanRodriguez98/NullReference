using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnableAndDisableSound : MonoBehaviour
{
    [System.Serializable]
    public struct Audio
    {
        public AudioClip clip;
        public float volume;
    }
   
    public Audio enableSound;
    public Audio disableSound;

    private void OnEnable()
    {
        NewSound(enableSound);
    }

    private void OnDisable()
    {
        NewSound(disableSound);
    }

    public void NewSound(Audio audio)
    {
        GameObject go = new GameObject();
        go.transform.position = transform.position;
        AudioSource a = go.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.loop = false;
        a.spatialBlend = 1.0f;
        a.clip = audio.clip;
        a.volume = audio.volume;
        a.Play();
        Destroy(go, audio.clip.length);
    }
}
