/*
**			Created by Christian Viñolo on 01/03/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using abunity.Input;
using UnityEngine;

namespace abunity.Controllers
{
	public class ShipController : MonoBehaviour
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

		[SerializeField] private Rigidbody2D rb;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		public AbuInput Input { get; set; } = new();

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

		public void Move(Vector2 dir)
		{
			rb.velocity = dir;
		}

		#endregion
	}
}