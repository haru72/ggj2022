using UnityEngine;
using System;


public class AnimBehaviour : MonoBehaviour
{
	Action _callback;
	public void SetCallBack( Action callback )
	{
		_callback = callback;
	}

	/// <summary>
	/// アニメーション側から呼び出される
	/// </summary>
	public void CallBack()
	{
		if( _callback != null )
		{
			_callback();
		}
	}
}