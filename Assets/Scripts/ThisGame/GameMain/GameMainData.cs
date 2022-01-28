using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainData : PlayerSpace.Player.IPlayer
	{
		public GameObject GameObject { get; }
		public PlayerSpace.Player Player { get; }

		public GameMainData( GameObject gameObject )
		{
			GameObject = gameObject;
			Player = new PlayerSpace.Player(gameObject.transform.Find("Chara").gameObject, this);
		}
	
		bool PlayerSpace.Player.IPlayer.CanMove(Vector2Int nextMasu)
        {
			return true;
        }
	}


}
