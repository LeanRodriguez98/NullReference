using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
	public GameObject cubePrefab;
	public Transform spawnPosition;
	public float throwingForce;

	void Start ()
	{
		GameObject spawnedCube = Instantiate(cubePrefab, spawnPosition.position, spawnPosition.rotation);
		Rigidbody cubeRB = spawnedCube.GetComponent<Rigidbody>();
		cubeRB.AddForce(spawnPosition.up * throwingForce);
	}
}
