using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PressurePlate : MonoBehaviour
	{
		public List<DoorConnection> m_doorConnections;
		public Animator m_animator;

		private void OnTriggerStay(Collider other)
		{
			IsBeingPressed(true);
		}

		private void OnTriggerExit(Collider other)
		{
			IsBeingPressed(false);
		}

		private void IsBeingPressed(bool isBeingPressed)
		{
			m_animator.SetBool("isPressed", isBeingPressed);
			EnableDoorConnections(isBeingPressed);
		}

		void EnableDoorConnections(bool enabled)
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
				m_doorConnections[i].SetIsEnabled(enabled);
		}
	}
}

