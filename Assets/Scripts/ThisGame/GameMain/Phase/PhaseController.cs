using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public enum PhaseType
	{
		Startup,
		Tutorial,
		Main,
		GameClear,
		GameOver,
	}

	public class PhaseController
	{
		Dictionary<PhaseType , Type> SubClassDic => new Dictionary<PhaseType , Type>()
		{
			{ PhaseType.Startup , typeof(StartupPhase) },
			{ PhaseType.Tutorial , typeof(TutorialPhase) },
			{ PhaseType.Main , typeof(MainPhase) },
			{ PhaseType.GameClear, typeof(GameClearPhase) },
			{ PhaseType.GameOver, typeof(GameOverPhase) },
		};

		GameMainData GameMainData { get; }
		PhaseBase _phaseBase = null;




		public PhaseController( GameMainData gameMainData )
		{
			GameMainData = gameMainData;
		}

		public void ChangePhase( PhaseType phaseType )
		{
			_phaseBase = (PhaseBase)Activator.CreateInstance( SubClassDic[ phaseType ] , new object[]{ GameMainData } );
		}

		public void Update()
		{
			if( _phaseBase == null )
			{
				return;
			}

			_phaseBase.Update();
		}
	}
}
