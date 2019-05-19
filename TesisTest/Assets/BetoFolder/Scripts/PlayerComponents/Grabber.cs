using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Grabber : MonoBehaviour
	{
		public Transform m_grabbingPoint;
		public float m_maxDistanceToGrab;

		private GameObject m_currentPickedUpObject;
		private bool m_isGrabbing;

		private void Start()
		{
			m_currentPickedUpObject = null;
			m_isGrabbing = false;
		}

		private void Update()
		{
			if (!m_isGrabbing)
				CheckForPickableObjects();
			else if (Input.GetKeyDown(KeyCode.Mouse0))
				DropObject();
		}

		private void CheckForPickableObjects()
		{
			RaycastHit hit;
			if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, m_maxDistanceToGrab))
			{
				GameObject obj = hit.collider.gameObject;

				//UI_Player.GetInstance().EnableCrosshair(IsPickUpable(obj));

				if (Input.GetKeyDown(KeyCode.Mouse0) && IsPickUpable(obj))
					PickUp(obj);
			}
		}

		private bool IsPickUpable(GameObject obj)
		{
			if (obj.tag == "PickUpable")
				return true;
			else
				return false;
		}

		private void PickUp(GameObject pickedUpObject)
		{
			SetCurrentPickedUpObject(pickedUpObject);

			Rigidbody rb = m_currentPickedUpObject.GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.constraints = RigidbodyConstraints.FreezeAll;

			m_isGrabbing = true;

			UI_Player.GetInstance().EnableCrosshair(false);
		}

		private void SetCurrentPickedUpObject(GameObject pickedUpObject)
		{
			m_currentPickedUpObject = pickedUpObject;
			m_currentPickedUpObject.transform.parent = m_grabbingPoint.transform;
			m_currentPickedUpObject.transform.position = m_grabbingPoint.position;
			m_currentPickedUpObject.transform.rotation = m_grabbingPoint.rotation;
			m_currentPickedUpObject.layer = LayerMask.NameToLayer("PickedUpObject");
		}


		private void DropObject()
		{
			Rigidbody rb = m_currentPickedUpObject.GetComponent<Rigidbody>();
			rb.useGravity = true;
			rb.constraints = RigidbodyConstraints.FreezePositionX;
			rb.constraints = RigidbodyConstraints.FreezePositionZ;

			m_currentPickedUpObject.layer = LayerMask.NameToLayer("Default");
			m_currentPickedUpObject.transform.parent = null;
			m_currentPickedUpObject = null;
			m_isGrabbing = false;
		}
	}
}
