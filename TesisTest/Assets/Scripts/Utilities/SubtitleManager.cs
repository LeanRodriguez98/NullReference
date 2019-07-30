using System.Collections.Generic;
using UnityEngine;

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
    private void Awake()
    {
        instance = this;
        audios = new Dictionary<string, Audio>();
        LoadSubtitles();
    }

    public void LoadSubtitles()
    {

        string[] data = sourceCSV.text.Split(new char[] { '\n' });
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] row = data[i].Split(new char[] { ',' });
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
}
