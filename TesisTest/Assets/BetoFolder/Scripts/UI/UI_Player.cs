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

		public void EnableCrosshair(bool isInteractable)
		{
			m_interactableCrosshair.SetActive(isInteractable);
		}
	}
}
