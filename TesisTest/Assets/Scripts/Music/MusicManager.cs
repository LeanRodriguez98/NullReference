using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;
    private ushort index;
    private float volume;

    //public List<AudioClip> musics;
    [Range(1, 50)] public uint fadeMilisecondsDelay;

    [System.Serializable]
    public struct Music
    {
        [HideInInspector] public AudioSource audioSource;
        public AudioClip clip;
        public float volume;
    }
    public Music[] musics;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < musics.Length; i++)
        {
            musics[i].audioSource = gameObject.AddComponent<AudioSource>();
            musics[i].audioSource.clip = musics[i].clip;
            musics[i].audioSource.loop = true;
            musics[i].volume *= 1;//<---- ajustar volumen
            musics[i].audioSource.volume = 0.0f;
            musics[i].audioSource.playOnAwake = false;
        }
        index = 0;
        musics[index].audioSource.volume = musics[index].volume;
        musics[index].audioSource.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextSong();
        }
    }

    public void NextSong()
    {
        StartCoroutine(ChangeSong());
    }

    private IEnumerator ChangeSong()
    {
        if (musics.Length > index+1)
        {
            bool a = false;
            bool b = false;
            while (!a || !b)
            {
                musics[index + 1].audioSource.Play();
                musics[index].audioSource.volume -= (musics[index].volume / fadeMilisecondsDelay);
                if (musics[index].audioSource.volume <= 0.0f)
                {
                    musics[index].audioSource.volume = 0.0f;
                    musics[index].audioSource.Stop();
                    a = true;
                }
                musics[index + 1].audioSource.volume += (musics[index + 1].volume / fadeMilisecondsDelay);
                if (musics[index + 1].audioSource.volume >= musics[index + 1].volume)
                {
                    musics[index + 1].audioSource.volume = musics[index + 1].volume;
                    b = true;
                }

                if (!a || !b)
                {
                    yield return new WaitForSeconds(0.1f);
                    a = false;
                    b = false;
                }
            }
            index++;
        }
    }


}
