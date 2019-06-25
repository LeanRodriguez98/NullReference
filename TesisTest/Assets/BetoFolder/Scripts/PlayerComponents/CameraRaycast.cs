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
			m_interactableFound = CheckInteractableEntity();
			if (m_interactableFound && Input.GetKeyDown(KeyCode.Mouse0))
				m_interactableFound.Interact();
		}

		Interactable CheckInteractableEntity()
		{
			UI_Player playerUI = UI_Player.GetInstance();

			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, m_interactionRange))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();

				if (playerUI)
				{
					if (interactable != null && interactable.CanInteract)
					{

						playerUI.EnableCrosshair(true);
						return interactable;
					}
					else
					{
						playerUI.EnableCrosshair(false);
						return null;
					}
				}
				return null;
			}
			else
			{
				if (playerUI)
					playerUI.EnableCrosshair(false);

				return null;
			}
		}
	}
}