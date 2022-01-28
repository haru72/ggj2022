using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollisionController : Singleton<CollisionController>
{
	public enum CollisionLayer
	{
		Chara,
		Wall,
	};
	Dictionary<CollisionLayer , List<ICollisionObject>> _collisionListDic = new Dictionary<CollisionLayer , List<ICollisionObject>>();

	protected override void InitSub()
	{
		base.InitSub();
		var enumAry = Enum.GetValues( typeof( CollisionLayer ) );
		foreach( var val in enumAry )
		{
			_collisionListDic.Add( (CollisionLayer)val , new List<ICollisionObject>() );
		}
	}

	public void Add( ICollisionObject collisionObject )
	{
		_collisionListDic[ collisionObject .GetLayer() ].Add( collisionObject );
	}

	public void Update()
	{
		var removeListDic = new Dictionary< CollisionLayer , List<ICollisionObject>>();

		foreach( var keyVal in _collisionListDic )
		{
			var collisionList = keyVal.Value;
			foreach( var collision in collisionList )
			{
				if( ! collision.IsDestory() )
				{
					collision.Update();
				}
			}
		}

		foreach( var keyVal in _collisionListDic )
		{
			var removeList = new List<ICollisionObject>();
			var collisionList = keyVal.Value;
			foreach( var collision in collisionList )
			{
				if( collision.IsDestory() )
				{
					if( ! removeList.Contains( collision ) )
					{
						removeList.Add( collision );
					}
					continue;
				}
				if( ! collision.IsActive() )
				{
					continue;
				}
				//同時に複数のオブジェクトと当たった場合、全部とヒット処理をする
				var targetLayerList = collision.GetTargetLayerList();
				foreach( var targetLayer in targetLayerList )
				{
					if( ! _collisionListDic.ContainsKey( targetLayer ) )
					{
						continue;
					}
					var collisionList2 = _collisionListDic[targetLayer ];
					foreach( var collision2 in collisionList2 )
					{
						if( collision == collision2 )
						{
							continue;
						}

						if( collision2.IsDestory() )
						{
							if( !removeList.Contains( collision2 ) )
							{
								removeList.Add( collision2 );
							}
							continue;
						}

						if( !collision2.IsActive() )
						{
							continue;
						}


						if( collision.IsHit( collision2 ) )
						{
							collision.Hit( collision2 );
						}
					}
				}

				if( collision.IsDestory() )
				{
					if( ! removeList.Contains( collision ) )
					{
						removeList.Add( collision );
					}
				}
			}

			removeListDic.Add( keyVal.Key , removeList );
		}

		foreach( var keyVal in removeListDic )
		{
			var removeList = keyVal.Value;
			foreach( var remove in removeList )
			{
				_collisionListDic[ keyVal.Key ].Remove( remove );
			}
		}

	}
}