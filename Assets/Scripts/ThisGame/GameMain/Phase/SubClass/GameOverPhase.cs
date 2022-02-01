using System.Collections;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public class GameOverPhase : PhaseBase
	{
		public GameOverPhase( GameMainData gameMainData ) : base( gameMainData )
		{
			gameMainData.GameOver.SetActive( true );
			gameMainData.UIGameMainManager.CandleSpeechBubbleClose();
			gameMainData.UIGameMainManager.SpeechBubbleClose();
		}

		public override void Update()
		{
			GameMainData.Player.Update();
			GameMainData.CleanController.Update();
			GameMainData.MasuGimicManager.Update();
		}
	}
}