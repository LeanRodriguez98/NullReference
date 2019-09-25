using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
	public GameObject settingsMenu;
	public GameObject[] objectsToHide;

	public Dropdown lenguageDropdown;
	public Slider soundsVolumeSlider;
	public Slider voicesVolumeSlider;
    public Toggle lenguageToggle;
    public SO_GameOptions gameOptions;

	private bool settingsMenuVisible;
	private bool subtitles = false;

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

		settingsMenuVisible = false;
		settingsMenu.SetActive(false);
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
		settingsMenuVisible = !settingsMenuVisible;
		settingsMenu.SetActive(settingsMenuVisible);

		for (int i = 0; i < objectsToHide.Length; i++)
			objectsToHide[i].SetActive(!settingsMenuVisible);
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
