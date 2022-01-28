using UnityEngine;
using System.Collections.Generic;

public interface ICollisionObject
{
	void SetActive( bool active );
	bool IsActive();
	bool IsHit( ICollisionObject collisionObject );
	bool IsHited( ICollisionObject collisionObject );
	void Hit( ICollisionObject collisionObject );
	void Reset();
	void Remove( ICollisionObject collisionObject );
	bool IsDestory();

	CollisionController.CollisionLayer GetLayer();
	List<CollisionController.CollisionLayer> GetTargetLayerList();

	void Update();
}