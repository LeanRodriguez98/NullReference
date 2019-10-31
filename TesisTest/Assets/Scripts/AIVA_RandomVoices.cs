using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVA_RandomVoices : MonoBehaviour {

    public string[] keys;
    public float timeBetweenTrys;
    public float tryProbability;
	void Start ()
    {
        Invoke("TryPlayVoiceline", timeBetweenTrys);
	}

    

    public void TryPlayVoiceline()
    {
        if (UnityEngine.Random.Range(0.0f, tryProbability) < 100f)
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
    }	
}
