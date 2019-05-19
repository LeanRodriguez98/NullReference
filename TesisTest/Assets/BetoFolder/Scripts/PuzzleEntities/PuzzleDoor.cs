using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PuzzleDoor : MonoBehaviour
	{
		public List<DoorConnection> m_pressurePlateConnections;
		public List<DoorConnection> m_leaverConnections;

		private Animator m_animator;

		private void Start()
		{
			m_animator = GetComponent<Animator>();
		}

		public void UpdateState()
		{
			m_animator.SetBool("AllTriggersEnabled", ShouldOpenDoor());
		}

		private bool ShouldOpenDoor()
		{
			for (int i = 0; i < m_leaverConnections.Count; i++)
				if (m_leaverConnections[i].IsEnabled())
					return true;

			if (m_pressurePlateConnections.Count == 0) return false;

			for (int i = 0; i < m_pressurePlateConnections.Count; i++)
				if (m_pressurePlateConnections[i].IsEnabled() == false)
					return false;

			return true;
		}
	}
}
