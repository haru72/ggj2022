using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMain
	{
		GameMainData GameMainData { get; }


		public GameMain( GameObject gameObject )
		{
			GameMainData = new GameMainData( gameObject );
		}

		public void Update()
		{
			CollisionController.GetInstance().Update();

			// キー入力で移動
			if (Input.GetKeyDown(KeyCode.W))
			{
				GameMainData.Player.Move(PlayerSpace.Player.E_MoveDirection.Forward);	// 前
			}
			else if (Input.GetKeyDown(KeyCode.A))
            {
				GameMainData.Player.Move(PlayerSpace.Player.E_MoveDirection.Left);		// 左
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				GameMainData.Player.Move(PlayerSpace.Player.E_MoveDirection.Back);		// 後ろ
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				GameMainData.Player.Move(PlayerSpace.Player.E_MoveDirection.Right);		// 右
			}
		}

	}
}
