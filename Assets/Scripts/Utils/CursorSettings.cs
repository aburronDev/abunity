/*
**			Created by Christian Viñolo on 04/02/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Utils
{
	public class CursorSettings : MonoBehaviour
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

		[SerializeField] private bool isVisible = false;
		[SerializeField] private CursorLockMode lockMode = CursorLockMode.Locked;

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

		private void Awake()
		{
			Cursor.visible = isVisible;
			Cursor.lockState = lockMode;
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		#endregion
	}
}