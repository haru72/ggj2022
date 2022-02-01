using System;
using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_DropCandle : MasuGimic
	{
		bool _canTouch = true;

		public MasuGimic_DropCandle( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
		}

		public override void Update()
		{
		}


		public override void Action()
		{
			_canTouch = false;
			MasuGimicBehaviour.gameObject.SetActive( false );
		}

		public override bool CanTouch()
		{
			return _canTouch;
		}

	}
}