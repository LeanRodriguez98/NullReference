using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		public List<DoorConnection> m_doorConnections;

		private Animator m_animator;
		private bool leaverPointingBackwards;

		public override void Start()
		{
			base.Start();
			m_animator = GetComponent<Animator>();
			leaverPointingBackwards = false;
		}

		public override void Interact()
		{
			base.Interact();

			m_animator.SetTrigger("Interact");
			UpdateDoorConnectionsStates();
			leaverPointingBackwards = !leaverPointingBackwards;
		}

		private void UpdateDoorConnectionsStates()
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
			{
				bool currentDoorState = m_doorConnections[i].IsEnabled();
				m_doorConnections[i].SetIsEnabled(!currentDoorState);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.collider.CompareTag("PickUpable"))
			{
				float collisionPointDot = Vector3.Dot(transform.forward, transform.position - collision.collider.transform.position);
				Debug.Log(collisionPointDot);

				if (collisionPointDot < 0.0f && leaverPointingBackwards ||
					collisionPointDot > 0.0f && !leaverPointingBackwards)
				{
					Interact();
				}
			}
		}
	}
}
