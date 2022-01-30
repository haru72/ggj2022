using System.Collections;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public class TutorialPhase : PhaseBase
	{
		public TutorialPhase( GameMainData gameMainData ) : base( gameMainData )
		{
			gameMainData.UIGameMainManager.TutorialOpen();
		}

		public override void Update()
		{
			if( Input.GetKeyDown(KeyCode.Space) )
			{
				/*
				if(  )
				{
					GameMainData.UIGameMainManager.TutorialNextPage();
				}
				else
				*/
				{
					GameMainData.UIGameMainManager.TutorialClose();
					GameMainData.PhaseController.ChangePhase( PhaseType.Main );
				}
			}
		}
	}
}