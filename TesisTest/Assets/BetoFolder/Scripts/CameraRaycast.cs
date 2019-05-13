using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class CameraRaycast : MonoBehaviour
	{
		public float m_torqueAppliedToDoor;

		private bool m_torqueApplied;

		private void Start()
		{
			m_torqueApplied = false;
		}


		void Update()
		{
			if (!m_torqueApplied)
				CheckForFallingDoor();
		}

		void CheckForFallingDoor()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				if (hit.collider.CompareTag("FallingDoor"))
				{
					Rigidbody doorRB = hit.collider.gameObject.GetComponent<Rigidbody>();
					doorRB.AddTorque(new Vector3(0, 0, m_torqueAppliedToDoor));
					m_torqueApplied = true;
				}
			}
		}
	}
}