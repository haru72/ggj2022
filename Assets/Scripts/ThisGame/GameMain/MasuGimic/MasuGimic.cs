using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public abstract class MasuGimic
	{
		protected MasuGimicBehaviour MasuGimicBehaviour { get; }
		public MasuGimic( MasuGimicBehaviour masuGimicBehaviour )
		{
			MasuGimicBehaviour = masuGimicBehaviour;
		}

		public GimicType GimicType => MasuGimicBehaviour.GimicType;

		public abstract void Update();

		public abstract void Action();
		public abstract bool CanTouch();
	}
}