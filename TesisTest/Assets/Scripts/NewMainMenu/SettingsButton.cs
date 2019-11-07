using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
	public class SettingsButton : MenuButton
    {
		public float delay;

		private bool clicked = false;

      
        private void OnEnable()
        {
            clicked = false;
        }

        private void OnMouseEnter()
        {
                audioSource.Play();
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
