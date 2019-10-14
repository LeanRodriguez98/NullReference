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
        public Color subtitleColor;
    }

    private Dictionary<string, Audio> audios;
    public TextAsset sourceCSV;
    private List<string> audioQueque;
    public AudioSource audioSource;
    public Text subtitles;
    private const char AIVA_DialogsFolderFirstChar = 'A';
    private const char PLAYER_DialogsFolderFirstChar = 'P';

    private void Awake()
    {
        instance = this;
        audios = new Dictionary<string, Audio>();
        audioQueque = new List<string>();
        LoadSubtitles();
    }

    private void Start()
    {
        audioSource.volume *= GameManager.GetInstance().gameOptions.voicesVolume;// PlayerPrefs.GetFloat("VolumeLevel");
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
                a.subtitleColor = Color.white;
                int.TryParse(row[0], out a.id);
                a.clip = (AudioClip)Resources.Load(row[2]);
                if (row[2].ToCharArray()[0] == AIVA_DialogsFolderFirstChar)
                    a.subtitleColor = Color.red;
                else if(row[2].ToCharArray()[0] == PLAYER_DialogsFolderFirstChar)
                    a.subtitleColor = Color.blue;
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
            Debug.LogError("There is no audio with key: " + key);
        }
        return a;
    }

    public void AddAudioManually(string key, AudioClip clip, string englishSubtitles, string spanishSubtitles)
    {
        Audio a;
        a.clip = clip;
        a.subtitleColor = Color.white;
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
                    if(GameManager.GetInstance().gameOptions.dilplaySubtitles/*   PlayerPrefs.GetInt("DisplaySubtitles") == 1*/)
                    {
                        if(subtitles && subtitles.IsActive())
                        {
                            if(GameManager.GetInstance().gameOptions.lenguage == (int)GameManager.Lenguges.English /*PlayerPrefs.GetInt("SubtitleLenguage") == 0*/)
                                subtitles.text = aux.englishSubtitles;
                            else
                                subtitles.text = aux.spanishSubtitles;

                            subtitles.color = aux.subtitleColor;
                        }
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
