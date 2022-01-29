using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace.WallSpacce
{
	public class WallManager
	{
		Wall[] WallAry;
		Dictionary<string , bool> IsWallDic = new Dictionary<string , bool>();
		public WallManager( Transform transform , Func<Vector3 , Vector2Int> calcMasuByPos )
		{
			WallAry = transform.GetComponentsInChildren<Wall>();
			Debug.Log( WallAry.Length );
			foreach( var wall in WallAry )
			{
				var masu = calcMasuByPos( wall.transform.position );
				var str = ToDicKey( masu );
				if( IsWallDic.ContainsKey( str ) )
				{
					Debug.LogError( "壁重複あり:" + str );
					continue;
				}
				IsWallDic.Add( str , true );
			}

		}

		string ToDicKey( Vector2Int masu )
		{
			return ( masu.x + "_" + masu.y );
		}

		public bool IsInWall( Vector2Int masu )
		{
			var str = ToDicKey( masu );
			return IsWallDic.ContainsKey( str );
		}

	}
}