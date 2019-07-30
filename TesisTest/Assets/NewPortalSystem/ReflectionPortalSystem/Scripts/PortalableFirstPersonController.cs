using System;
using UnityEngine;

namespace PortalableFirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
	public class PortalableFirstPersonController : MonoBehaviour
    {
		[Serializable]
		public class MouseLook
		{
			public float xSensitivity = 2f;
			public float ySensitivity = 2f;
			public bool clampVerticalRotation = true;
			public float minimumX = -90F;
			public float maximumX = 90F;
			public bool smooth;
			public float smoothTime = 5f;
			public bool lockCursor = true;


			public Quaternion characterTargetRot;
			public Quaternion cameraTargetRot;


			public Quaternion ClampRotationAroundXAxis(Quaternion q)
			{
				q.x /= q.w;
				q.y /= q.w;
				q.z /= q.w;
				q.w = 1.0f;

				float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

				angleX = Mathf.Clamp (angleX, minimumX, maximumX);

				q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

				return q;
			}

		}
        [Serializable]
        public class MovementSettings
        {
            public float forwardSpeed = 8.0f;
            public float backwardSpeed = 4.0f;
            public float strafeSpeed = 4.0f;
            public float runMultiplier = 2.0f; 
	        public KeyCode runKey = KeyCode.LeftShift;
            public float jumpForce = 30f;
            public AnimationCurve slopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
            [HideInInspector] public float currentTargetSpeed = 8f;

            public void UpdateDesiredTargetSpeed(Vector2 input)
            {
                if (input == Vector2.zero)
                {
                    return;
                }
				if (input.x > 0 || input.x < 0)
				{
					currentTargetSpeed = strafeSpeed;
				}
				if (input.y < 0)
				{
					currentTargetSpeed = backwardSpeed;
				}
				if (input.y > 0)
				{
					currentTargetSpeed = forwardSpeed;
				}
            }
        }


        [Serializable]
        public class AdvancedSettings
        {
            public float groundCheckDistance = 0.01f;
            public float stickToGroundHelperDistance = 0.5f;
            public float slowDownRate = 20f;
            public bool airControl;
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset;
        }

        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();

        private Rigidbody rigidBody;
        private CapsuleCollider capsuleCollider;
        private float yRotation;
        private Vector3 groundContactNormal;
        private bool jump;
        private bool previouslyGrounded;
        private bool jumping;
        private bool isGrounded;

        public Vector3 Velocity
        {
            get { return rigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return isGrounded; }
        }

        public bool Jumping
        {
            get { return jumping; }
        }

        public void UpdateOrientation(Quaternion orientation)
		{
			mouseLook.characterTargetRot = Quaternion.Euler (0, orientation.eulerAngles.y, 0);
		}

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            capsuleCollider = GetComponent<CapsuleCollider>();

			mouseLook.characterTargetRot = Quaternion.Euler (0, transform.localRotation.eulerAngles.y, 0);
			mouseLook.cameraTargetRot = Quaternion.Euler(cam.transform.localRotation.eulerAngles.x,0,0);

			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
        }


        private void Update()
        {
            RotateView();

			if (Input.GetMouseButtonDown (0)) {
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}

			float yRot = Input.GetAxis("Mouse X") * mouseLook.xSensitivity;
			float xRot = Input.GetAxis("Mouse Y") * mouseLook.ySensitivity;

			mouseLook.characterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
			mouseLook.cameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if (mouseLook.clampVerticalRotation)
            {
                mouseLook.cameraTargetRot = mouseLook.ClampRotationAroundXAxis(mouseLook.cameraTargetRot);
            }

			if(mouseLook.smooth)
			{
				transform.localRotation = Quaternion.Slerp (transform.localRotation, mouseLook.characterTargetRot,
					mouseLook.smoothTime * Time.deltaTime);
				cam.transform.localRotation = Quaternion.Slerp (cam.transform.localRotation, mouseLook.cameraTargetRot,
					mouseLook.smoothTime * Time.deltaTime);
			}
			else
			{
				transform.localRotation = mouseLook.characterTargetRot;
				cam.transform.localRotation = mouseLook.cameraTargetRot;
			}


            if (Input.GetButtonDown("Jump") && !jump)
            {
                jump = true;
            }
        }


        private void FixedUpdate()
        {
            GroundCheck();
            Vector2 input = GetInput();

            if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || isGrounded))
            {
                Vector3 desiredMove = cam.transform.forward*input.y + cam.transform.right*input.x;
                desiredMove = Vector3.ProjectOnPlane(desiredMove, groundContactNormal).normalized;

                desiredMove.x = desiredMove.x*movementSettings.currentTargetSpeed;
                desiredMove.z = desiredMove.z*movementSettings.currentTargetSpeed;
                desiredMove.y = desiredMove.y*movementSettings.currentTargetSpeed;
                if (rigidBody.velocity.sqrMagnitude <
                    (movementSettings.currentTargetSpeed*movementSettings.currentTargetSpeed))
                {
                    rigidBody.AddForce(desiredMove*SlopeMultiplier(), ForceMode.Impulse);
                }
            }

            if (isGrounded)
            {
                rigidBody.drag = 5f;

                if (jump)
                {
                    rigidBody.drag = 0f;
                    rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
                    rigidBody.AddForce(new Vector3(0f, movementSettings.jumpForce, 0f), ForceMode.Impulse);
                    jumping = true;
                }

                if (!jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && rigidBody.velocity.magnitude < 1f)
                {
                    rigidBody.Sleep();
                }
            }
            else
            {
                rigidBody.drag = 0f;
                if (previouslyGrounded && !jumping)
                {
                    StickToGroundHelper();
                }
            }
            jump = false;
        }


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(groundContactNormal, Vector3.up);
            return movementSettings.slopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, capsuleCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo, ((capsuleCollider.height/2f) - capsuleCollider.radius) + advancedSettings.stickToGroundHelperDistance, ~0, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    rigidBody.velocity = Vector3.ProjectOnPlane(rigidBody.velocity, hitInfo.normal);
                }
            }
        }


        private Vector2 GetInput()
        {
            Vector2 input = new Vector2{x = Input.GetAxis("Horizontal"),y = Input.GetAxis("Vertical")};
			movementSettings.UpdateDesiredTargetSpeed(input);
            return input;
        }


        private void RotateView()
        {
            if (Mathf.Abs(Time.timeScale) < float.Epsilon)
            {
                return;
            }

            float oldYRotation = transform.eulerAngles.y;


            if (isGrounded || advancedSettings.airControl)
            {
                Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
                rigidBody.velocity = velRotation*rigidBody.velocity;
            }
        }

        private void GroundCheck()
        {
            previouslyGrounded = isGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, capsuleCollider.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo, ((capsuleCollider.height/2f) - capsuleCollider.radius) + advancedSettings.groundCheckDistance, ~0, QueryTriggerInteraction.Ignore))
            {
                isGrounded = true;
                groundContactNormal = hitInfo.normal;
            }
            else
            {
                isGrounded = false;
                groundContactNormal = Vector3.up;
            }
            if (!previouslyGrounded && isGrounded && jumping)
            {
                jumping = false;
            }
        }
    }
}
