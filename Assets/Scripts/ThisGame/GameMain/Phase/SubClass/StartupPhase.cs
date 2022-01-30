using System.Collections;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public class StartupPhase : PhaseBase
	{
		public StartupPhase( GameMainData gameMainData ) : base( gameMainData )
		{
			//最初アクティブじゃないとエラー
			GameMainData.UIGameMainManager.GameUIOpen();
		}

		public override void Update()
		{
			if( FadeManager.IsFadeEnd() )
			{
				GameMainData.UIGameMainManager.GameUIClose();

				GameMainData.PhaseController.ChangePhase( PhaseType.Tutorial );
				//GameMainData.PhaseController.ChangePhase( PhaseType.Main );
			}
		}
	}
}