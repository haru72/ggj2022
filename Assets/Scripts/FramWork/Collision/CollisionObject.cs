using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 当たり判定用ベースクラス
/// 当たったオブジェクトを保持しておく
/// </summary>
abstract public class CollisionObject : ICollisionObject
{
	protected CollisionController.CollisionLayer _layer;
	protected List<CollisionController.CollisionLayer> _targetLayerList = new List<CollisionController.CollisionLayer>();
	bool _active = true;
	protected bool _isDestroy = false;
	List<ICollisionObject> _collisionObjectList = new List<ICollisionObject>();

	#region ICollisionObject

	void ICollisionObject.SetActive( bool active )
	{
		SetActive( active );
	}
	public void SetActive( bool active )
	{
		_active = active;
	}

	public bool IsActive()
	{
		return _active;
	}

	bool ICollisionObject.IsActive()
	{
		return IsActive();
	}


	bool ICollisionObject.IsHit( ICollisionObject collisionObject )
	{
		return IsHitSub( collisionObject );
	}

	bool ICollisionObject.IsHited( ICollisionObject collisionObject )
	{
		return IsHited( collisionObject );
	}

	void ICollisionObject.Hit( ICollisionObject collisionObject )
	{
		if( ! IsHited( collisionObject ) )
		{
			_collisionObjectList.Add( collisionObject );
		}

		HitSub( collisionObject );
	}

	void ICollisionObject.Reset()
	{
		Reset();
	}
	public void Reset()
	{
		ResetSub();
		ClearHitList();
	}

	void ICollisionObject.Remove(ICollisionObject collisionObject)
	{
		_collisionObjectList.Remove( collisionObject );
	}


	bool ICollisionObject.IsDestory()
	{
		return _isDestroy;
	}

	void ICollisionObject.Update()
	{
		Update();
	}

	CollisionController.CollisionLayer ICollisionObject.GetLayer()
	{
		return _layer;
	}

	List<CollisionController.CollisionLayer> ICollisionObject.GetTargetLayerList()
	{
		return _targetLayerList;
	}

	#endregion

	public void Init()
	{
		InitSub();
		CollisionController.GetInstance().Add( this );
	}

	void ClearHitList()
	{
		//クリアする前に関連の collisionObject から自分を外す
		foreach( var collisionObject in _collisionObjectList )
		{
			collisionObject.Remove( this );
		}
		_collisionObjectList.Clear();
	}

	public bool IsHited( ICollisionObject collisionObject )
	{
		if( _collisionObjectList.Contains( collisionObject ) )
		{
			return true;
		}
		return false;
	}

	#region abstract
	abstract protected void InitSub();
	abstract protected bool IsHitSub( ICollisionObject collisionObject );
	abstract protected void ResetSub();
	abstract protected void HitSub( ICollisionObject collisionObject );
	abstract protected void Update();
	public void Destroy()
	{
		ClearHitList();
		_isDestroy = true;
	}
	#endregion


}