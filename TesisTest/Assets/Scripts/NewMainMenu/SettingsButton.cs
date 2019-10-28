using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
	public class SettingsButton : MonoBehaviour
	{
		public float delay;

		private bool clicked = false;
        private AudioSource audioSource;
        //private bool canPlayAudio = true;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }
        private void OnEnable()
        {
            clicked = false;
        }

        private void OnMouseEnter()
        {
                audioSource.Play();
           // if (!audioSource.isPlaying && canPlayAudio)
           // {
           //     canPlayAudio = false;
           // }
            
        }

        private void OnMouseOver()
        {

        }
        private void OnMouseExit()
        {
            //canPlayAudio = true;

        }

        void OnMouseDown()
		{
			if (!clicked)
			{
				Invoke("DisplaySettings", delay);
                GlitchEffect.glitchEffectInstance.DisplayGlitchOn();
				clicked = true;
			}
		}

		private void DisplaySettings()
		{
			MainMenu.instace.DisplaySettings(true);
		}
	}
}
