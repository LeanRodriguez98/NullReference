using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLevitator : MonoBehaviour
{
	public float levitationStrength;

	private Rigidbody cubeRB;

	private void Start()
	{
		cubeRB = null;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PickUpable"))
		{
			cubeRB = other.GetComponent<Rigidbody>();
			//cubeRB.useGravity = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("PickUpable"))
		{
			//cubeRB.useGravity = true;
			cubeRB = null;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("PickUpable"))
			LevitateCube();
	}

	private void LevitateCube()
	{
		if (cubeRB)
		{
			Vector3 dirToLevitationPoint = (transform.position - cubeRB.gameObject.transform.position);
			cubeRB.velocity = dirToLevitationPoint * levitationStrength;
			cubeRB.gameObject.transform.Rotate(.5f, 1, .5f);
		}
	}
}
