using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoicelineTrigger : MonoBehaviour 
{
	public string[] keys;
	public SubtitleManager subtitleManager;
	public float waitForSeconds;
	public bool playOnAwake;
	private void Awake()
	{
		if(playOnAwake)
			Invoke("LoadQueque", waitForSeconds);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && subtitleManager)
		{
			Invoke("LoadQueque", waitForSeconds);
			gameObject.SetActive(false);
		}
				
	}

	private void LoadQueque()
	{
		subtitleManager.LoadAudioQueque(keys);
	}

}
