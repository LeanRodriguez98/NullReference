using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
	private bool optionsMenu = false;
	private bool subtitles = false;
	public Dropdown lenguageDropdown;
	public Slider volumeSlider;
	public GameObject[] onOptionMenuEnable;
	public GameObject[] onOptionMenuDisable;

	private void Start()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		PlayerPrefs.SetInt("DisplaySubtitles", 0);
		PlayerPrefs.SetInt("SubtitleLenguage", lenguageDropdown.value);
		PlayerPrefs.SetFloat("VolumeLevel", volumeSlider.value);
	}

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void OptionsMenuToggle()
	{
		optionsMenu = !optionsMenu;
			
			for(int i = 0; i < onOptionMenuEnable.Length; i++)
			{
				onOptionMenuEnable[i].SetActive(optionsMenu);
			}
			for(int i = 0; i < onOptionMenuDisable.Length; i++)
			{
				onOptionMenuDisable[i].SetActive(!optionsMenu);
			}
	}

	public void SetSubtitles()
	{
		subtitles = !subtitles;
		
		if(subtitles)
			PlayerPrefs.SetInt("DisplaySubtitles", 1);
		else
		{
			PlayerPrefs.SetInt("DisplaySubtitles", 0);
			PlayerPrefs.SetInt("SubtitleLenguage", 0);
		}
	}

	public void SetSubtitlesLenguage()
	{
		PlayerPrefs.SetInt("SubtitleLenguage", lenguageDropdown.value);
	}

	public void SetVolumeLevel()
	{
		PlayerPrefs.SetFloat("VolumeLevel", volumeSlider.value);
	}
}
