using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMainData
	{
		public GameObject GameObject { get; }
		public GameMainData( GameObject gameObject )
		{
			GameObject = gameObject;
		}
	}
}
