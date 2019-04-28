using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpableObject : MonoBehaviour {
    private Player playerInstance;
    private Rigidbody rb;
    private bool isGrabbed;
    private Collider objectcollider;
	// Use this for initialization
	void Start () {
        playerInstance = Player.instance;
        rb = GetComponent<Rigidbody>();
        isGrabbed = false;
        objectcollider = GetComponent<Collider>();
    }
	
	// Update is called once per frame
	void Update () {


        if (isGrabbed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.parent = null;
                rb.constraints = RigidbodyConstraints.None;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                Invoke("SetIsGrabbed", Time.deltaTime * 2);
                isGrabbed = false;
                gameObject.layer = 0;
                if (objectcollider != null)
                    objectcollider.enabled = true;
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "InteractPoint")
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.parent = playerInstance.GrabbPoint.transform;
                rb.constraints = RigidbodyConstraints.FreezeAll;
                Invoke("SetIsGrabbed", Time.deltaTime * 2);
                gameObject.layer = 11; // 11 is the "characterChildren" layer
                if (objectcollider != null)
                    objectcollider.enabled = false;
            }
        }
    }

    public void SetIsGrabbed()
    {
        isGrabbed = !isGrabbed;
    }

}
