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
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos ); // (*)引数の座標は仮です。
			}
			else if (Input.GetKeyDown(KeyCode.A)) // 左
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );
				GameMainData.Player.Move( nextPos );
			}
			else if (Input.GetKeyDown(KeyCode.S)) // 下
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.y -= 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}
			else if (Input.GetKeyDown(KeyCode.D)) //右
			{
				var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.GetNowMasu );
				masu.x += 1;
				var nextPos = GameMainData.PanelController.CalcPosByMasu( masu );

				GameMainData.Player.Move( nextPos );
			}

			GameMainData.Player.Update();
		}

	}
}
