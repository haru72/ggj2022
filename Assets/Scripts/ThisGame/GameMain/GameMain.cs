using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace
{
	public class GameMain
	{
		GameMainData GameMainData { get; }

		public GameMain( GameMainScene gameMainScene )
		{
			GameMainData = new GameMainData( gameMainScene.gameObject , gameMainScene );

			var masu = GameMainData.PanelController.CalcMasuByPos( GameMainData.Player.Pos );
			var pos = GameMainData.PanelController.CalcPosByMasu( masu );
			GameMainData.Player.SetPos( pos );
			GameMainData.CameraController.Setup( pos );
			GameMainData.PhaseController.ChangePhase( PhaseSpace.PhaseType.Startup );

			SystemController.GetInstance().SystemBehaviour.StartCoroutine( StartupCoroutine() );
		}

		IEnumerator StartupCoroutine()
		{
			{
				var particleGenerateData = new MyParticle.GenerateData()
				{
					_groupId = MyParticle.ParticleController.ParticleId.Purify,
					_particleGroupInitData = new MyParticle.GenerateData.ParticleGroupInitData()
					{
						_maxNum = 12,
						_particleBaseObj = Resources.Load<GameObject>( "Prefabs/Particle_Purify" ),
					}
				};
				MyParticle.ParticleController.GetInstance().GenerateParticleGroup( particleGenerateData );
			}

			yield return null;
			GameMainData.GameMainUtility.ChangeCandleNum( GameMainData.DefineInterface.StartCandleNum );
			FadeManager.FadeIn( null );
			MyAudioController.GetInstance().PlayBGM( MyAudioController.BGMType.Game );

		}


		public void Update()
		{
			GameMainData.PhaseController.Update();

			CollisionController.GetInstance().Update();
			MyAnimationController.GetInstance().Update();
			MyParticle.ParticleController.GetInstance().Update();
		}

	}
}
