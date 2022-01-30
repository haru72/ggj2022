using System;
using System.Collections;
using UnityEngine;

namespace GameMainSpace
{
	public class GameOver
	{
		GameObject GameObject { get; }
		AnimBehaviour AnimBehaviour { get; }
		public GameOver( GameObject gameObject , Action callback )
		{
			GameObject = gameObject;
			gameObject.SetActive( false );
			AnimBehaviour = gameObject.GetComponent<AnimBehaviour>();
			AnimBehaviour.SetCallBack( callback );
		}

		public void SetActive( bool active )
		{
			GameObject.SetActive( active );
		}
	}
}