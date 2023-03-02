/*
**			Created by Christian Viñolo on 06/08/2022
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Controllers
{
	public class PlayerController : MonoBehaviour
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

		[SerializeField] private PlatformerController controller;
		[SerializeField] private float moveSpeed = 6f;  //   6f original
		[SerializeField] private float jumpHeight = 4f; //   4f original
		[SerializeField] private float jumpTime = 0.5f; // 0.5f original

		private Vector3 velocity;
		private float jumpVelocity;
		private float gravity;
		private float horizontalInput;

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

		private void Start()
		{
			SetGravity();
			SetJumpVelocity();
		}

		private void Update()
		{
			ResetVerticalVelocity();

#if UNITY_EDITOR
			CheckMovementInput();
			CheckJumpInput();
#endif
			UpdateGravity();
			UpdateMovement();
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		private void CheckJumpInput()
		{
			/*if (Input.GetKeyDown(KeyCode.Space))
			{
				UpdateJump();
			}*/
		}

		private void CheckMovementInput()
		{
			//horizontalInput = Input.GetAxisRaw("Horizontal");
		}

		private void ResetVerticalVelocity()
		{
			if (controller.IsAbove || controller.IsBelow)
			{
				velocity.y = 0;
			}
		}

		private void SetGravity()
		{
			gravity = -(2 * jumpHeight) / Mathf.Pow(jumpTime, 2);
		}

		private void SetJumpVelocity()
		{
			jumpVelocity = Mathf.Abs(gravity) * jumpTime;
		}

		private void UpdateGravity()
		{
			velocity.y += gravity * Time.deltaTime;
		}

		private void UpdateJump()
		{
			if (controller.IsBelow)
			{
				velocity.y = jumpVelocity;
			}
		}

		private void UpdateMovement()
		{
			velocity.x = horizontalInput * moveSpeed;
			controller.UpdateMovement(velocity * Time.deltaTime);
		}

		#endregion
	}
}