/*
**			Created by Christian Viñolo on 15/07/2021
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Controllers
{
	public class FPSCameraLook : FPSBase
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		private const float camLimit = 90.0f;

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		[SerializeField] private Camera cam = null;
		[SerializeField] private float mouseSensitivity = 3.5f;
		[SerializeField, Range(0.0f, 0.5f)] private float smoothTime = 0.03f;

		private Vector2 lookDir = new Vector2();
		private Vector2 lookDelta = new Vector2();
		private Vector2 lookVelocity = new Vector2();

		private float camPitch = 0.0f;
		private float sensitivity = 0.0f;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		#endregion

		/*#####################################################################
		###############################  UNITY  ###############################
		#####################################################################*/
		#region Unity Methods

		protected override void Awake()
		{
			base.Awake();
		}

		private void Start()
		{
			UnFreeze();
		}

		private void Update()
		{
			UpdateCameraLook();
		}

		private void OnValidate()
		{
			UnFreeze();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		private void UpdateLookDir(Vector2 inputDir) => lookDir = inputDir;

		private void UpdateCameraLook()
		{
			lookDelta = Vector2.SmoothDamp(lookDelta, lookDir, ref lookVelocity, smoothTime);

			camPitch -= lookDelta.y * sensitivity;
			camPitch = Mathf.Clamp(camPitch, -camLimit, camLimit);

			cam.transform.localEulerAngles = Vector3.right * camPitch;
			transform.Rotate(Vector3.up * lookDelta.x * sensitivity);
		}

		private void Freeze(int _) => sensitivity = 0;
		private void UnFreeze() => sensitivity = mouseSensitivity;

		protected override void EnableInput()
		{
			player.Input.onRightStick += UpdateLookDir;
		}

		protected override void DisableInput()
		{
			player.Input.onRightStick -= UpdateLookDir;
		}

		#endregion
	}
}