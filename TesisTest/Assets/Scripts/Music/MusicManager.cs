using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;
    private AudioSource audioSource;
    private ushort index;
    private float volume;
    public List<AudioClip> musics;
    [Range(1, 50)] public uint fadeMilisecondsDelay;
    private int iterations = 0;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        index = 0;
        audioSource.clip = musics[index];
        audioSource.loop = true;

        // ajustar volumen

        volume = audioSource.volume;
        audioSource.Play();
    }

    public void NextSong()
    {
        StartCoroutine(FadeOffVolume());
    }

    private IEnumerator FadeOffVolume()
    {
        while (iterations < fadeMilisecondsDelay)
        {
            audioSource.volume -= (volume / fadeMilisecondsDelay);
            iterations++;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        iterations = 0;
        ChangeSong();
    }

    private void ChangeSong()
    {
        index++;
        if (index < musics.Count)
            audioSource.clip = musics[index];
        audioSource.Play();
        StartCoroutine(FadeOnVolume());
    }

    private IEnumerator FadeOnVolume()
    {
        while (iterations < fadeMilisecondsDelay)
        {
            audioSource.volume += (volume / fadeMilisecondsDelay);
            iterations++;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        iterations = 0;
    }

}
