using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BetoScripts
{
	public class UI_Player : MonoBehaviour
	{
		public GameObject m_interactableCrosshair;
		public Text currentObjective;
		public Text interactText;
		public Text throwText;
		public Text dropText;

		private bool m_lookingAtInteractable;

		#region Singleton
		private static UI_Player instance;

		public static UI_Player GetInstance()
		{
			return instance;
		}
		private void Awake()
		{
			instance = this;
			
			if(PlayerPrefs.GetInt("SubtitleLenguage") == 0)
			{
				interactText.text = SubtitleManager.instance.GetAudio("PressUI").englishSubtitles;
				throwText.text = SubtitleManager.instance.GetAudio("ThrowUI").englishSubtitles;
				dropText.text = SubtitleManager.instance.GetAudio("DropUI").englishSubtitles;
			}
			else
			{
				interactText.text = SubtitleManager.instance.GetAudio("PressUI").spanishSubtitles;
				throwText.text = SubtitleManager.instance.GetAudio("ThrowUI").spanishSubtitles;
				dropText.text = SubtitleManager.instance.GetAudio("DropUI").spanishSubtitles;
			}
		}
		#endregion 

		void Start()
		{
			m_lookingAtInteractable = false;
			currentObjective.text = GameManager.GetInstance().GetAivaObjective();
		}

		private void Update()
		{
			if (currentObjective.gameObject.activeSelf && GameManager.GetInstance().RestartedAIVA)
				currentObjective.text = GameManager.GetInstance().GetCoffeObjective();
		}

		public void EnableCrosshair(bool isInteractable)
		{
			m_interactableCrosshair.SetActive(isInteractable);
		}
	}
}
