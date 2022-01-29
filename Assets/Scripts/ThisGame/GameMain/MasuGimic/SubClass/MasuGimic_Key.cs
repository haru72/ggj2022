using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Key : MasuGimic
	{
		public MasuGimic_Key( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
		}

		public override void Update()
		{
		}

		public override void Action()
		{
			MasuGimicBehaviour.gameObject.SetActive( false );
		}

		public override bool CanTouch()
		{
			return true;
		}
	}
}