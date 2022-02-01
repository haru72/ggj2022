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
			if( isInWall )
			{
				return true;
			}

			var masuGimicCandle = GameMainData.MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.Candlestick );
			if( masuGimicCandle != null )
			{
				return true;
			}


			var masuGimicGoal = GameMainData.MasuGimicManager.GetMasuGimic( masu, MasuGimicSpace.GimicType.Goal );
			if( masuGimicGoal != null && masuGimicGoal.CanTouch() )
			{
				if( GameMainData.Player.GetSetKeyNum <= 0 )
				{
					return true;
				}
			}

			return false;
		}

		public void ChangeCandleNum( int candleNum )
		{
			GameMainData.Player.GetSetCandleNum = candleNum;
			GameMainData.UIGameMainManager.SetCandleNum( candleNum );

			if( candleNum == 0 )
			{

				var screenPos = RectTransformUtility.WorldToScreenPoint( Camera.main , GameMainData.Player.GetNowMasu + new Vector3( 0.5f , 3 , 0 ) );

				var pos = new Vector2(
					( screenPos.x - Screen.width / 2 ) * 1f / GameMainData.Canvas.transform.localScale.x ,
					( screenPos.y - Screen.height / 2 ) * 1f / GameMainData.Canvas.transform.localScale.y
				);


				GameMainData.UIGameMainManager.CandleSpeechBubbleOpenCanNotUse( pos );
			}
			else
			{
				GameMainData.UIGameMainManager.CandleSpeechBubbleClose();
			}

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