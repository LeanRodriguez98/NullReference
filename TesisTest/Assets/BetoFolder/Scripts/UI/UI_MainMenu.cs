using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
  

	private bool optionsMenu = false;
	private bool subtitles = false;
	public Dropdown lenguageDropdown;
	public Slider soundsVolumeSlider;
	public Slider voicesVolumeSlider;
    public Toggle lenguageToggle;
	public GameObject[] onOptionMenuEnable;
	public GameObject[] onOptionMenuDisable;
    public SO_GameOptions gameOptions;
    

	private void Start()
	{
        Utilities.LoadGame(gameOptions);
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		//PlayerPrefs.SetInt("DisplaySubtitles", 0);
        lenguageToggle.isOn = gameOptions.dilplaySubtitles;
		//PlayerPrefs.SetInt("SubtitleLenguage", lenguageDropdown.value);
        lenguageDropdown.value = gameOptions.lenguage;
		//PlayerPrefs.SetFloat("VolumeLevel", volumeSlider.value);
        soundsVolumeSlider.value = gameOptions.soundsVolume;
        voicesVolumeSlider.value = gameOptions.voicesVolume;

    }

	public void LoadScene(string sceneName)
	{
		Utilities.LoadScene(sceneName);
	}

	public void Quit()
	{
        Utilities.ExitGame();
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

        if (subtitles)
        {
          //  PlayerPrefs.SetInt("DisplaySubtitles", 1);
            gameOptions.dilplaySubtitles = true;
        }
        else
        {
            //PlayerPrefs.SetInt("DisplaySubtitles", 0);
            gameOptions.dilplaySubtitles = false;
            //PlayerPrefs.SetInt("SubtitleLenguage", 0);
            gameOptions.lenguage = (int)GameManager.Lenguges.English;
        }
        Utilities.SaveGame(gameOptions);
    }

	public void SetSubtitlesLenguage()
	{
		//PlayerPrefs.SetInt("SubtitleLenguage", lenguageDropdown.value);
        gameOptions.lenguage = lenguageDropdown.value;
        Utilities.SaveGame(gameOptions);

    }

    public void SetSoundsVolumeLevel()
	{
		//PlayerPrefs.SetFloat("VolumeLevel", volumeSlider.value);
        gameOptions.soundsVolume = soundsVolumeSlider.value;
        Utilities.SaveGame(gameOptions);

    }

    public void SetVoicesVolumeLevel()
    {
        gameOptions.voicesVolume = voicesVolumeSlider.value;
        Utilities.SaveGame(gameOptions);

    }
}
