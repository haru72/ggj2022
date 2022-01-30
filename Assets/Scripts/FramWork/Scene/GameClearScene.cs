
	using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FramWork.Scene
{
	public class GameClearScene : MonoBehaviour
	{
		Coroutine _coroutine;
		void Awake()
		{
			FadeManager.FadeIn( null );
			MyAudioController.GetInstance().PlayBGM( MyAudioController.BGMType.Clear );
		}

		private void FixedUpdate()
		{
		}

		private void Update()
		{
			if( !FadeManager.IsFadeEnd() )
			{
				return;
			}

			if( _coroutine != null )
			{
				return;
			}

			if( Input.GetKeyDown( KeyCode.Space ) )
			{
				FadeManager.FadeOut( () => { SceneController.GetInstance().ChangeScene( "Title" ); } );
			}
		}

		private void LateUpdate()
		{
		}
	}
}