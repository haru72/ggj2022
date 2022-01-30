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

			var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.Pos );
			var pos = GameMainData.PanelController.CalcPosByMasu( masu );
			GameMainData.Player.SetPos( pos );
			GameMainData.CameraController.Setup( pos );
			GameMainData.PhaseController.ChangePhase( PhaseSpace.PhaseType.Startup );

			SystemController.GetInstance().SystemBehaviour.StartCoroutine( StartupCoroutine() );
		}

		IEnumerator StartupCoroutine()
		{
			yield return null;
			GameMainData.GameMainUtility.ChangeCandleNum( GameMainData.DefineInterface.StartCandleNum );
			FadeManager.FadeIn( null );
		}


		public void Update()
		{
			GameMainData.PhaseController.Update();

			CollisionController.GetInstance().Update();
			MyAnimationController.GetInstance().Update();
		}

	}
}
