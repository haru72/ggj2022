using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public class MainPhase : PhaseBase
	{
		public MainPhase( GameMainData gameMainData ) : base( gameMainData )
		{
			GameMainData.UIGameMainManager.GameUIOpen();
		}

		public override void Update()
		{
			InputPlayer();

			GameMainData.Player.Update();
			GameMainData.CleanController.Update();
			GameMainData.MasuGimicManager.Update();

			GameMainData.CameraController.Update( GameMainData.Player.Pos );

		}

		void InputPlayer()
		{
			if( !GameMainData.Player.CanAction() )
			{
				return;
			}

			// キー入力で移動
			if( InputManager.IsPressUp() ) // 上
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}
			else if( InputManager.IsPressLeft() ) // 左
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );
				GameMainData.Player.Move( nextPos );
			}
			else if( InputManager.IsPressDown() ) // 下
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}
			else if( InputManager.IsPressRight() ) //右
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}
			else if( InputManager.IsTriggerAction() )
			{
				if( IsForegroundGoal() )
				{
					//ゴール手前 誤ってろうそくを使わないようにする
				}
				else if( GameMainData.Player.GetSetCandleNum > 0 )
				{
					bool isLookCandle = InputAction_Candle();
					if( !isLookCandle )
					{
						InputAction_Curse();
					}
				}
			}
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
			if( masuGimic == null )
			{
				return false;
			}

			if( masuGimic.CanTouch() )
			{
				masuGimic.Action();
				GameMainData.Player.LightupCandle();
				var candleNum = GameMainData.Player.GetSetCandleNum - 1;
				GameMainData.GameMainUtility.ChangeCandleNum( candleNum );
				GameMainData.UIGameMainManager.AppearCandleMinus();
				GameMainData.OpenLightCandleStickBaloon();
				
				GameMainData.UIGameMainManager.CandleSpeechBubbleClose();

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

			GameMainData.Player.Purify();
			var candleNum = GameMainData.Player.GetSetCandleNum - 1;
			GameMainData.GameMainUtility.ChangeCandleNum( candleNum );
			GameMainData.UIGameMainManager.AppearCandleMinus();
		}

	}
}