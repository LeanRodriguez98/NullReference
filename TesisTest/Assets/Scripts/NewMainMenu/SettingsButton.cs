using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
	public class SettingsButton : MonoBehaviour
	{
		public UI_MainMenu mainMenu;
		public float delay;

		private bool clicked = false;

		void OnMouseDown()
		{
			if (!clicked)
			{
				//MainMenu.instace.OnButtonClicked();
				Invoke("DisplaySettings", delay);
				clicked = true;
			}
		}

		private void DisplaySettings()
		{
			mainMenu.OptionsMenuToggle();
		}
	}
}
