using System.Collections;
using UnityEngine;

namespace GameMainSpace
{
	public class GameMainUtility
	{
		GameMainData GameMainData { get; }
		public GameMainUtility( GameMainData gameMainData )
		{
			GameMainData = gameMainData;
		}

		public bool IsInWall( Vector3 pos )
		{
			var masu = GameMainData.PanelController.CalcMasuByPos( new Vector3( pos.x , 0 , pos.z ) );
			bool isInWall = GameMainData.WallManager.IsInWall( masu );

			return isInWall;
		}
	}
}