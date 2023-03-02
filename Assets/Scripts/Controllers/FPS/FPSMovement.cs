/*
**			Created by Christian Viñolo on 15/07/2021
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Controllers
{
	public class FPSMovement : FPSBase
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		[SerializeField] private float walkSpeed = 6.0f;
		[SerializeField, Range(0.0f, 0.5f)] private float smoothTime = 0.01f;

		private Vector2 moveDir = new();
		private Vector2 smoothDir = new();
		private Vector2 currentVelocity = new();

		private float moveSpeed = 0.0f;

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
			Unfreeze();
		}

		private void FixedUpdate()
		{
			UpdateMovement();
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

		private void UpdateMoveDir(Vector2 inputDir) => moveDir = inputDir;

		private void UpdateMovement()
		{
			moveDir.Normalize();

			smoothDir = Vector2.SmoothDamp(smoothDir, moveDir, ref currentVelocity, smoothTime);

			var moveVelocity = (transform.right * smoothDir.x + transform.forward * smoothDir.y).normalized * moveSpeed;

			if (moveVelocity != Vector3.zero)
			{
				player.Move(player.Position + moveVelocity * Time.fixedDeltaTime);
			}
		}

		private void Freeze(int _) => moveSpeed = 0.0f;
		private void Unfreeze() => moveSpeed = walkSpeed;

		protected override void EnableInput()
		{
			player.Input.onLeftStick += UpdateMoveDir;
		}

		protected override void DisableInput()
		{
			player.Input.onLeftStick -= UpdateMoveDir;
		}

		#endregion
	}
}