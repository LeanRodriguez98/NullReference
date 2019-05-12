using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
	public Transform grabber;
	public float maxDistanceToGrab;

	private GameObject pickedUpObject;
	private bool isGrabbing;

	private void Start()
	{
		pickedUpObject = null;
		isGrabbing = false;
	}

	private void Update ()
	{
		if (!isGrabbing)
			CheckForPickableObjects();
		else
		{
			KeepObjectAtGrabberPosition();

			if (Input.GetKeyDown(KeyCode.Mouse0))
				Drop(pickedUpObject);
		}
	}

	private void CheckForPickableObjects()
	{
		RaycastHit hit;
		if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, maxDistanceToGrab))
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
				TryToPickUp(hit.collider.gameObject);
		}
	}

	private void TryToPickUp(GameObject obj)
	{
		if (obj.tag == "PickableObject")
			PickUp(obj);
	}

	private void PickUp(GameObject objectToPickUp)
	{
		objectToPickUp.transform.position = grabber.position;
		objectToPickUp.transform.parent = grabber.transform;
		objectToPickUp.transform.rotation = grabber.rotation;
		pickedUpObject = objectToPickUp;

		Rigidbody rb = pickedUpObject.GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.constraints = RigidbodyConstraints.FreezeAll;

		isGrabbing = true;
	}

	private void KeepObjectAtGrabberPosition()
	{
		Rigidbody rb = pickedUpObject.GetComponent<Rigidbody>();
		rb.position = grabber.position;
	}

	private void Drop(GameObject objectToDrop)
	{
		Rigidbody rb = pickedUpObject.GetComponent<Rigidbody>();
		rb.useGravity = true;
		rb.constraints = RigidbodyConstraints.None;

		objectToDrop.transform.parent = null;
		pickedUpObject = null;
		isGrabbing = false;
	}
}
