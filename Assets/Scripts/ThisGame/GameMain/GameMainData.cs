using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainData : PlayerSpace.Player.IPlayer
	{
		public GameObject GameObject { get; }
		public PlayerSpace.Player Player { get; }
		public PanelSpace.MasuController PanelController { get; }
		public WallSpacce.WallManager WallManager { get; }
		public MasuGimicSpace.MasuGimicManager MasuGimicManager { get; }

		GameMainUtility GameMainUtility => new GameMainUtility( this );

		public GameMainData(GameObject gameObject)
		{
			GameObject = gameObject;
			Player = new PlayerSpace.Player(gameObject.transform.Find("Chara").gameObject, this);

			PanelController = new PanelSpace.MasuController( Vector3.zero );

			var fieldTransform = gameObject.transform.Find( "Field" );

			WallManager = new WallSpacce.WallManager( fieldTransform , PanelController.CalcMasuByPos );
			MasuGimicManager = new MasuGimicSpace.MasuGimicManager( fieldTransform , PanelController.CalcMasuByPos );

		}

		bool PlayerSpace.Player.IPlayer.CanMove( Vector3 nextMasu )
		{
			return ! GameMainUtility.IsInWall( nextMasu );
		}

		void PlayerSpace.Player.IPlayer.ZeroCandleCallBack()
		{
		}

		void PlayerSpace.Player.IPlayer.FinishMove( Vector3 nowMasu )
		{
		}
	}


}
