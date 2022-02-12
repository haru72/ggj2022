using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameMainSpace.PhaseSpace
{
	public class StartupPhase : PhaseBase
	{
		Coroutine _coroutine;
		public StartupPhase( GameMainData gameMainData ) : base( gameMainData )
		{
			//最初アクティブじゃないとエラー
			GameMainData.UIGameMainManager.GameUIOpen();
			if( !FadeManager.IsActive() )
			{
				_coroutine = SystemController.GetInstance().SystemBehaviour.StartCoroutine( StartupCoroutine() );
				SceneManager.LoadScene( "Fade" , LoadSceneMode.Additive );
				SceneManager.LoadScene( "InputManager" , LoadSceneMode.Additive );
			}
		}
		IEnumerator StartupCoroutine()
		{
			MyAudioController.GetInstance().AddLoadTarget_All();
			yield return MyAudioController.GetInstance().LoadAsync();
			_coroutine = null;
			MyAudioController.GetInstance().PlayBGM( MyAudioController.BGMType.Title );
		}

		public override void Update()
		{
			if( FadeManager.IsFadeEnd() && _coroutine == null )
			{
				GameMainData.UIGameMainManager.GameUIClose();

				GameMainData.PhaseController.ChangePhase( PhaseType.Tutorial );
				//GameMainData.PhaseController.ChangePhase( PhaseType.Main );
			}
		}
	}
}