/*
**			Created by Christian Viñolo on 03/03/2023
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEditor;
using UnityEngine;

namespace abunity.Utils
{
	public class GameExit : MonoBehaviour
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

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		public void ExitGame()
		{
#if UNITY_STANDALONE
			Application.Quit();
#endif
#if UNITY_EDITOR
			EditorApplication.isPlaying = false;
#endif
		}

		#endregion
	}
}