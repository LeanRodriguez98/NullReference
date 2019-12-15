using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
    public class Grabber : MonoBehaviour
    {
        //public GameObject throwObj_UI;
        public Transform grabbingPoint;
        public float maxDistanceToGrab;
        public float distanceToAutoDrop;
        public float grabbingStrength;
        public float throwStrength;
        public float aimingTransitionSpeed;
        public float distanceToRaiseObjOnAiming;
        public float fovOnAiming;
        private GameObject pickedUpObject;
        private Rigidbody pickedUpObjectRB;

        private Camera playerCamera;
        private float initialFOV;
        private Vector3 objPositionWhenAiming;
        private Vector3 grabberInitialPosition;
        private Cube cubeComponent;

        private enum State
        {
            NoObjectGrabbed,
            GrabbingObject,
            Aiming
        }
        private State state;

        private void Start()
        {
            playerCamera = Camera.main;

            pickedUpObject = null;
            pickedUpObjectRB = null;
            objPositionWhenAiming = playerCamera.transform.up * distanceToRaiseObjOnAiming;
            grabberInitialPosition = grabbingPoint.localPosition;

            initialFOV = playerCamera.fieldOfView;

            state = State.NoObjectGrabbed;
        }

        private void Update()
        {
            UpdateState();
        }

        private void UpdateState()
        {
            switch (state)
            {
                case State.NoObjectGrabbed:
                    CheckForPickableObjects();
                    break;
                case State.GrabbingObject:
                    GrabObject();
                    break;
                case State.Aiming:
                    Aiming();
                    break;
            }

        }

        private void CheckForPickableObjects()
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, maxDistanceToGrab))// && GameManager.GetInstance().RestartedAIVA)
            {
                GameObject potentialObjectForPickUp = hit.collider.gameObject;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    PickUpObjectIfPossible(potentialObjectForPickUp);
            }
            LerpGrabbingPointToPosition(grabberInitialPosition);
            LerpCameraFOV(initialFOV);
        }

        private void PickUpObjectIfPossible(GameObject obj)
        {
            if (obj.CompareTag("PickUpable"))
                PickUp(obj);
        }

        private void PickUp(GameObject obj)
        {
            pickedUpObject = obj;

            SetupObjectTransform(pickedUpObject);
            SetupObjectRigidBody(pickedUpObject);
            SetColliderActiveOf(pickedUpObject, false);
            SetObjectAndChildsToLayerMask(pickedUpObject, "PickedUpObject");
            TryToSetupCubeComponent(pickedUpObject, true);

            //throwObj_UI.SetActive(true);
            if (cubeComponent != null)
                cubeComponent.PlayLeavingSound();

            state = State.GrabbingObject;
            UI_Player.GetInstance().SetInteractionState(UI_Player.PlayerInteractionState.GrabbingObject);

        }

        private void SetupObjectTransform(GameObject obj)
        {
            obj.transform.rotation = transform.rotation;
            obj.transform.parent = grabbingPoint.parent;
        }

        private void SetupObjectRigidBody(GameObject obj)
        {
            pickedUpObjectRB = obj.GetComponent<Rigidbody>();
            pickedUpObjectRB.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void GrabObject()
        {
            KeepObjectAtGrabberPosition();
            LerpGrabbingPointToPosition(grabberInitialPosition);
            LerpCameraFOV(initialFOV);

            if (Input.GetKeyDown(KeyCode.Mouse0))
			{
                DropObject();
			}
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                state = State.Aiming;
                UI_Player.GetInstance().SetInteractionState(UI_Player.PlayerInteractionState.AimingToThrowObject);
            }

            UI_Player.GetInstance().DisplayPlayerInteractUI(false);
        }

        private void KeepObjectAtGrabberPosition()
        {
            Vector3 vectorToGrabber = grabbingPoint.position - pickedUpObject.transform.position;
            pickedUpObjectRB.velocity = vectorToGrabber * (grabbingStrength / pickedUpObjectRB.mass);

            CheckForAutodrop(vectorToGrabber.magnitude);
        }

        private void CheckForAutodrop(float distanceFromGrabber)
        {
            if (distanceFromGrabber > distanceToAutoDrop)
                DropObject();
        }

        private void DropObject()
        {
            TryToSetupCubeComponent(pickedUpObject, false);
            SetObjectAndChildsToLayerMask(pickedUpObject, "Default");
            SetColliderActiveOf(pickedUpObject, true);
            ResetPickedUpObject();

            //throwObj_UI.SetActive(false);
            UI_Player.GetInstance().SetInteractionState(UI_Player.PlayerInteractionState.Idle);
            state = State.NoObjectGrabbed;
            cubeComponent = null;
        }

        private void TryToSetupCubeComponent(GameObject obj, bool cubeIsGrabbed)
        {
            cubeComponent = obj.GetComponent<Cube>();
            if (cubeComponent != null)
                cubeComponent.SetIsGrabbed(cubeIsGrabbed);
        }

        private void SetObjectAndChildsToLayerMask(GameObject obj, string layerName)
        {
            obj.layer = LayerMask.NameToLayer(layerName);
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                obj.transform.GetChild(i).gameObject.layer = LayerMask.NameToLayer(layerName);
            }
        }

        private void SetColliderActiveOf(GameObject pickedUpObject, bool enableCollider)
        {
            Collider objCollider = pickedUpObject.GetComponent<Collider>();
            if (objCollider != null) { }
            //objCollider.enabled = enableCollider;
        }

        private void ResetPickedUpObject()
        {
            pickedUpObjectRB.constraints = RigidbodyConstraints.None;
            pickedUpObjectRB = null;

            pickedUpObject.transform.parent = null;
            pickedUpObject = null;
        }

        private void Aiming()
        {
            KeepObjectAtGrabberPosition();
            AimingState();

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                state = State.GrabbingObject;
                UI_Player.GetInstance().SetInteractionState(UI_Player.PlayerInteractionState.GrabbingObject);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse0))
                ThrowObject();

            UI_Player.GetInstance().DisplayPlayerInteractUI(false);
        }

        private void AimingState()
        {
            LerpGrabbingPointToPosition(objPositionWhenAiming);
            LerpCameraFOV(fovOnAiming);
        }

        private void LerpGrabbingPointToPosition(Vector3 position)
        {
            grabbingPoint.localPosition = Vector3.Lerp(grabbingPoint.localPosition, position, aimingTransitionSpeed);
        }

        private void LerpCameraFOV(float fov)
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, fov, aimingTransitionSpeed);
        }

        private void ThrowObject()
        {
            pickedUpObjectRB.velocity = Vector3.zero;
            pickedUpObjectRB.AddForce(grabbingPoint.forward * throwStrength);
            if (cubeComponent != null)
                cubeComponent.PlayThrowSound();
            DropObject();
        }
    }
}
