using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : MonoBehaviour {
    private Player playerInstance;
    private Rigidbody rb;
    private bool isGrabbed;
	// Use this for initialization
	void Start () {
        playerInstance = Player.instance;
        rb = GetComponent<Rigidbody>();
        isGrabbed = false;
    }
	
	// Update is called once per frame
	void Update () {


        if (isGrabbed)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                transform.parent = null;
                rb.constraints = RigidbodyConstraints.None;
                isGrabbed = false;
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "InteractPoint")
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.parent = playerInstance.GrabbPoint.transform;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                isGrabbed = true;
            }
        }
    }
}
