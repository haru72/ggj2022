using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace.PlayerSpace
{
	public class Player
	{
		public interface IPlayer
		{
			bool CanMove( Vector2Int nextMasu );
		}

		GameObject GameObject { get; }
		IPlayer PlayerInterface { get; }

		public Player( GameObject gameObject , IPlayer playerInterface )
		{
			GameObject = gameObject;
			PlayerInterface = playerInterface;
		}

		public void Update()
		{
		}

	}
}
