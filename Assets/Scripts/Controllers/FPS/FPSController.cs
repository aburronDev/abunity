/*
**			Created by Christian Viñolo on 15/07/2021
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using abunity.Input;
using UnityEngine;

namespace abunity.Controllers
{
	public class FPSController : MonoBehaviour
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

		[SerializeField] private Rigidbody rb;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		public AbuInput Input { get; set; } = new();

		public Vector3 Position => rb.position;
		public Quaternion Rotation => rb.rotation;

		#endregion

		/*#####################################################################
		###############################  UNITY  ###############################
		#####################################################################*/
		#region Unity Methods

		private void Awake()
		{
			Input.Enable();
		}

		private void OnDestroy()
		{
			Input.Disable();
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		public void Move(Vector3 position)
		{
			rb.MovePosition(position);
		}

		#endregion
	}
}