/*
**			Created by Christian on 02/03/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Controllers
{
	public class CubeController : MonoBehaviour
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

		[SerializeField] private float moveSpeed;
		[SerializeField] private float rotationSpeed;

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

		private void Update()
		{
			UpdateMovement();
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		private void UpdateMovement()
		{
			var position = new Vector3(transform.position.x, Mathf.Cos(Time.time * moveSpeed));
			var rotation = new Vector3(Mathf.Sin(Time.time * rotationSpeed), Mathf.Cos(Time.time * rotationSpeed));

			transform.position = position;
			transform.Rotate(rotation);
		}

		#endregion
	}
}