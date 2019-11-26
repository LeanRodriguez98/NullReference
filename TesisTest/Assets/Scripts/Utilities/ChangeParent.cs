using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeParent : MonoBehaviour
{
    public GameObject objectToChangeParent;
    public Transform newParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            objectToChangeParent.transform.parent = newParent;
            Destroy(this);
        }
    }
}
