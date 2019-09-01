using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class CameraRaycast : MonoBehaviour
	{
		public float m_interactionRange;

		private UI_Player playerUI;
		private Interactable m_interactableFound;

		private void Start()
		{
			m_interactableFound = null;
			playerUI = UI_Player.GetInstance();
		}

		void Update()
		{
			m_interactableFound = CheckInteractableEntity();
			if (m_interactableFound && Input.GetKeyDown(KeyCode.Mouse0))
				m_interactableFound.Interact();

			CheckForTriggeEvents();
		}

		void CheckForTriggeEvents()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit))
			{
				RaycastTriggerEvent triggerEvent = hit.collider.GetComponent<RaycastTriggerEvent>();
				if (triggerEvent)
					triggerEvent.TriggerEvent();
			}
		}

		Interactable CheckInteractableEntity()
		{
			if (playerUI != null)
				return ShootRaycast();
			return null;
		}

		Interactable ShootRaycast()
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, m_interactionRange))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null && interactable.CanInteract)
				{
					playerUI.SetInteractionState(UI_Player.PlayerInteractionState.LookingAtInteractable);
					return interactable;
				}
				else
				{
					playerUI.SetInteractionState(UI_Player.PlayerInteractionState.Idle);
					return null;
				}
			}
			else
			{
				playerUI.SetInteractionState(UI_Player.PlayerInteractionState.Idle);
				return null;
			}
		}
	}
}