using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick
{

	public enum FlickDir
	{
		None,
		Up,
		Down,
		Right,
		Left
	}


	const int FLICK_FRAME = 10;

	bool _isChecking;
	FlickDir _flickDir;
	Vector2[] _posFrameAry = new Vector2[FLICK_FRAME];
	float _flickRange = 20f;
	public void Init( float flickRange )
	{
		_flickRange = flickRange;
	}

	void UpdateAllFrame()
	{
		var touchPos = InputController.GetInstance().GetTouchPos();
		for( int i = 0 ; i < _posFrameAry.Length ; i++ )
		{
			_posFrameAry[i] = touchPos;
		}
	}

	void Reset()
	{
		for( int i = 0 ; i < _posFrameAry.Length ; i++ )
		{
			_posFrameAry[i] = new Vector2();
		}
		_isChecking = false;

	}


	public void Update()
	{
		_flickDir = FlickDir.None;
		if( Input.GetMouseButtonDown( 0 ) )
		{
			UpdateAllFrame();
			_isChecking = true;
		} else if( Input.GetMouseButtonUp( 0 ) )
		{
			Reset();
			return;
		} else if( Input.GetMouseButton( 0 ) )
		{
			_flickDir = CalcFlickDir();
			if( _flickDir != FlickDir.None )
			{
				Reset();
				return;
			}
		} else
		{
			return;
		}

		if( ! _isChecking )
		{
			return;
		}

		UpdateFrame();
	}

	void UpdateFrame()
	{
		for( int i = 0 ; i < _posFrameAry.Length - 1 ; i++ )
		{
			int index = _posFrameAry.Length - 1 - i;
			_posFrameAry[index] = _posFrameAry[index - 1];
		}

		_posFrameAry[0] = InputController.GetInstance().GetTouchPos();
	}

	Vector2 GetPosOld()
	{
		return _posFrameAry[_posFrameAry.Length - 1];
	}

	Vector2 GetPosNow()
	{
		return _posFrameAry[0];
	}

	bool IsFlick()
	{
		var distance = Vector2.Distance( GetPosNow() , GetPosOld() );
		return distance >= _flickRange;
	}

	FlickDir CalcFlickDir()
	{
		if( ! IsFlick() )
		{
			return FlickDir.None;
		}

		var vec = (GetPosNow() - GetPosOld()).normalized;
		var dir = FlickDir.None;
		if( vec.x * vec.x > vec.y * vec.y )
		{
			dir = FlickDir.Left;
			if( vec.x > 0 )
			{
				dir = FlickDir.Right;
			}
		} else
		{
			dir = FlickDir.Down;
			if( vec.y > 0 )
			{
				dir = FlickDir.Up;
			}
		}

		return dir;
	}

	public bool IsFlickToDir( FlickDir dir )
	{
		return _flickDir == dir;
	}

}
