using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeLauncher : MonoBehaviour
{
	public GameObject cubePrefab;
	public Transform spawnPosition;
	public float throwingForce;
    public string playerTag;
	void Shoot ()
	{
		GameObject spawnedCube = Instantiate(cubePrefab, spawnPosition.position, spawnPosition.rotation);
		Rigidbody cubeRB = spawnedCube.GetComponent<Rigidbody>();
		cubeRB.AddForce(spawnPosition.up * throwingForce);
        Destroy(gameObject);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            Shoot();
        }
    }
}
