using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
public class VR_Movement_Goune : MonoBehaviour {

	protected CharacterController Controller = null;
	protected OVRCameraRig CameraController = null;

	[Header("Oculus Settings")]
	[Tooltip("Amount of degrees you turn when pressing Q/E")]
	[Range(0,180)]
	public float RotationRatchet 			= 45.0f;			// Amount of degrees for instant turns with Oculus (with Q/E)

	[Tooltip("Something related to slow down side and backward move")]
	public float BackAndSideDampen 			= 0.5f;				// Oculus variable, I think it's something to slow down sides and backward move
	
	private bool prevHatLeft 				= false;
	private bool prevHatRight 				= false;
	private float SimulationRate 			= 60f;
	private float RotationScaleMultiplier 	= 1.0f;
	private bool  SkipMouseRotation 		= false;
	private float MoveScale 				= 1.0f;
	private float MoveScaleMultiplier 		= 1.0f;
	private float YRotation 				= 0.0f;
	float boostTimeElapsed 					= 0.0f;

	[Header("JetPack System")]
	[Tooltip("Starting amount of fuel")]
	public float Fuel						= 0f;				// Fuel starting amount

	[Tooltip("Consumption of fuel each second")]
	public float fuelConsumption 			= 0.1f;				// Fuel consumption

	[Tooltip("Jetpack force")]
	[Range(0,4000)]
	public float jetPackForce = 1000f;							// Jetpack force

	[Tooltip("Reload time (s) for side boost")]
	public float boostReloadTime 			= 4.0f;				// Cooldown time for boost
	
	[Tooltip("Duration (in seconds) of the side boost")]
	public float boostDuration				= 0.3f;				// Duration (in seconds) of the side boost
	[Tooltip("Speed of the side boost")]
	public float boostPower					= 3;				// Speed of the force impulse
	private float _boostPower				= 0;
	private bool isSideBoostReady			= true;				// Flag to use SideBoost

	[Tooltip("How much sprint mode multiplies your speed")]
	public float turboSpeedMultiplier		= 2f;				// Multiplyer for sprint mode
	private float _turboSpeedMultiplier 	= 1f;

	
	

	[Header("Landing helper")]
	[Tooltip("Amount of height from ground the landing helper kicks in")]
	public float heightSoftLanding  		= 3.0f;				// Height used to soften the landing (to prevent sickness from falling)

	[Tooltip("Strenght of the deceleration when the landing helper kicks in")]
	public float dragSoftLanding 			= 8.0f;				// Drag amount that is set on the rigidbody to slow down your landing

	[Tooltip("Default drag. Advised to set it to 1")]
	public float dragDefault	 			= 1.0f;				// Default drag amount that is set on the rigidbody

	private float maxHeightGrounded;							// Maximum amount of height admitted to switch from grounded mode fo jetpack mode
	private float heightGroundedBias		= 0.1f;				// Permitted bias and variation
	
	private Vector3 m_GroundContactNormal;
	private RaycastHit hitInfo;

	[Space(20)]
	[Tooltip("Switch form normal mode to tank mode")]
	public bool HmdRotatesY 				= true;
	private float Acceleration 				= 0.1f;
	private float RotationAmount 			= 1.5f;

	#region MovementSettings
	[System.Serializable]
	public class MovementSettings
	{
		[Header("Ground Speeds")]
		public float ForwardGroundSpeed 	= 2.5f;   			// Speed when walking forward
		public float BackwardGroundSpeed 	= 2.0f;  			// Speed when walking backwards
		public float StrafeGroundSpeed 		= 1.8f;    			// Speed when walking sideways

		[Header("Air Speeds")]
		public float ForwardAirSpeed 		= 1.95f;			// Speed when flying forward
		public float BackwardAirSpeed 		= 1.6f;				// Speed when flying backwards
		public float StrafeAirSpeed			= 1.3f;				// Speed when flying sideways
		
		[HideInInspector] public float CurrentTargetSpeed = 4f;	// Speed that will be applied to Rigidbody
		
		// Gets the proper speed based on input, while grounded
		public void UpdateDesiredTargetSpeedGround(Vector2 input)
		{
			// If no input, return no speed
			if (input == Vector2.zero) 	return;
			
			// Strafe movement (the slower one)
			if (input.x > 0 || input.x < 0)
			{
				CurrentTargetSpeed = StrafeGroundSpeed * Mathf.Abs (input.x);
			}
			// Backwards movement
			if (input.y < 0)
			{
				CurrentTargetSpeed = BackwardGroundSpeed * Mathf.Abs (input.y);
			}
			// Forward movement
			// Handled last as if strafing and moving forward at the same time forwards speed should take precedence
			if (input.y > 0)
			{
				CurrentTargetSpeed = ForwardGroundSpeed * Mathf.Abs (input.y);
			}
		}
		
