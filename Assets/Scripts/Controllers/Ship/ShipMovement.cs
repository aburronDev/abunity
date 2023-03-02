/*
**			Created by Christian Viñolo on 02/03/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Controllers
{
	public class ShipMovement : ShipBase
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

		[SerializeField] private float moveSpeed = 10f;
		[Space]
		[SerializeField] private Vector2 initialPosition;
		[SerializeField] private Vector4 bounds;

		private Vector2 moveDir = new();
		private Vector2 newPosition = new();

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
			InitMovement();
		}

		private void FixedUpdate()
		{
			UpdateMovement();
		}

		private void LateUpdate()
		{
			ClampMovement();
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

		private void InitMovement()
		{
			transform.position = initialPosition;
		}

		private void UpdateMoveDir(Vector2 inputDir)
		{
			moveDir = inputDir;
		}

		private void UpdateMovement()
		{
			newPosition = moveSpeed * moveDir;

			ship.Move(newPosition);
		}

		private void ClampMovement()
		{
			var xClamp = Mathf.Clamp(transform.position.x, bounds.x, bounds.y);
			var yClamp = Mathf.Clamp(transform.position.y, bounds.z, bounds.w);

			newPosition.Set(xClamp, yClamp);

			transform.position = newPosition;
		}

		protected override void EnableInput()
		{
			ship.Input.onLeftStick += UpdateMoveDir;
		}

		protected override void DisableInput()
		{
			ship.Input.onLeftStick -= UpdateMoveDir;
		}

		#endregion
	}
}