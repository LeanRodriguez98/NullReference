using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Grabber : MonoBehaviour
	{
		public GameObject throwObj_UI;
		public Transform m_grabbingPoint;
		public float m_maxDistanceToGrab;
		public float m_distanceToAutoDrop;
		public float m_grabbingStrength;
		public float m_throwStrength;

		private GameObject m_currentPickedUpObject;
		private Rigidbody m_currentObjectRB;
		private bool m_isGrabbing;

        private GameObject obj;
        private Portable objCollider;

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
				obj = hit.collider.gameObject;

                if (obj.CompareTag("PickUpable"))
                {
                    UI_Player.GetInstance().EnableCrosshair(true);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        PickUp(obj);
                        objCollider = obj.GetComponent<Portable>();
                        if (objCollider != null)
                        {
                            objCollider.enabled = false;
                        }
                    }
                }
			}
		}

		private void PickUp(GameObject pickedUpObject)
		{
			SetCurrentPickedUpObject(pickedUpObject);
			m_isGrabbing = true;
			throwObj_UI.SetActive(true);
		}

		private void SetCurrentPickedUpObject(GameObject pickedUpObject)
		{
			m_currentPickedUpObject = pickedUpObject;
			m_currentPickedUpObject.layer = LayerMask.NameToLayer("PickedUpObject");

            for (int i = 0; i < m_currentPickedUpObject.transform.childCount; i++)
            {
                m_currentPickedUpObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("PickedUpObject");
            }

            m_currentPickedUpObject.transform.rotation = m_grabbingPoint.rotation;
			m_currentPickedUpObject.transform.parent = m_grabbingPoint.parent;

			m_currentObjectRB = m_currentPickedUpObject.GetComponent<Rigidbody>();
			m_currentObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
		}

		private void GrabObject()
		{
			Vector3 distanceToGrabber = m_grabbingPoint.position - m_currentPickedUpObject.transform.position;
			m_currentObjectRB.velocity = distanceToGrabber * (m_grabbingStrength / m_currentObjectRB.mass);

			bool shouldAutodrop = distanceToGrabber.magnitude > m_distanceToAutoDrop;
			if (shouldAutodrop || Input.GetKeyDown(KeyCode.Mouse0))
				DropObject();
			else if (Input.GetKeyDown(KeyCode.Mouse1))
				ThrowObject();

			UI_Player.GetInstance().EnableCrosshair(false);
		}

		private void DropObject()
		{
			m_currentObjectRB.constraints = RigidbodyConstraints.None;
			m_currentObjectRB = null;

			m_currentPickedUpObject.layer = LayerMask.NameToLayer("Default");

            for (int i = 0; i < m_currentPickedUpObject.transform.childCount; i++)
            {
                m_currentPickedUpObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer("Default");
            }
            m_currentPickedUpObject.transform.parent = null;
			m_currentPickedUpObject = null;
			m_isGrabbing = false;

            if (objCollider != null)
            {
                objCollider.enabled = true;
            }

			throwObj_UI.SetActive(false);
		}

		private void ThrowObject()
		{
			m_currentObjectRB.velocity = Vector3.zero;
			m_currentObjectRB.AddForce(m_grabbingPoint.transform.forward * m_throwStrength);
            
			DropObject();
		}
	}
}
