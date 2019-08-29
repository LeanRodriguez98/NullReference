using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Grabber : MonoBehaviour
	{
		public GameObject throwObj_UI;
		public Transform grabbingPoint;
		public float maxDistanceToGrab;
		public float distanceToAutoDrop;
		public float grabbingStrength;
		public float throwStrength;
		public float objInterpolationSpeed;
		public float distanceToLowerObjOnAiming;

		private GameObject currentPickedUpObject;
		private Rigidbody currentObjectRB;

        private Collider objCollider;

		private Camera playerCamera;
		private Vector3 objPositionWhenAiming;
		private Vector3 grabberInitialPosition;

		private enum State 
		{
			NoObjectGrabbed,
			GrabbingObject,
			Aiming
		}
		private State state;

        private void Start()
		{
			currentPickedUpObject = null;
			currentObjectRB = null;
			playerCamera = Camera.main;
			objPositionWhenAiming = grabbingPoint.localPosition + (-grabbingPoint.transform.up * distanceToLowerObjOnAiming);
			grabberInitialPosition = grabbingPoint.localPosition;

			state = State.NoObjectGrabbed;
		}

		private void Update()
		{
			UpdateState();
		}

		private void UpdateState()
		{
			switch(state)
			{
				case State.NoObjectGrabbed: CheckForPickableObjects();
					break;
				case State.GrabbingObject: GrabObject();
					break;
				case State.Aiming: Aiming();
					break;
			}
		}

		private void CheckForPickableObjects()
		{
			RaycastHit hit;
			if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, maxDistanceToGrab))
			{
				GameObject objectToBeProcessed = hit.collider.gameObject;

                if (objectToBeProcessed.CompareTag("PickUpable"))
                {
                    UI_Player.GetInstance().EnableCrosshair(true);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        PickUp(objectToBeProcessed);
                    }
                }
			}
		}
     
		private void PickUp(GameObject pickedUpObject)
		{
			SetCurrentPickedUpObject(pickedUpObject);
			DisableColliderFrom(pickedUpObject);

			throwObj_UI.SetActive(true);
			state = State.GrabbingObject;
		}

		private void SetCurrentPickedUpObject(GameObject pickedUpObject)
		{
			currentPickedUpObject = pickedUpObject;
			SetObjectAndChildsToLayerMask("PickedUpObject");

            currentPickedUpObject.transform.rotation = transform.rotation;
			currentPickedUpObject.transform.parent = grabbingPoint.parent;

			currentObjectRB = currentPickedUpObject.GetComponent<Rigidbody>();
			currentObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
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
			KeepObjectAtGrabberPosition();
			grabbingPoint.localPosition = Vector3.Lerp(grabbingPoint.localPosition, grabberInitialPosition, objInterpolationSpeed);

			if (Input.GetKeyDown(KeyCode.Mouse0))
				DropObject();

			if (Input.GetKeyDown(KeyCode.Mouse1))
				state = State.Aiming;

			UI_Player.GetInstance().EnableCrosshair(false);
		}

		private void KeepObjectAtGrabberPosition()
		{
			Vector3 vectorToGrabber = grabbingPoint.position - currentPickedUpObject.transform.position;
			currentObjectRB.velocity = vectorToGrabber * (grabbingStrength / currentObjectRB.mass);

			CheckForAutodrop(vectorToGrabber.magnitude);
		}

		private void CheckForAutodrop(float distanceFromGrabber)
		{
			if (distanceFromGrabber > distanceToAutoDrop)
				DropObject();
		}

		private void DropObject()
		{
			if (currentPickedUpObject.GetComponent<Cube>() != null)
			{
				currentPickedUpObject.GetComponent<Cube>().SetIsGrabbed(false);
			}

			currentObjectRB.constraints = RigidbodyConstraints.None;
			currentObjectRB = null;

			SetObjectAndChildsToLayerMask("Default");

            currentPickedUpObject.transform.parent = null;
			currentPickedUpObject = null;

            if (objCollider != null)
            {
                objCollider.enabled = true;
            }

			throwObj_UI.SetActive(false);

			state = State.NoObjectGrabbed;
        }

		private void SetObjectAndChildsToLayerMask(string layerName)
		{
			currentPickedUpObject.layer = LayerMask.NameToLayer(layerName);
			for (int i = 0; i < currentPickedUpObject.transform.childCount; i++)
			{
				currentPickedUpObject.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer(layerName);
			}
		}

		private void Aiming()
		{
			KeepObjectAtGrabberPosition();
			AimingState();

			if (Input.GetKeyUp(KeyCode.Mouse1))
				ThrowObject();
			else if (Input.GetKeyDown(KeyCode.Mouse0))
				state = State.GrabbingObject;
		}

		private void AimingState()
		{
			grabbingPoint.localPosition = Vector3.Lerp(grabbingPoint.localPosition, objPositionWhenAiming, objInterpolationSpeed);
		}

		private void ThrowObject()
		{
			currentObjectRB.velocity = Vector3.zero;
			currentObjectRB.AddForce(transform.forward * throwStrength);
			DropObject();
		}
	}
}
