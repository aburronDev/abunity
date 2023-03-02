/*
**			Created by Christian Viñolo on 06/08/2022
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using UnityEngine;

namespace abunity.Utils
{
	public struct CollisionData
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

		private bool isAbove, isBelow;
		private bool isLeft, isRight;
		private bool climbingSlope;
		private bool descendingSlope;

		private float previousSlopeAngle;
		private float currentSlopeAngle;

		private Vector3 previousVelocity;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		public bool IsAbove => isAbove;
		public bool IsBelow => isBelow;
		public bool ClimbingSlope => climbingSlope;

		public float CurrentSlopeAngle { set => currentSlopeAngle = value; }

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		public void Reset(Vector3 velocity)
		{
			isAbove = isBelow = false;
			isLeft = isRight = false;
			climbingSlope = false;
			descendingSlope = false;

			previousSlopeAngle = currentSlopeAngle;
			currentSlopeAngle = 0;

			previousVelocity = velocity;
		}

		public void UpdateHorizontalDir(float dir)
		{
			isLeft = dir == -1;
			isRight = dir == 1;
		}

		public void UpdateVerticalDir(float dir)
		{
			isBelow = dir == -1;
			isAbove = dir == 1;
		}

		public void UpdateClimbSlope(float slopeAngle)
		{
			isBelow = true;
			climbingSlope = true;
			currentSlopeAngle = slopeAngle;
		}

		public void UpdateDescendSlope(float slopeAngle)
		{
			isBelow = true;
			descendingSlope = true;
			currentSlopeAngle = slopeAngle;
		}

		public void UpdateVerticalClimbSlopeVelocity(ref Vector3 velocity)
		{
			if (climbingSlope)
			{
				velocity.y = Mathf.Tan(currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
			}
		}

		public void UpdateHorizontalClimbSlopeVelocity(ref Vector3 velocity)
		{
			if (climbingSlope)
			{
				velocity.x = velocity.y / Mathf.Tan(currentSlopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
			}
		}

		public void UpdateHorizontalDescendSlopeVelocity(ref Vector3 velocity)
		{
			if (descendingSlope)
			{
				descendingSlope = false;
				velocity = previousVelocity;
			}
		}

		public bool IsPreviousSlopeAngle(float slopeAngle)
		{
			return slopeAngle == previousSlopeAngle;
		}

		#endregion
	}
}