using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
	public class SettingsButton : MonoBehaviour
	{
		public float delay;

		private bool clicked = false;

        private void OnEnable()
        {
            clicked = false;
        }
		void OnMouseDown()
		{
			if (!clicked)
			{
				Invoke("DisplaySettings", delay);
				clicked = true;
			}
		}

		private void DisplaySettings()
		{
			MainMenu.instace.DisplaySettings(true);
		}
	}
}
