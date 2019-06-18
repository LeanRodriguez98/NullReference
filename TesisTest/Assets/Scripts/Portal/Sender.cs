using UnityEngine;


public class Sender : MonoBehaviour
{

    public GameObject receiver;
    private Portable currentlyOverlappingObject;
    private Collider currentlyOverlappingObjectCollider;
    private Sender otherSender;
    private PortalCallFunction functionAtTeleport;
    private bool senderUntilActivate = false;
    private BoxCollider colliderPlane;
    private Player playerInstance;
    [SerializeField] private float autoWalkFrames = 5.0f;
    [Space(10)]
    [SerializeField] private bool activateVisualGlitch = true;
    [SerializeField] private float visualGlitchDuration = 0.0f;
    void Start()
    {
        otherSender = receiver.GetComponent<Sender>();
        functionAtTeleport = GetComponent<PortalCallFunction>();
        colliderPlane = GetComponent<BoxCollider>();
        playerInstance = Player.instance;
        if (visualGlitchDuration < 0)
        {
            visualGlitchDuration = 0;
        }
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
                        if (colliderPlane.bounds.Intersects(currentlyOverlappingObjectCollider.bounds))
                        {
                            if (currentlyOverlappingObject == playerInstance.playerPortable)
                            {
                                playerInstance.AutoWalk(autoWalkFrames * Time.deltaTime);
                            }
                            currentlyOverlappingObject.Teleport(this.transform, receiver.transform);
                            currentlyOverlappingObject = null;
                            senderUntilActivate = true;
                            Invoke("SenderUntilActivateDisabled", Time.deltaTime);
                            if (activateVisualGlitch)
                            {
                                if (GlitchEffect.glitchEffectInstance != null)
                                {
                                    if (visualGlitchDuration == 0)
                                    {
                                        GlitchEffect.glitchEffectInstance.DisplayGlitchOn();
                                    }
                                    else
                                    {
                                        GlitchEffect.glitchEffectInstance.DisplayGlitchOn(visualGlitchDuration);
                                    }
                                }
                            }
                            if (functionAtTeleport != null)
                            {
                                functionAtTeleport.CallFuncions();
                            }
                        }
                        else
                        {
                            OnOverlappingObjectExit();
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
            if (other.gameObject.CompareTag("Player"))
            {
                currentlyOverlappingObject = playerInstance.playerPortable;
                currentlyOverlappingObjectCollider = playerInstance.mainCollider;
            }
            else if (other.GetComponentInParent<Portable>() != null)
            {
                currentlyOverlappingObject = other.GetComponentInParent<Portable>();
                currentlyOverlappingObjectCollider = other.GetComponentInParent<Collider>();
                otherSender.OnOverlappingObjectExit();


            }
            else if (other.GetComponent<Portable>() != null)
            {
                currentlyOverlappingObject = other.GetComponent<Portable>();
                currentlyOverlappingObjectCollider = other.GetComponent<Collider>();

                otherSender.OnOverlappingObjectExit();

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.GetComponentInParent<Portable>() != null || other.GetComponent<Portable>() != null)
        { 
            otherSender.OnOverlappingObjectExit();
        }
    }

    public void OnOverlappingObjectExit()
    {
        currentlyOverlappingObject = null;
        currentlyOverlappingObjectCollider = null;
    }


}