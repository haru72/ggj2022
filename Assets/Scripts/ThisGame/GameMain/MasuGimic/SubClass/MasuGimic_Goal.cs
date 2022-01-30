using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Goal : MasuGimic
	{
		bool _isLock = true;
		MyAnimation MyAnimation { get; }

		System.Action _callback;

		public MasuGimic_Goal( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
			MyAnimation = new MyAnimation();
			MyAnimation.Init( masuGimicBehaviour.GetComponent<Animator>() );
		}

		public override void Update()
		{
		}

		public void SetCallback( System.Action callback )
		{
			_callback = callback;
		}

		public override void Action()
		{
			_isLock = false;
			MyAnimation.Play( "Open" , 0 , _callback );
		}

		public override bool CanTouch()
		{
			return _isLock;
		}
	}
}