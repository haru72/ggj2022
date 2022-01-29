using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMain
	{
		GameMainData GameMainData { get; }


		public GameMain( GameObject gameObject )
		{
			GameMainData = new GameMainData( gameObject );
		}

		public void Update()
		{
			CollisionController.GetInstance().Update();

			// キー入力で移動
			if( Input.GetKeyDown( KeyCode.W ) ) // 上
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos ); // (*)引数の座標は仮です。
			}
			else if( Input.GetKeyDown( KeyCode.A ) ) // 左
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );
				GameMainData.Player.Move( nextPos );
			}
			else if( Input.GetKeyDown( KeyCode.S ) ) // 下
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}
			else if( Input.GetKeyDown( KeyCode.D ) ) //右
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			} else if( Input.GetKeyDown(  KeyCode.Space ) )
			{
				bool isLookCandle = InputAction_Candle();

				if( ! isLookCandle )
				{
					InputAction_Curse();
				}

			}


			GameMainData.Player.Update();
			GameMainData.CleanController.Update();
			GameMainData.MasuGimicManager.Update();
		}


		bool InputAction_Candle()
		{
			var playerMasu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
			var forwardMasu = GameMainData.GameMainUtility.CalcForwardMasu( playerMasu , GameMainData.Player.GetForward );

			var masuGimic = GameMainData.MasuGimicManager.GetMasuGimic( forwardMasu );
			if( masuGimic == null  )
			{
				return false;
			}
			if( masuGimic.GimicType != MasuGimicSpace.GimicType.Candlestick )
			{
				return false;
			}

			if( masuGimic.CanTouch() )
			{
				masuGimic.Action();
			}

			return true;
		}

		void InputAction_Curse()
		{
			var playerMasu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
			var masuList = GameMainData.GameMainUtility.CalcForwardMasuList( playerMasu , GameMainData.Player.GetForward , 2 );
			var posList = new List<Vector3>();
			foreach( var masu in masuList )
			{
				var pos = GameMainData.PanelController.CalcPosByMasu( masu );
				posList.Add( pos );

				//呪い削除
				var masuGimic = GameMainData.MasuGimicManager.GetMasuGimic( masu );
				if( masuGimic != null &&
					masuGimic.GimicType == MasuGimicSpace.GimicType.Curse &&
					masuGimic.CanTouch()
				)
				{
					Debug.Log( "assaffdg" );
					masuGimic.Action();
				}
			}

			//浄化発生
			GameMainData.CleanController.Setup( posList );
		}

	}
}