		// Gets the proper speed based on input, while flying
		public void UpdateDesiredTargetSpeedAir(Vector2 input)
		{
			// If no input, return no speed
			if (input == Vector2.zero) return;
			
			// Strafe movement (the slower one)
			if (input.x > 0 || input.x < 0)
			{
				CurrentTargetSpeed = StrafeAirSpeed * Mathf.Abs (input.x);
			}
			// Backwards movement
			if (input.y < 0)
			{
				CurrentTargetSpeed = BackwardAirSpeed * Mathf.Abs (input.y);
			}
			// Forward movement
			// Handled last as if strafing and moving forward at the same time forwards speed should take precedence
			if (input.y > 0)
			{
				CurrentTargetSpeed = ForwardAirSpeed * Mathf.Abs (input.y);
			}
		}
		
	}

	#endregion
	public MovementSettings movementSettings = new MovementSettings();			// MovementSettings with speed values and evaluators
	
	// Use this for initialization
	void Awake ()
	{
		Controller = gameObject.GetComponent<CharacterController>();

		if(Controller == null)
			Debug.LogWarning("OVRPlayerController: No CharacterController attached.");

		OVRCameraRig[] CameraControllers;
		CameraControllers = gameObject.GetComponentsInChildren<OVRCameraRig>();

		if(CameraControllers.Length == 0)
			Debug.LogWarning("OVRPlayerController: No OVRCameraRig attached.");
		else if (CameraControllers.Length > 1)
			Debug.LogWarning("OVRPlayerController: More then 1 OVRCameraRig attached.");
		else
			CameraController = CameraControllers[0];

		YRotation = transform.rotation.eulerAngles.y;

	}

	void Start()
	{
		maxHeightGrounded = GetComponent<CapsuleCollider> ().height / 2 + heightGroundedBias;

	}

