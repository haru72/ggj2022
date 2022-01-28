using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainData
	{
		public GameObject GameObject { get; }
		public PlayerSpace.Player Player { get; }

		public GameMainData( GameObject gameObject )
		{
			GameObject = gameObject;
		}
	}
}
