using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVA_RandomVoices : MonoBehaviour
{

    public string[] keys;
    public float timeBetweenTrys;
    public float tryProbability;
    void Start()
    {
        Invoke("TryPlayVoiceline", timeBetweenTrys);
    }



    public void TryPlayVoiceline()
    {
        if (UnityEngine.Random.Range(0.0f, 100f) < tryProbability)
        {
            PlayVoiceline();
        }
        Invoke("TryPlayVoiceline", timeBetweenTrys);
    }

    public void PlayVoiceline()
    {
        List<string> voice = new List<string>();
        voice.Add(keys[UnityEngine.Random.Range(0, keys.Length)]);
        SubtitleManager.instance.LoadAudioQueque(voice.ToArray());
        BetoScripts.UI_Player.GetInstance().SetAivaUIDisplay(true);
        Invoke("AivaUIDisplayOff", SubtitleManager.instance.GetAudioByKey(voice[0]).clip.length);
    }

    public void AivaUIDisplayOff()
    {
        BetoScripts.UI_Player.GetInstance().SetAivaUIDisplay(false);
    }
}
