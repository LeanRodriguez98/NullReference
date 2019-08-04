using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager instance;

    public struct Audio
    {
        public int id;
        public AudioClip clip;
        public string englishSubtitles;
        public string spanishSubtitles;
    }

    private Dictionary<string, Audio> audios;
    public TextAsset sourceCSV;
    private List<string> audioQueque;
    public AudioSource audioSource;
    public Text subtitles;

    private void Awake()
    {
        instance = this;
        audios = new Dictionary<string, Audio>();
        audioQueque = new List<string>();
        LoadSubtitles();
    }

    public void LoadSubtitles()
    {

        string[] data = sourceCSV.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length; i++)
        {
            string[] row = data[i].Split(new char[] { ';' });
            if (row[1] != "")
            {
                Audio a;
                int.TryParse(row[0], out a.id);
                a.clip = (AudioClip)Resources.Load(row[2]);
                a.englishSubtitles = row[3];
                a.spanishSubtitles = row[4];
                audios.Add(row[1], a);
            }
        }
    }

    public Audio GetAudio(string key)
    {
        Audio a;
        a.clip = null;
        a.id = -1;
        a.englishSubtitles = "";
        a.spanishSubtitles = "";
        if (!audios.TryGetValue(key, out a))
        {
            Debug.LogError("Dosen't exist a audio whith key " + key);
        }
        return a;
    }

    public void AddAudioManually(string key, AudioClip clip, string englishSubtitles, string spanishSubtitles)
    {
        Audio a;
        a.clip = clip;
        a.id = audios.Count + 1;
        a.englishSubtitles = englishSubtitles;
        a.spanishSubtitles = spanishSubtitles;
        audios.Add(key, a);
    }

    public void LoadAudioQueque(string[] keys)
    {
        audioQueque.Clear();
        for (int i = 0; i < keys.Length; i++)
        {
            audioQueque.Add(keys[i]);
        }
    }

    void Update()
    {
        if (audioSource)
        {
            if(!audioSource.isPlaying)
            {
                if (audioQueque.Count > 0)
                {
                    Audio aux = GetAudio(audioQueque[0]);
                    audioSource.clip = aux.clip;
                    if(subtitles && subtitles.IsActive())
                    {
                            subtitles.text = aux.englishSubtitles;
                    }
                    audioSource.Play();
                    audioQueque.RemoveAt(0);
                }
                else
                    if(subtitles)
                        subtitles.text = null;
            }
        }
    }
}
