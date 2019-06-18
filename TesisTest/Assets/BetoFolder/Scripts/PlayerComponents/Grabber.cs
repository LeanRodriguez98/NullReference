using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Grabber : MonoBehaviour
	{
		public Transform m_grabbingPoint;
		public float m_maxDistanceToGrab;
		public float m_distanceToAutoDrop;
		public float m_strength;

		private GameObject m_currentPickedUpObject;
		private Rigidbody m_currentObjectRB;
		private bool m_isGrabbing;

		private void Start()
		{
			m_currentPickedUpObject = null;
			m_currentObjectRB = null;
			m_isGrabbing = false;
		}

		private void Update()
		{
			if (!m_isGrabbing)
				CheckForPickableObjects();
			else
				GrabObject();
		}

		private void CheckForPickableObjects()
		{
			RaycastHit hit;
			if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, m_maxDistanceToGrab))
			{
				GameObject obj = hit.collider.gameObject;

				if (IsPickUpable(obj))
				{
					UI_Player.GetInstance().EnableCrosshair(true);

					if (Input.GetKeyDown(KeyCode.Mouse0))
						PickUp(obj);
				}
			}
		}

		private bool IsPickUpable(GameObject obj)
		{
			return (obj.tag == "PickUpable");
		}

		private void PickUp(GameObject pickedUpObject)
		{
			SetCurrentPickedUpObject(pickedUpObject);
			m_isGrabbing = true;
		}

		private void SetCurrentPickedUpObject(GameObject pickedUpObject)
		{
			m_currentPickedUpObject = pickedUpObject;
			m_currentObjectRB = m_currentPickedUpObject.GetComponent<Rigidbody>();
			m_currentPickedUpObject.layer = LayerMask.NameToLayer("PickedUpObject");
			m_currentPickedUpObject.transform.parent = m_grabbingPoint;
		}

		private void GrabObject()
		{
			Vector3 distanceToGrabber = m_grabbingPoint.position - m_currentPickedUpObject.transform.position;
			KeepObjectAtGrabberPosition(distanceToGrabber);

			bool shouldAutodrop = distanceToGrabber.magnitude > m_distanceToAutoDrop;
			if (shouldAutodrop || Input.GetKeyDown(KeyCode.Mouse0))
				DropObject();

			UI_Player.GetInstance().EnableCrosshair(false);
		}

		private void KeepObjectAtGrabberPosition(Vector3 distToGrabber)
		{
			if (m_currentObjectRB != null)
			{
				m_currentObjectRB.velocity = distToGrabber * (m_strength / m_currentObjectRB.mass);
				m_currentObjectRB.rotation = m_grabbingPoint.rotation;
			}
		}

		private void DropObject()
		{
			m_currentObjectRB.constraints = RigidbodyConstraints.None;
			m_currentObjectRB = null;

			m_currentPickedUpObject.layer = LayerMask.NameToLayer("Default");
			m_currentPickedUpObject.transform.parent = null;
			m_currentPickedUpObject = null;
			m_isGrabbing = false;
		}
	}
}
