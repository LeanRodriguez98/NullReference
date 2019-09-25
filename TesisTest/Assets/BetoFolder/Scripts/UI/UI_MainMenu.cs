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
        lenguageToggle.isOn = gameOptions.dilplaySubtitles;
        lenguageDropdown.value = gameOptions.lenguage;
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
            gameOptions.dilplaySubtitles = true;
        }
        else
        {
            gameOptions.dilplaySubtitles = false;
            gameOptions.lenguage = (int)GameManager.Lenguges.English;
        }
        Utilities.SaveGame(gameOptions);
    }

	public void SetSubtitlesLenguage()
	{
        gameOptions.lenguage = lenguageDropdown.value;
        Utilities.SaveGame(gameOptions);

    }

    public void SetSoundsVolumeLevel()
	{
        gameOptions.soundsVolume = soundsVolumeSlider.value;
        Utilities.SaveGame(gameOptions);

    }

    public void SetVoicesVolumeLevel()
    {
        gameOptions.voicesVolume = voicesVolumeSlider.value;
        Utilities.SaveGame(gameOptions);

    }
}
