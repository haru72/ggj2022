using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainData : PlayerSpace.Player.IPlayer
	{
		public interface IDefine
		{
			int StartCandleNum { get; }
			int CleanRange { get; }
			float PlayerMoveSpeed { get; }
			float PlayerTurnSpeed { get; }
		}

		public GameObject GameObject { get; }
		public PlayerSpace.Player Player { get; }
		public PanelSpace.MasuController PanelController { get; }
		public WallSpacce.WallManager WallManager { get; }
		public MasuGimicSpace.MasuGimicManager MasuGimicManager { get; }
		public CleanSpace.CleanController CleanController { get; }
		public UIGameMainManager UIGameMainManager { get; }
		public CameraSpace.CameraController CameraController { get; }
		public GameOver GameOver { get; }

		public PhaseSpace.PhaseController PhaseController { get; }
		public GameMainUtility GameMainUtility => new GameMainUtility( this );

		public Canvas Canvas { get; }

		public KaisouCounter KaisouCounter { get; }

		public IDefine DefineInterface { get; }

		public GameMainData(GameObject gameObject , IDefine defineInterface )
		{
			GameObject = gameObject;
			DefineInterface = defineInterface;
			Player = new PlayerSpace.Player(gameObject.transform.Find("Chara").gameObject, this);

			PanelController = new PanelSpace.MasuController( Vector3.zero );

			var fieldTransform = gameObject.transform.Find( "Field" );

			WallManager = new WallSpacce.WallManager( fieldTransform , PanelController.CalcMasuByPos );
			MasuGimicManager = new MasuGimicSpace.MasuGimicManager( fieldTransform , PanelController.CalcMasuByPos );

			CleanController = new CleanSpace.CleanController( gameObject.transform.Find( "CleanManager" ) , 10 );

			CameraController = new CameraSpace.CameraController();

			UIGameMainManager = gameObject.transform.Find( "UIGameMainManager" ).GetComponent<UIGameMainManager>();

			GameOver = new GameOver( gameObject.transform.Find( "GameOverCanvas" ).gameObject ,
				()=> {
					SceneController.GetInstance().ChangeScene( "Title" );
				}
			);

			KaisouCounter = new KaisouCounter();

			Canvas = gameObject.transform.Find( "Canvas" ).GetComponent<Canvas>();

			PhaseController = new PhaseSpace.PhaseController( this );
		}


		bool PlayerSpace.Player.IPlayer.CanMove( Vector3 nextMasu )
		{
			return ! GameMainUtility.IsInWall( nextMasu );
		}

		void PlayerSpace.Player.IPlayer.FinishMove( Vector3 nowMasu )
		{
			var masu = PanelController.CalcMasuByPos( nowMasu );

			//Damage
			{
				var masuGimic = MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.Curse );
				if( masuGimic != null && masuGimic.CanTouch() )
				{
					int candleNum = Player.GetSetCandleNum - 1;
					masuGimic.Action();

					if( candleNum < 0 )
					{
						//GameOver
						Player.Dead();
						PhaseController.ChangePhase( PhaseSpace.PhaseType.GameOver );
					}
					else
					{
						//死なないときはロウソクの数を更新
						GameMainUtility.ChangeCandleNum( Player.GetSetCandleNum - 1 );
						UIGameMainManager.AppearCandleMinus();
						Player.Damage();
					}
				}
			}

			//Item
			{
				var masuGimic = MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.Key );
				if( masuGimic != null && masuGimic.CanTouch() )
				{
					Player.GetSetKeyNum = Player.GetSetKeyNum + 1;
					masuGimic.Action();
					Player.Pickup();
				}
			}

			//Goal
			{
				var masuGimic = MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.Goal );
				if( masuGimic != null && masuGimic.CanTouch() )
				{
					Player.GetSetKeyNum = Player.GetSetKeyNum - 1;
					var masuGimic_Goal = masuGimic as MasuGimicSpace.MasuGimic_Goal;
					masuGimic_Goal.SetCallback( ()=> {
						FadeManager.FadeOut(()=> { SceneController.GetInstance().ChangeScene( "GameClear" ); } );
					} );
					masuGimic_Goal.Action();
					PhaseController.ChangePhase( PhaseSpace.PhaseType.GameClear );
				}
			}

			//キャンドル前
			if( Player.GetSetCandleNum > 0 )
			{
				var playerMasu = PanelController.CalcMasuByPos( Player.GetNowMasu );
				var forwardMasu = GameMainUtility.CalcForwardMasu( playerMasu , Player.GetForward );

				var masuGimic = MasuGimicManager.GetMasuGimic( forwardMasu , MasuGimicSpace.GimicType.Candlestick );
				if( masuGimic != null )
				{
					var screenPos = RectTransformUtility.WorldToScreenPoint( Camera.main , Player.GetNowMasu + new Vector3( 0.5f , 3 , 0 ) );

					var pos = new Vector2(
						( screenPos.x - Screen.width / 2 ) * 1f / Canvas.transform.localScale.x ,
						( screenPos.y - Screen.height / 2 ) * 1f / Canvas.transform.localScale.y
					);

					if( masuGimic.CanTouch() )
					{
						UIGameMainManager.CandleSpeechBubbleOpenCanUse( pos );
					}
				}
				else 
				{
					UIGameMainManager.CandleSpeechBubbleClose();
				}
			}

			{
				var masuGimic = MasuGimicManager.GetMasuGimic( masu , MasuGimicSpace.GimicType.DropCandle );
				if( masuGimic != null && masuGimic.CanTouch() )
				{
					masuGimic.Action();
					GameMainUtility.ChangeCandleNum( Player.GetSetCandleNum + 1 );
					Player.PickupChandle();

				}
			}


			var candleList = MasuGimicManager.GetCandleList();
			foreach( var masuGimic_candle in candleList )
			{
				var dif = (Player.Pos - masuGimic_candle.Pos).sqrMagnitude;
				bool active = (dif < 400 );
				masuGimic_candle.SetActiveLight( active );
			}
		}

		float PlayerSpace.Player.IPlayer.MoveSpeed => DefineInterface.PlayerMoveSpeed;
		float PlayerSpace.Player.IPlayer.TurnSpeed => DefineInterface.PlayerTurnSpeed;



		public void OpenLightCandleStickBaloon()
		{
			var str = KaisouCounter.GetKaisouText();

			var screenPos = RectTransformUtility.WorldToScreenPoint( Camera.main , Player.GetNowMasu + new Vector3( 3f , 3.5f , 0 ) );

			var pos = new Vector2(
				( screenPos.x - Screen.width / 2 ) * 1f / Canvas.transform.localScale.x ,
				( screenPos.y - Screen.height / 2 ) * 1f / Canvas.transform.localScale.y
			);

			UIGameMainManager.SpeechBubbleOpen( pos , str );
		}

	}

}
