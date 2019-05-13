using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class DoorTrigger : MonoBehaviour
	{
		public Door m_door;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				m_door.SetPlayerIsOnTrigger(true);
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.CompareTag("Player"))
				m_door.SetPlayerIsOnTrigger(false);
		}
	}
}
