using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimicCollision :CollisionObject
	{
		protected override void InitSub()
		{
		}
		protected override void Update()
		{
		}

		protected override void ResetSub()
		{
		}

		protected override bool IsHitSub( ICollisionObject collisionObject )
		{
			var cleanCollision = collisionObject as CleanSpace.CleanCollision;
			if( cleanCollision != null )
			{

				return true;
			}
			return false;
		}

		protected override void HitSub( ICollisionObject collisionObject )
		{
		}

	}
}