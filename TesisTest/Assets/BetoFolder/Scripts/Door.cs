using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Door : MonoBehaviour
	{
		public GameObject m_senderDoorPlank;
		public GameObject m_receiverDoorPlank;
		public float m_openedAngle;
		public float m_openingSpeed;

		private enum DoorState { Open, Close }
		private DoorState m_doorState;
		private bool m_playerIsOnTrigger;
		private float m_doorCurrentAngle;

		void Start()
		{
			m_playerIsOnTrigger = false;
			m_doorState = DoorState.Close;
			m_doorCurrentAngle = 0;
		}

		void Update()
		{
			UpdateDoorState();
		}

		private void UpdateDoorState()
		{
			if (m_playerIsOnTrigger)
				m_doorState = DoorState.Open;
			else
				m_doorState = DoorState.Close;

			switch (m_doorState)
			{
				case DoorState.Open:
					Open();
					break;

				case DoorState.Close:
					Close();
					break;
			}
		}

		private void Open()
		{
			m_doorCurrentAngle = Mathf.SmoothStep(m_doorCurrentAngle, m_openedAngle, m_openingSpeed * Time.deltaTime);

			m_receiverDoorPlank.transform.localRotation = Quaternion.Euler(0, m_doorCurrentAngle, 0);
			m_senderDoorPlank.transform.localRotation = Quaternion.Euler(0, m_doorCurrentAngle, 0);
		}

		private void Close()
		{
			m_doorCurrentAngle = Mathf.Lerp(m_doorCurrentAngle, 0, m_openingSpeed * Time.deltaTime);

			m_receiverDoorPlank.transform.localRotation = Quaternion.Euler(0, m_doorCurrentAngle, 0);
			m_senderDoorPlank.transform.localRotation = Quaternion.Euler(0, m_doorCurrentAngle, 0);
		}

		public void SetPlayerIsOnTrigger(bool playerIsOnTrigger)
		{
			m_playerIsOnTrigger = playerIsOnTrigger;
		}
	}
}