	// Update is called once per frame
	void Update ()
	{
		// Helps to redirect the velocity while turning 
		RotateView ();

		float moveInfluence = Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;

		Quaternion ort = (HmdRotatesY) ? CameraController.centerEyeAnchor.rotation : transform.rotation;
		Vector3 ortEuler = ort.eulerAngles;
		ortEuler.z = ortEuler.x = 0f;
		ort = Quaternion.Euler (ortEuler);


		Vector3 euler = transform.rotation.eulerAngles;

		bool curHatLeft = OVRGamepadController.GPC_GetButton (OVRGamepadController.Button.LeftShoulder);
		// Input controls to instantly rotate from GamePad
		// implementing a sort of Input.GetKeyDown
		if (curHatLeft && !prevHatLeft)
		{
			euler.y -= RotationRatchet;
		}
		prevHatLeft = curHatLeft;


		bool curHatRight = OVRGamepadController.GPC_GetButton (OVRGamepadController.Button.RightShoulder);
		// Input controls to instantly rotate from GamePad
		// implementing a sort of Input.GetKeyDown
		if (curHatRight && !prevHatRight)
		{
			euler.y += RotationRatchet;
		}
		prevHatRight = curHatRight;

		// Input control to instantly rotate from keyboard
		if (Input.GetKeyDown (KeyCode.Q))
		{
			euler.y -= RotationRatchet;
		}
		// Input control to instantly rotate from keyboard
		if (Input.GetKeyDown (KeyCode.E))
		{
			euler.y += RotationRatchet;
		}


		float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;

		if (!SkipMouseRotation)
		{
			euler.y += Input.GetAxis ("Mouse X") * rotateInfluence * 3.25f;
		}

		moveInfluence = SimulationRate * Time.deltaTime * Acceleration * 0.1f * MoveScale * MoveScaleMultiplier;
		#if !UNITY_ANDROID // LeftTrigger not avail on Android game pad
		moveInfluence *= 1.0f + OVRGamepadController.GPC_GetAxis (OVRGamepadController.Axis.LeftTrigger);
		#endif

		transform.rotation = Quaternion.Euler (euler);


		float rightAxisX = OVRGamepadController.GPC_GetAxis (OVRGamepadController.Axis.RightXAxis);

		euler.y += rightAxisX * rotateInfluence;

		transform.rotation = Quaternion.Euler (euler);

		Vector2 input = GetInput(IsGrounded());


		// Handles turbo setting
		if (Input.GetKey (KeyCode.LeftShift))
		{
			_turboSpeedMultiplier = turboSpeedMultiplier;
		}
		else
		{
			_turboSpeedMultiplier = 1;
		}

		
		// always move along the camera forward as it is the direction that it being aimed at
		Vector3 desiredMove;
		if (HmdRotatesY) 
		{
			// If I have a boostPower, which means, I'm doing a side boost, I don't have to normalize and I ignore every side movement
			if (_boostPower != 0)
			{
				desiredMove = CameraController.centerEyeAnchor.forward*input.y + CameraController.centerEyeAnchor.right*_boostPower/_turboSpeedMultiplier;
				desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal);
			}
			else
			{
				desiredMove = CameraController.centerEyeAnchor.forward*input.y + CameraController.centerEyeAnchor.right*input.x;
				desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;
			}
		}
		else
		{
		
			if (_boostPower != 0)
			{
				desiredMove = transform.forward*input.y + transform.right*_boostPower/_turboSpeedMultiplier;
				desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal);
			}
			else
			{
				desiredMove = transform.forward*input.y + transform.right*input.x;
				desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;
			}
		}

		// Set speed for x and z axis if necessary
		desiredMove.x = desiredMove.x*movementSettings.CurrentTargetSpeed * _turboSpeedMultiplier;
		desiredMove.z = desiredMove.z*movementSettings.CurrentTargetSpeed * _turboSpeedMultiplier;

		// Give rigidbody velocity, keeping the previous vertical velocity (so we're able to constantly add force)
		GetComponent<Rigidbody>().velocity = new Vector3(desiredMove.x, GetComponent<Rigidbody>().velocity.y, desiredMove.z);
		
		if ((OVRGamepadController.GPC_GetButton (OVRGamepadController.Button.A) || Input.GetButton("Jump")) && Fuel > 0)
		{
			// Add vertical force for JetPack and decreases fuel
			GetComponent<Rigidbody>().AddForce (Vector3.up * jetPackForce * Time.deltaTime);
			Fuel -= fuelConsumption * Time.deltaTime;
		}
		
		// Use forward boots if it's off cooldown
		if (( Input.GetKeyDown (KeyCode.JoystickButton1)	|| Input.GetKeyDown (KeyCode.Z)) && isSideBoostReady)
		{
			// Start boost
			StartCoroutine (SideBoostLeft());
			// Start cooldown time
			StartCoroutine (ReloadSideBoost());
		}
		
		// Use backward boots if it's off cooldown
		if (( Input.GetKeyDown (KeyCode.JoystickButton2)	|| Input.GetKeyDown (KeyCode.C)) && isSideBoostReady)
		{
			// Start boost
			StartCoroutine (SideBoostRight());
			// Start cooldown time
			StartCoroutine (ReloadSideBoost());
		}
	}

	// Check if the player is touching the ground or not
	public bool IsGrounded()
	{
		RaycastHit hitInfo;

		// If the player is touching the ground
		if (Physics.Raycast(transform.position, -Vector3.up, out hitInfo, maxHeightGrounded ))
		{
			GetComponent<Rigidbody>().drag = dragDefault;
			m_GroundContactNormal = hitInfo.normal;
			return true;
		}
		else
		{
			// If the player is in air, and he's letting himself fall
			if(Physics.Raycast(transform.position, -Vector3.up, out hitInfo, heightSoftLanding) && !Input.GetButton("Jump")
			   && !OVRGamepadController.GPC_GetButton (OVRGamepadController.Button.A))
			{
				GetComponent<Rigidbody>().drag = dragSoftLanding;
			}
			else
			{
				GetComponent<Rigidbody>().drag = dragDefault;
			}
			m_GroundContactNormal = Vector3.up;
			return false;
		}
	}
	
	// Get a Vector2 Input, adjusting movement speed according to direction
	private Vector2 GetInput(bool isGrounded)
	{
		Vector2 input;

		if (OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftXAxis) == 0 
		    && OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftYAxis) == 0) 
		{
			input = new Vector2
			{
				x = Input.GetAxis("Horizontal"),
				y = Input.GetAxis("Vertical")
			};
		} 
		else 
		{
			input	= new Vector2
			{
				x = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftXAxis),
				y = OVRGamepadController.GPC_GetAxis(OVRGamepadController.Axis.LeftYAxis)*-1
			};
		}

		if (isGrounded)
		{
			movementSettings.UpdateDesiredTargetSpeedGround(input);
		}
		else
		{
			movementSettings.UpdateDesiredTargetSpeedAir(input);
		}
		
		return input;
	}
	
	private void RotateView()
	{
		//avoids the mouse looking if the game is effectively paused
		if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
		
		// get the rotation before it's changed
		float oldYRotation = transform.eulerAngles.y;
		
		if (IsGrounded())
		{
			// Rotate the rigidbody velocity to match the new direction that the character is looking
			Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
			GetComponent<Rigidbody>().velocity = velRotation*GetComponent<Rigidbody>().velocity;
		}
	}


	public IEnumerator SideBoostLeft()
	{
		_boostPower = -boostPower;
		yield return new WaitForSeconds(boostDuration);
		_boostPower = 0f;
	}
	
	public IEnumerator SideBoostRight()
	{
		_boostPower = boostPower;
		yield return new WaitForSeconds(boostDuration);
		_boostPower = 0f;
	}

	// Coroutine to reload boost
	public IEnumerator ReloadSideBoost()
	{
		isSideBoostReady = false;
		yield return new WaitForSeconds(4);
		isSideBoostReady = true;
	}



}
*/