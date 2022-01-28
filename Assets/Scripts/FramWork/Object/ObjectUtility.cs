using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

static public class ObjectUtility
{
	public enum Priority
	{
		Min = 0,	//アイテムとか
		MoreLow,	//
		Low,        //雑魚キャラとか
		Middle,		//プレイヤーとか
		High,       //
		MoreHigh,	//ボスとか
		Max,		//壁とか
	}

	public interface IObject
	{
		Vector3 GetPos();
		Priority GetPriority();
		void Bump();
		bool EnableHit();
	}


	public class ObjectCommonAction
	{

		ObjectActionBase _objectAction;
		IObject _interface;
		Priority _priority = Priority.Middle;

		public ObjectCommonAction( IObject objectInterface )
		{
			_interface = objectInterface;
			ObjectController.GetInstance().Add( this );
		}


		public Vector3 GetPos()
		{
			return _interface.GetPos();
		}

		public void SetPriority( Priority priority )
		{
			_priority = priority;
		}

		public Priority GetPriority()
		{
			return _priority;
		}

		public void Stop()
		{
			_objectAction = null;
		}

		public void Update()
		{
			if( _objectAction != null )
			{
				_objectAction.Update();
			}
		}

		public bool IsActive()
		{
			if( _objectAction == null )
			{
				return false;
			}
			return ( ! _objectAction.IsEnd() );
		}
	}

	abstract class ObjectActionBase
	{
		abstract public void Update();
		abstract public bool IsEnd();
	}

	public class RotateCls
	{
		float _rotateTimer = 0;
		float _rotateSpeed = 0.1f;
		DirUtility.eDir _oldDir;
		DirUtility.eDir _newDir;
		public void Setup( float rotateSpeed , DirUtility.eDir oldDir , DirUtility.eDir newDir )
		{
			_rotateTimer = 0;
			_rotateSpeed = rotateSpeed;
			_oldDir = oldDir;
			_newDir = newDir;
		}

		public float Rotate()
		{
			_rotateTimer += _rotateSpeed;
			if( IsEnd() )
			{
				_rotateTimer = 1f;
			}
			return DirUtility.RotateYToDir( _newDir , _oldDir , _rotateTimer );
		}

		public bool IsEnd()
		{
			return _rotateTimer >= 1f;
		}
	}
}