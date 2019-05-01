using UnityEngine;
using System.Collections;

using UnityEditor;

public class Sender : MonoBehaviour
{

    public GameObject receiver;
    public Portable currentlyOverlappingObject;
    private Sender otherSender;
    private PortalCallFunction functionAtTeleport;
    private bool senderUntilActivate = false;
    void Start()
    {
        otherSender = receiver.GetComponent<Sender>();
        functionAtTeleport = GetComponent<PortalCallFunction>();
    }



    void FixedUpdate()
    {
        if (!otherSender.GetSenderUntilActivate())
        {
            if (currentlyOverlappingObject != null)
            {
                float currentDot = Vector3.Dot(transform.up, currentlyOverlappingObject.transform.position - transform.position);
                if (currentDot <= 0.0f)
                {
                    if (currentlyOverlappingObject.enabled)
                    {
                        currentlyOverlappingObject.Teleport(this.transform, receiver.transform);
                        currentlyOverlappingObject = null;
                        senderUntilActivate = true;
                        Invoke("SenderUntilActivateDisabled", Time.deltaTime);
                        if (functionAtTeleport != null)
                        {
                            functionAtTeleport.CallFuncions();
                        }
                    }
                }
            }
        }
    }

    public void SenderUntilActivateDisabled()
    {
        senderUntilActivate = false;
    }

    public bool GetSenderUntilActivate()
    {
        return senderUntilActivate;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!otherSender.GetSenderUntilActivate())
        {
            if (other.GetComponentInParent<Portable>() != null)
            {
                currentlyOverlappingObject = other.GetComponentInParent<Portable>();
                otherSender.OnOverlappingObjectExit();


            }
            else if (other.GetComponent<Portable>() != null)
            {
                currentlyOverlappingObject = other.GetComponent<Portable>();
                otherSender.OnOverlappingObjectExit();

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<Portable>() != null)
        {
            otherSender.OnOverlappingObjectExit();
        }
        else if (other.GetComponent<Portable>() != null)
        {
            otherSender.OnOverlappingObjectExit();
        }
    }

    public void OnOverlappingObjectExit()
    {
        currentlyOverlappingObject = null;
    }


}