using System.Collections;
using UnityEngine;

namespace GameMainSpace.CameraSpace
{
	public class CameraController
	{
		Vector3 _range;
		public void Setup( Vector3 targetPos )
		{
			_range = targetPos - Camera.main.transform.position;
			
		}

		public void Update( Vector3 targetPos )
		{
			Camera.main.transform.position = targetPos - _range;
		}
	}
}