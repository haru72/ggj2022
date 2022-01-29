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
			if (Input.GetKeyDown(KeyCode.W)) // 上
			{
				GameMainData.Player.Move(new Vector3(0f,0f,1f)); // (*)引数の座標は仮です。
			}
			else if (Input.GetKeyDown(KeyCode.A)) // 左
            {
				GameMainData.Player.Move(new Vector3(-1f, 0f, 0f));
			}
			else if (Input.GetKeyDown(KeyCode.S)) // 下
			{
				GameMainData.Player.Move(new Vector3(0f, 0f, -1f));
			}
			else if (Input.GetKeyDown(KeyCode.D)) //右
			{
				GameMainData.Player.Move(new Vector3(1f, 0f, 0f));
			}

			GameMainData.Player.Update();
		}

	}
}
