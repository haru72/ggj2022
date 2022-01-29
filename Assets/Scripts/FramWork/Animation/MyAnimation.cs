using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class MyAnimation
{
	public class CallbackCls
	{
		public bool _isLoop;
		public List<Action> _callbackList = new List<Action>();
	}

	Animator _animator;

	string _newStateName = "";
	string _stateName = "";
	int _priority = 0;
	CallbackCls _callbackCls = new CallbackCls();
	int _callbackIndex = 0;

	public void Init( Animator animator )
	{
		_animator = animator;

		var animBehaviour = animator.GetComponent<AnimBehaviour>();
		if( animBehaviour != null )
		{
			animBehaviour.SetCallBack( Callback );
		}
		MyAnimationController.GetInstance().Add( this );
	}

	void Callback()
	{
		//callback内でPlay()が呼ばれたときに、
		//間違って_callbackIndexがインクリメントされないように、
		//_callbackIndexの後にcallback呼ぶ
		int nowCallbackIndex = _callbackIndex;
		if( nowCallbackIndex >= _callbackCls._callbackList.Count )
		{
			return;
		}

		_callbackIndex++;
		if(
			_callbackCls._isLoop &&
			_callbackIndex >= _callbackCls._callbackList.Count
		)
		{
			_callbackIndex = 0;
		}

		_callbackCls._callbackList[ nowCallbackIndex ]();

	}

	public void SetBool( string paramName , bool value )
	{
		_animator.SetBool( paramName , value );
	}

	public bool Play( string stateName , int priority = 0 , Action callback = null )
	{
		var callbackCls = new CallbackCls();
		callbackCls._isLoop = true;
		if( callback != null )
		{
			callbackCls._callbackList.Add( callback );
		}

		return Play( stateName , priority , callbackCls );
	}

	public bool Play( string stateName , int priority , CallbackCls callbackCls )
	{
		if( IsPlaying( _stateName ) )
		{
			if( _priority > priority )
			{
				return false;
			}
		}

		_newStateName = stateName;
		_priority = priority;

		_callbackCls = callbackCls;
		_callbackIndex = 0;

		return true;
	}

	public bool IsEnd()
	{
		var stateInfo = _animator.GetCurrentAnimatorStateInfo( 0 );
		return ( ! stateInfo.loop ) && ( stateInfo.normalizedTime >= 1f );
	}

	public bool IsPlaying( string stateName )
	{
		var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
		if( ! stateInfo.IsName( stateName ) )
		{
			return false;
		}

		if( IsEnd() )
		{
			return false;
		}

		return true;
	}

	public void Update()
	{
		if( String.IsNullOrEmpty( _newStateName ) )
		{
			return;
		}
		_animator.Play( _newStateName );
		_stateName = _newStateName;
		_newStateName = "";
	}

	public void Destroy()
	{
		MyAnimationController.GetInstance().Remove( this );
	}

}
