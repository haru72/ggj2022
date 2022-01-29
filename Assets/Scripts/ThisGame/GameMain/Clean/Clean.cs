using System.Collections;
using UnityEngine;

namespace GameMainSpace.CleanSpace
{
	public class Clean
	{
		GameObject GameObject { get; }
		float _timer = 0;
		const float EndTime = 0.3f;

		bool IsActive => GameObject.activeSelf;

		public Clean( GameObject gameObject )
		{
			GameObject = gameObject;
		}

		public void Setup( Vector3 pos ) 
		{
			GameObject.SetActive( true );
			GameObject.transform.position = pos + Vector3.zero;

			_timer = 0;
		}

		public void Update()
		{
			if( ! IsActive )
			{
				return;
			}
			_timer += Time.deltaTime;
			if( _timer >= EndTime )
			{
				GameObject.SetActive( false );
			}
		}

	}
}