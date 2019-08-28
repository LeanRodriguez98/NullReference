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
		public float objInterpolationSpeed;
		public float distanceToLowerObjOnAiming;

		private GameObject m_currentPickedUpObject;
		private Rigidbody m_currentObjectRB;
		private bool m_isGrabbing;

        private GameObject obj;
        private Collider objCollider;

		private Camera playerCamera;
		private Vector3 objPositionWhenAiming;
		private Vector3 grabberInitialPosition;
		private bool throwCanceled;
		private bool onAiming;

        private void Start()
		{
			m_currentPickedUpObject = null;
			m_currentObjectRB = null;
			m_isGrabbing = false;
			playerCamera = Camera.main;
			objPositionWhenAiming = m_grabbingPoint.localPosition + (-m_grabbingPoint.transform.up * distanceToLowerObjOnAiming);
			grabberInitialPosition = m_grabbingPoint.localPosition;
			throwCanceled = false;
			onAiming = false;
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
                    }
                }
			}
		}

     
		private void PickUp(GameObject pickedUpObject)
		{
			SetCurrentPickedUpObject(pickedUpObject);
			DisableColliderFrom(pickedUpObject);

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

            m_currentPickedUpObject.transform.rotation = transform.rotation;
			m_currentPickedUpObject.transform.parent = m_grabbingPoint.parent;

			m_currentObjectRB = m_currentPickedUpObject.GetComponent<Rigidbody>();
			m_currentObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
		}

		private void DisableColliderFrom(GameObject pickedUpObject)
		{
			objCollider = pickedUpObject.GetComponent<Collider>();
			if (objCollider != null)
			{
				objCollider.enabled = false;
			}

			if (pickedUpObject.GetComponent<Cube>() != null)
			{
				pickedUpObject.GetComponent<Cube>().SetIsGrabbed(true);
			}
		}

		private void GrabObject()
		{
			Vector3 distanceToGrabber = m_grabbingPoint.position - m_currentPickedUpObject.transform.position;
			m_currentObjectRB.velocity = distanceToGrabber * (m_grabbingStrength / m_currentObjectRB.mass);

			if (!onAiming)
			{
				bool shouldAutodrop = distanceToGrabber.magnitude > m_distanceToAutoDrop;
				if (shouldAutodrop || Input.GetKeyDown(KeyCode.Mouse0))
					DropObject();
			}
			CheckForAimingState();

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

            if (obj.GetComponent<Cube>() != null)
            {
                obj.GetComponent<Cube>().SetIsGrabbed(false);
            }
        }

		private void CheckForAimingState()
		{
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				throwCanceled = false;
				onAiming = true;
			}
			
			if (!throwCanceled)
			{
				if (Input.GetKey(KeyCode.Mouse1))
					AimingState();
				else if (Input.GetKeyUp(KeyCode.Mouse1))
					ThrowObject();
			}
			else
			{
				m_grabbingPoint.localPosition = Vector3.Lerp(m_grabbingPoint.localPosition, grabberInitialPosition, objInterpolationSpeed);
			}
		}

		private void AimingState()
		{
			m_grabbingPoint.localPosition = Vector3.Lerp(m_grabbingPoint.localPosition, objPositionWhenAiming, objInterpolationSpeed);

			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				throwCanceled = true;
				onAiming = false;
			}
		}

		private void ThrowObject()
		{
			throwCanceled = true;

			m_currentObjectRB.velocity = Vector3.zero;
			m_currentObjectRB.AddForce(transform.forward * m_throwStrength);
            
			DropObject();
		}
	}
}
