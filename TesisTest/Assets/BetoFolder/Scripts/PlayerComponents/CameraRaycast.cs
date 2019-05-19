using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class CameraRaycast : MonoBehaviour
	{
		public float m_interactionRange;

		private Interactable m_interactableFound;

		private void Start()
		{
			m_interactableFound = null;
		}

		void Update()
		{
			if (!m_interactableFound)
				m_interactableFound = CheckInteractableEntity();
			else if (Input.GetKeyDown(KeyCode.Mouse0))
				m_interactableFound.Interact();
		}

		Interactable CheckInteractableEntity()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, m_interactionRange))
			{
				//Debug.Log("Looking at: " + hit.collider.name);
				return hit.collider.GetComponent<Interactable>();
			}
			else
				return null;
		}
	}
}