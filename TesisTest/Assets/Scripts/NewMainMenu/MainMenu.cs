using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NewMainMenu
{
    public class MainMenu : MonoBehaviour
    {
        #region Singleton
        public static MainMenu instace;
        private void Awake()
        {
            instace = this;
        }
        #endregion
        [SerializeField] [HideInInspector] private MenuShard[] shards;
        [SerializeField] [HideInInspector] private MenuButton[] buttons;
        public string colorSwapLavel;
        public Substance.Game.SubstanceGraph[] colorSwapGraphs;
        [Space(20)]
        [Header("Music Source")]
        public AudioSource musicSource;
        private float defaultMusicValue;
        [Space(20)]
        [Header("Menu elements")]
        public GameObject[] mainMenuElements;
        public GameObject[] settingsElements;

        public Dropdown lenguageDropdown;
        public Slider soundsVolumeSlider;
        public Slider voicesVolumeSlider;
        public Slider musicVolumeSlider;
        public Toggle lenguageToggle;
        public SO_GameOptions gameOptions;
        private bool subtitles = false;
        private bool settingsDone = false;
        private void Start()
        {
            Utilities.LoadGame(gameOptions);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            lenguageToggle.isOn = gameOptions.dilplaySubtitles;
            lenguageDropdown.value = gameOptions.lenguage;
            soundsVolumeSlider.value = gameOptions.soundsVolume;
            voicesVolumeSlider.value = gameOptions.voicesVolume;
            musicVolumeSlider.value = gameOptions.musicVolume;
            defaultMusicValue = musicSource.volume;
            musicSource.volume *= gameOptions.musicVolume;
            settingsDone = true;
        }
        public void SetMenuShardsAnimations()
        {
            shards = GetComponentsInChildren<MenuShard>();
            foreach (MenuShard shard in shards)
            {
                shard.SetAnimator();
            }
        }

        public void SetButtonsData()
        {
            buttons = FindObjectsOfType<MenuButton>();
        }

        public void OnButtonClicked()
        {
            for (int i = 0; i < colorSwapGraphs.Length; i++)
                colorSwapGraphs[i].SetInputFloat(colorSwapLavel, 0.0f);

            foreach (MenuShard shard in shards)
            {
                shard.FullRotate();
            }
        }

        public void DisplaySettings(bool _state)
        {
            foreach (GameObject go in settingsElements)
            {
                if (go != null)
                {

                    go.SetActive(_state);
                }
            }

            foreach (GameObject go in mainMenuElements)
            {
                if (go != null)
                {

                    go.SetActive(!_state);
                }
            }
        }

        public void SetSubtitles()
        {
            if (settingsDone)
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

        }

        public void SetSubtitlesLenguage()
        {
            if (settingsDone)
            {
                gameOptions.lenguage = lenguageDropdown.value;
                Utilities.SaveGame(gameOptions);
            }
        }

        public void SetSoundsVolumeLevel()
        {
            if (settingsDone)
            {
                gameOptions.soundsVolume = soundsVolumeSlider.value;
                Utilities.SaveGame(gameOptions);
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].SetButtonsVolume(soundsVolumeSlider.value);
                }

                for (int i = 0; i < shards.Length; i++)
                {
                    shards[i].SetShardVolume(soundsVolumeSlider.value);
                }

                GlitchEffect.glitchEffectInstance.SetVolume(soundsVolumeSlider.value);
            }
        }

        public void SetVoicesVolumeLevel()
        {
            if (settingsDone)
            {
                gameOptions.voicesVolume = voicesVolumeSlider.value;
                Utilities.SaveGame(gameOptions);
            }
        }

        public void SetMusicVolumeLevel()
        {
            if (settingsDone)
            {
                gameOptions.musicVolume = musicVolumeSlider.value;
                Utilities.SaveGame(gameOptions);
                musicSource.volume = defaultMusicValue * musicVolumeSlider.value;
            }
        }
    }
}
