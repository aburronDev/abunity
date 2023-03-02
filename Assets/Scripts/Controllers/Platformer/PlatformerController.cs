/*
**			Created by Christian Viñolo on 06/08/2022
**
**	Shared and licensed under the Attribution-NonCommercial-ShareAlike
**			4.0 International Creative Commons License
*/

using abunity.Utils;
using UnityEngine;

namespace abunity.Controllers
{
	public class PlatformerController : MonoBehaviour
	{
		/*#####################################################################
		#############################  CONSTANTS  #############################
		#####################################################################*/
		#region Constants

		private const float SKIN_WIDTH = .015f;
		private const int MIN_RAYS = 2;

		#endregion

		/*#####################################################################
		##############################  FIELDS  ###############################
		#####################################################################*/
		#region Fields

		[SerializeField] private new Collider collider;
		[SerializeField] private LayerMask collisionMask;
		[Space]
		[SerializeField] private int horizontalRayCount = 3;
		[SerializeField] private int verticalRayCount = 3;
		[Space]
		[SerializeField] private float maxClimbAngle = 80;
		[SerializeField] private float maxDescendAngle = 80;

		private RaycastOrigin rayOrigin;
		private CollisionData collisionData;

		private float horizontalRaySpacing;
		private float verticalRaySpacing;

		#endregion

		/*#####################################################################
		#############################  PROPERTIES  ############################
		#####################################################################*/
		#region Properties

		public bool IsAbove => collisionData.IsAbove;
		public bool IsBelow => collisionData.IsBelow;

		#endregion

		/*#####################################################################
		###############################  UNITY  ###############################
		#####################################################################*/
		#region Unity Methods

		private void Start()
		{
			CalculateRaySpacing();
		}

		#endregion

		/*#####################################################################
		##############################  METHODS  ##############################
		#####################################################################*/
		#region Methods

		private void CalculateRaySpacing()
		{
			Bounds bounds = collider.bounds;
			bounds.Expand(SKIN_WIDTH * -MIN_RAYS);

			horizontalRayCount = Mathf.Clamp(horizontalRayCount, MIN_RAYS, int.MaxValue);
			verticalRayCount = Mathf.Clamp(verticalRayCount, MIN_RAYS, int.MaxValue);

			horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
			verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
		}

		private float CalculateSlopeAngle(RaycastHit hit)
		{
			return Vector2.Angle(hit.normal, Vector2.up);
		}

		public void UpdateMovement(Vector3 velocity)
		{
			rayOrigin.UpdateOrigins(collider, SKIN_WIDTH);

			collisionData.Reset(velocity);

			if (velocity.y < 0f)
			{
				UpdateDescendSlope(ref velocity);
			}

			if (!Mathf.Approximately(velocity.x, 0f))
			{
				UpdateHorizontalCollisions(ref velocity);
			}

			if (!Mathf.Approximately(velocity.y, 0f))
			{
				UpdateVerticalCollisions(ref velocity);
			}

			transform.Translate(velocity);
		}

		private void UpdateHorizontalCollisions(ref Vector3 velocity)
		{
			float dirX = Mathf.Sign(velocity.x);
			float rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;

			for (int i = 0; i < horizontalRayCount; i++)
			{
				Vector2 origin = rayOrigin.GetHorizontalOrigin(dirX);
				origin += Vector2.up * (horizontalRaySpacing * i);

				if (Physics.Raycast(origin, Vector2.right * dirX, out RaycastHit hit, rayLength, collisionMask))
				{
					float slopeAngle = CalculateSlopeAngle(hit);

					if (i == 0 && slopeAngle <= maxClimbAngle)
					{
						collisionData.UpdateHorizontalDescendSlopeVelocity(ref velocity);

						float slopeStartDistance = 0;

						if (!collisionData.IsPreviousSlopeAngle(slopeAngle))
						{
							slopeStartDistance = hit.distance - SKIN_WIDTH;
							velocity.x -= slopeStartDistance * dirX;
						}

						UpdateClimbSlope(ref velocity, slopeAngle);

						velocity.x += slopeStartDistance * dirX;
					}

					if (!collisionData.ClimbingSlope || slopeAngle >= maxClimbAngle)
					{
						velocity.x = (hit.distance - SKIN_WIDTH) * dirX;
						rayLength = hit.distance;

						collisionData.UpdateVerticalClimbSlopeVelocity(ref velocity);
						collisionData.UpdateHorizontalDir(dirX);
					}
				}
			}
		}

		private void UpdateVerticalCollisions(ref Vector3 velocity)
		{
			float dirY = Mathf.Sign(velocity.y);
			float rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH;

			for (int i = 0; i < verticalRayCount; i++)
			{
				Vector2 origin = rayOrigin.GetVerticalOrigin(dirY);
				origin += Vector2.right * (verticalRaySpacing * i + velocity.x);

				if (Physics.Raycast(origin, Vector2.up * dirY, out RaycastHit hit, rayLength, collisionMask))
				{
					velocity.y = (hit.distance - SKIN_WIDTH) * dirY;
					rayLength = hit.distance;

					collisionData.UpdateHorizontalClimbSlopeVelocity(ref velocity);
					collisionData.UpdateVerticalDir(dirY);
				}
			}

			if (collisionData.ClimbingSlope)
			{
				float dirX = Mathf.Sign(velocity.x);
				rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;

				Vector2 origin = rayOrigin.GetHorizontalOrigin(dirX) + Vector2.up * velocity.y;

				if (Physics.Raycast(origin, Vector2.right * dirX, out RaycastHit hit, rayLength, collisionMask))
				{
					float slopeAngle = CalculateSlopeAngle(hit);

					if (!collisionData.IsPreviousSlopeAngle(slopeAngle))
					{
						velocity.x = (hit.distance - SKIN_WIDTH) * dirX;
						collisionData.CurrentSlopeAngle = slopeAngle;
					}
				}
			}
		}

		private void UpdateClimbSlope(ref Vector3 velocity, float slopeAngle)
		{
			float moveDistance = Mathf.Abs(velocity.x);
			float dirX = Mathf.Sign(velocity.x);

			float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
			float climbVelocityX = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * dirX;

			if (velocity.y <= climbVelocityY)
			{
				velocity.y = climbVelocityY;
				velocity.x = climbVelocityX;

				collisionData.UpdateClimbSlope(slopeAngle);
			}
		}

		private void UpdateDescendSlope(ref Vector3 velocity)
		{
			float dirX = Mathf.Sign(velocity.x);

			Vector2 origin = rayOrigin.GetDescendSlopeOrigin(dirX);

			if (Physics.Raycast(origin, Vector2.down, out RaycastHit hit, Mathf.Infinity, collisionMask))
			{
				float slopeAngle = CalculateSlopeAngle(hit);

				if (slopeAngle != 0 && slopeAngle <= maxDescendAngle)
				{
					if (Mathf.Sign(hit.normal.x) == dirX)
					{
						if (hit.distance - SKIN_WIDTH <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x))
						{
							float moveDistance = Mathf.Abs(velocity.x);

							float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
							float descendVelocityX = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * dirX;

							velocity.y -= descendVelocityY;
							velocity.x = descendVelocityX;

							collisionData.UpdateDescendSlope(slopeAngle);
						}
					}
				}
			}

		}

		#endregion
	}
}