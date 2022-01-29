using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainUtility
	{
		GameMainData GameMainData { get; }
		public GameMainUtility( GameMainData gameMainData )
		{
			GameMainData = gameMainData;
		}

		public bool IsInWall( Vector3 pos )
		{
			var masu = GameMainData.PanelController.CalcMasuByPos( new Vector3( pos.x , 0 , pos.z ) );
			bool isInWall = GameMainData.WallManager.IsInWall( masu );

			return isInWall;
		}

		public Vector2Int CalcForwardMasu( Vector2Int nowMasu , Vector3 forward )
		{

			if( forward.x > 0.5f )
			{
				return new Vector2Int( nowMasu.x + 1 , nowMasu.y );
			}
			else if( forward.x < -0.5f )
			{
				return new Vector2Int( nowMasu.x -1 , nowMasu.y );
			}
			else if( forward.z > 0.5f )
			{
				return new Vector2Int( nowMasu.x , nowMasu.y + 1 );
			}
			else if( forward.z < -0.5f )
			{
				return new Vector2Int( nowMasu.x , nowMasu.y - 1 );
			}

			return Vector2Int.zero;
		}

		public List<Vector2Int> CalcForwardMasuList( Vector2Int nowMasu , Vector3 forward , int range )
		{
			const int width = 3;
			var ret = new List<Vector2Int>();

			Debug.Log( "forward.x :" + forward.x );
			Debug.Log( "forward.z :" + forward.z );

			if( forward.x > 0.5f )
			{
				for( int y = 0 ; y < width ; y++ )
				{
					for( int x = 0 ; x < range ; x++ )
					{
						int tempX = x + 1;
						int tempY = y - 1;
						var masu = new Vector2Int( nowMasu.x + tempX , nowMasu.y + tempY );
						ret.Add( masu );
					}
				}
			}
			else if( forward.x < -0.5f )
			{
				for( int y = 0 ; y < width ; y++ )
				{
					for( int x = 0 ; x < range ; x++ )
					{
						int tempX = -(x + 1);
						int tempY = y - 1;
						var masu = new Vector2Int( nowMasu.x + tempX , nowMasu.y + tempY );
						ret.Add( masu );
					}
				}
			}
			else if( forward.z > 0.5f )
			{
				for( int y = 0 ; y < range ; y++ )
				{
					for( int x = 0 ; x < width ; x++ )
					{
						int tempX = x - 1;
						int tempY = y + 1;
						var masu = new Vector2Int( nowMasu.x + tempX , nowMasu.y + tempY );
						ret.Add( masu );
					}
				}
			}
			else if( forward.z < -0.5f )
			{
				for( int y = 0 ; y < range ; y++ )
				{
					for( int x = 0 ; x < width ; x++ )
					{
						int tempX = x - 1;
						int tempY = -( y + 1 );
						var masu = new Vector2Int( nowMasu.x + tempX , nowMasu.y + tempY );
						ret.Add( masu );
					}
				}
			}

			return ret;

		}
	}
}