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
		}
		#endregion 

		void Start()
		{
			m_lookingAtInteractable = false;
			currentObjective.text = GameManager.GetInstance().aivaObjective;
		}

		private void Update()
		{
			if (currentObjective.gameObject.activeSelf && GameManager.GetInstance().RestartedAIVA)
				currentObjective.text = GameManager.GetInstance().coffeeObjective;
		}

		public void EnableCrosshair(bool isInteractable)
		{
			m_interactableCrosshair.SetActive(isInteractable);
		}
	}
}
