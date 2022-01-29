using System.Collections;
using UnityEngine;

namespace GameMainSpace.CleanSpace
{
	public class CleanCollision : CollisionObject
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
			return false;
		}

		protected override void HitSub( ICollisionObject collisionObject )
		{
		}



	}
}