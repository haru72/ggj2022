using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Key : MasuGimic
	{
		bool _canTouch = true;
		public MasuGimic_Key( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
		}

		public override void Update()
		{
		}

		public override void Action()
		{
			MasuGimicBehaviour.gameObject.SetActive( false );
			_canTouch = false;
		}

		public override bool CanTouch()
		{
			return _canTouch;
		}
	}
}