using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		public List<DoorConnection> m_doorConnections;

		private Animator m_animator;

		public override void Start()
		{
			base.Start();
			m_animator = GetComponent<Animator>();
		}

		public override void Interact()
		{
			base.Interact();

			m_animator.SetTrigger("Interact");
			UpdateDoorConnectionsStates();
		}

		private void UpdateDoorConnectionsStates()
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
			{
				bool currentDoorState = m_doorConnections[i].IsEnabled();
				m_doorConnections[i].SetIsEnabled(!currentDoorState);
			}
		}
	}
}
