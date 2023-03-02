/*
**			Created by Christian Viñolo on 06/08/2022
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Utils
{
	public struct RaycastOrigin
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		private const float OFFSET = -2f;

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		private Vector2 topLeft, topRight;
		private Vector2 bottomLeft, bottomRight;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties


		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		public Vector2 GetHorizontalOrigin(float dir)
		{
			return dir == -1 ? bottomLeft : bottomRight;
		}

		public Vector2 GetVerticalOrigin(float dir)
		{
			return dir == -1 ? bottomLeft : topLeft;
		}

		public Vector2 GetDescendSlopeOrigin(float dir)
		{
			return dir == -1 ? bottomRight : bottomLeft;
		}

		public void UpdateOrigins(Collider collider, float expandAmount)
		{
			Bounds bounds = collider.bounds;
			bounds.Expand(expandAmount * OFFSET);

			bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
			bottomRight = new Vector2(bounds.max.x, bounds.min.y);
			topLeft = new Vector2(bounds.min.x, bounds.max.y);
			topRight = new Vector2(bounds.max.x, bounds.max.y);
		}

		#endregion
	}
}