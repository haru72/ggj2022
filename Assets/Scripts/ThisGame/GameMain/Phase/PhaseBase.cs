using System.Collections;
using UnityEngine;

namespace GameMainSpace.PhaseSpace
{
	public abstract class PhaseBase
	{
		protected GameMainData GameMainData { get; }
		public PhaseBase( GameMainData gameMainData )
		{
			GameMainData = gameMainData;
		}

		public abstract void Update();

	}
}
