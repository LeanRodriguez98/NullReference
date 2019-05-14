using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class CameraRaycast : MonoBehaviour
	{
		public float m_torqueAppliedToDoor;

		private bool m_entityFound;
		private bool m_torqueApplied;
		private InteractableEntity m_entity;

		private void Start()
		{
			m_torqueApplied = false;
			m_entityFound = false;
			m_entity = null;
		}


		void Update()
		{
			
			if (!m_entityFound)
				CheckInteractableEntity();

			//if (Input.GetKeyDown(KeyCode.Mouse0) && m_entity != null)
			//	m_entity.Interact();
		}

		void CheckInteractableEntity()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				if (!m_torqueApplied && hit.collider.CompareTag("FallingDoor"))
				{
					Rigidbody doorRB = hit.collider.gameObject.GetComponent<Rigidbody>();
					doorRB.AddTorque(new Vector3(0, 0, m_torqueAppliedToDoor));
					m_torqueApplied = true;
				}
			}
		}
	}
}