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
        public string colorSwapLavel;
        public Substance.Game.SubstanceGraph[] colorSwapGraphs;
        [Space(20)]
        [Header("Menu elements")]
        public GameObject[] mainMenuElements;
        public GameObject[] settingsElements;

        public Dropdown lenguageDropdown;
        public Slider soundsVolumeSlider;
        public Slider voicesVolumeSlider;
        public Toggle lenguageToggle;
        public SO_GameOptions gameOptions;
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

        }
        public void SetMenuShardsAnimations()
        {
            shards = GetComponentsInChildren<MenuShard>();
            foreach (MenuShard shard in shards)
            {
                shard.SetAnimator();
            }
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
}
