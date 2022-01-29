using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Goal : MasuGimic
	{
		bool _isLock = true;
		MyAnimation MyAnimation { get; }

		public MasuGimic_Goal( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
			MyAnimation = new MyAnimation();
			MyAnimation.Init( masuGimicBehaviour.GetComponent<Animator>() );
		}

		public override void Update()
		{
		}

		public override void Action()
		{
			_isLock = false;
			MyAnimation.Play( "Open" );
		}

		public override bool CanTouch()
		{
			return _isLock;
		}
	}
}