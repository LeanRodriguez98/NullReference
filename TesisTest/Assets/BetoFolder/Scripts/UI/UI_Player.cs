using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class UI_Player : MonoBehaviour
	{
		public GameObject m_interactableCrosshair;

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
		}

		void Update()
		{
			if (m_lookingAtInteractable)
				m_interactableCrosshair.SetActive(true);
			else
				m_interactableCrosshair.SetActive(false);
		}

		public void EnableCrosshair(bool isInteractable)
		{
			m_lookingAtInteractable = isInteractable;
		}
	}
}
