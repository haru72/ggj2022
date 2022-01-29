using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMain
	{
		GameMainData GameMainData { get; }

		public GameMain( GameMainScene gameMainScene )
		{
			GameMainData = new GameMainData( gameMainScene.gameObject , gameMainScene );
			
			SystemController.GetInstance().SystemBehaviour.StartCoroutine( StartupCoroutine() );
		}

		IEnumerator StartupCoroutine()
		{
			yield return null;
			GameMainData.GameMainUtility.ChangeCandleNum( GameMainData.DefineInterface.StartCandleNum );
		}


		public void Update()
		{

			// キー入力で移動
			if( Input.GetKeyDown( KeyCode.W ) ) // 上
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
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
				if( IsForegroundGoal() )
				{
					//ゴール手前 誤ってろうそくを使わないようにする
				}
				else if( GameMainData.Player.GetSetCandleNum > 1 )
				{
					bool isLookCandle = InputAction_Candle();
					if( !isLookCandle )
					{
						InputAction_Curse();
					}
				}
			}

			GameMainData.Player.Update();
			GameMainData.CleanController.Update();
			GameMainData.MasuGimicManager.Update();
			CollisionController.GetInstance().Update();
			MyAnimationController.GetInstance().Update();
		}

		bool IsForegroundGoal()
		{
			var playerMasu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
			var forwardMasu = GameMainData.GameMainUtility.CalcForwardMasu( playerMasu , GameMainData.Player.GetForward );

			var masuGimic = GameMainData.MasuGimicManager.GetMasuGimic( forwardMasu , MasuGimicSpace.GimicType.Goal );
			if( masuGimic == null )
			{
				return false;
			}

			return true;
		}

		bool InputAction_Candle()
		{
			var playerMasu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
			var forwardMasu = GameMainData.GameMainUtility.CalcForwardMasu( playerMasu , GameMainData.Player.GetForward );

			var masuGimic = GameMainData.MasuGimicManager.GetMasuGimic( forwardMasu , MasuGimicSpace.GimicType.Candlestick );
			if( masuGimic == null  )
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
			var masuList = GameMainData.GameMainUtility.CalcForwardMasuList(
			playerMasu , GameMainData.Player.GetForward , GameMainData.DefineInterface.CleanRange );
			var posList = new List<Vector3>();
			foreach( var masu in masuList )
			{
				var pos = GameMainData.PanelController.CalcPosByMasu( masu );
				posList.Add( pos );

				//呪い削除
				var masuGimic = GameMainData.MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.Curse );
				if( masuGimic != null && masuGimic.CanTouch() )
				{
					masuGimic.Action();
				}
			}

			//浄化発生
			GameMainData.CleanController.Setup( posList );

			var candleNum = GameMainData.Player.GetSetCandleNum - 1;
			GameMainData.GameMainUtility.ChangeCandleNum( candleNum );
		}

	}
}
