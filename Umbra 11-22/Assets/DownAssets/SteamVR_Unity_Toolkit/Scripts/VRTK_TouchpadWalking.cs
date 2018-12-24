namespace VRTK
{
    using UnityEngine;


    [RequireComponent(typeof(VRTK_PlayerPresence))]
    public class VRTK_TouchpadWalking : MonoBehaviour
    {
        [SerializeField]
        private bool leftController = true;
        public bool LeftController
        {
            get { return leftController; }
            set
            {
                leftController = value;
                SetControllerListeners(controllerManager.left);
            }
        }

        [SerializeField]
        private bool rightController = true;
        public bool RightController
        {
            get { return rightController; }
            set
            {
                rightController = value;
                SetControllerListeners(controllerManager.right);
            }
        }

        public VRTK_ControllerEvents Controller;

        public float maxWalkSpeed = 3f;
        public float deceleration = 0.1f;

        public AudioSource audio;
        public AudioClip[] footsound;

        public Vector3 oldPosition;
        public Vector3 newPosition;

        private SteamVR_ControllerManager controllerManager;
        private Vector2 touchAxis;
        public float movementSpeed = 0f;
		public float strafeSpeed = 0f;

        private bool leftSubscribed;
        private bool rightSubscribed;

        private ControllerInteractionEventHandler TouchpadPressed;
        private ControllerInteractionEventHandler touchpadAxisChanged;
        private ControllerInteractionEventHandler touchpadUntouched;

        private VRTK_PlayerPresence playerPresence;

        public float footstep_interval;
        float footstep_elapsed;

		bool keydown = false;

        private void Awake()
        {
            if (GetComponent<VRTK_PlayerPresence>())
            {
                playerPresence = GetComponent<VRTK_PlayerPresence>();
            }
            else
            {
                Debug.LogError("The VRTK_TouchpadWalking script requires the VRTK_PlayerPresence script to be attached to the [CameraRig]");
                return;
            }

            touchpadAxisChanged = new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
            touchpadUntouched = new ControllerInteractionEventHandler(DoTouchpadTouchEnd);

            controllerManager = GetComponent<SteamVR_ControllerManager>();
        }

        private void Start()
        {
            Utilities.SetPlayerObject(gameObject, VRTK_PlayerObject.ObjectTypes.CameraRig);

            audio = this.transform.GetComponent<AudioSource>();

            var controllerManager = FindObjectOfType<SteamVR_ControllerManager>();

            SetControllerListeners(controllerManager.left);
            SetControllerListeners(controllerManager.right);
					
			footstep_elapsed = maxWalkSpeed / 2;
        }

        private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
        {
            touchAxis = e.touchpadAxis;
        }

        private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e)
        {
            touchAxis = Vector2.zero;
        }

        private void CalculateSpeed(ref float speed, float inputValue)
        {
            if (inputValue != 0f)
            {
                speed = maxWalkSpeed * inputValue;
            }
            else
            {
                Decelerate(ref speed);
            }
        }

        private void Decelerate(ref float speed)
        {
            if (speed > 0)
            {
                speed -= Mathf.Lerp(deceleration, maxWalkSpeed, 0f);
            }
            else if (speed < 0)
            {
                speed += Mathf.Lerp(deceleration, -maxWalkSpeed, 0f);
            }
            else
            {
                speed = 0;
            }

            float deadzone = 0.1f;
            if (speed < deadzone && speed > -deadzone)
            {
                speed = 0;
            }
        }

        private void Move()
        {
            var movement = playerPresence.GetHeadset().forward * movementSpeed * Time.deltaTime;
            var strafe = playerPresence.GetHeadset().right * strafeSpeed * Time.deltaTime;
            float fixY = transform.position.y;
            transform.position += (movement + strafe);
            transform.position = new Vector3(transform.position.x, fixY, transform.position.z);
            newPosition = transform.position;
        }

        private void FixedUpdate()
        {

            if (Controller.touchpadPressed)
            {
                CalculateSpeed(ref movementSpeed, touchAxis.y);
                CalculateSpeed(ref strafeSpeed, touchAxis.x);

				Move();
				PlayFootStepSound(touchAxis.y,touchAxis.x);
				keydown = true;

				//print(touchAxis);
            }
          
            if(!Controller.touchpadPressed)
			{
				if(keydown)
				{
					keydown = false;
					if(footstep_elapsed - 1 >= footstep_interval / maxWalkSpeed)
					{
						audio.PlayOneShot(footsound[Random.Range(0, 2)]);
						footstep_elapsed = 0;
					}

				}
				//PlayFootStepSound(touchAxis.y, touchAxis.x);
            }

            //footstep_elapsed += Time.deltaTime;
        }

		void PlayFootStepSound(float _touchAxisY, float _touchAxisX)
        {	
			footstep_elapsed += Time.deltaTime;

			if(touchAxis.y != _touchAxisY
			   || touchAxis.x != _touchAxisX
                ||  footstep_elapsed >= footstep_interval / maxWalkSpeed)
            {
				audio.PlayOneShot(footsound[Random.Range(0, 2)]);
                footstep_elapsed = 0;
            }

            /*if(keydown)
            {
                audio.PlayOneShot(footsound[Random.Range(0, 2)]);
            }*/

        }

        private void FootStepSound()
        {
            if (Vector3.Distance(newPosition, oldPosition) > 1.5 )
            {
                audio.PlayOneShot(footsound[Random.Range(0, 2)]);
                oldPosition = this.transform.position;
            }
        }

        private void SetControllerListeners(GameObject controller)
        {
            if (controller && controller == controllerManager.left)
            {
                ToggleControllerListeners(controller, leftController, ref leftSubscribed);
            }
            else if (controller && controller == controllerManager.right)
            {
                ToggleControllerListeners(controller, rightController, ref rightSubscribed);
            }
        }

        private void ToggleControllerListeners(GameObject controller, bool toggle, ref bool subscribed)
        {
            var controllerEvent = controller.GetComponent<VRTK_ControllerEvents>();
            if (controllerEvent && toggle && !subscribed)
            {
                controllerEvent.TouchpadAxisChanged += touchpadAxisChanged;
                controllerEvent.TouchpadTouchEnd += touchpadUntouched;
                subscribed = true;
            }
            else if (controllerEvent && !toggle && subscribed)
            {
                controllerEvent.TouchpadAxisChanged -= touchpadAxisChanged;
                controllerEvent.TouchpadTouchEnd -= touchpadUntouched;
                touchAxis = Vector2.zero;
                subscribed = false;
            }
        }
    }
}