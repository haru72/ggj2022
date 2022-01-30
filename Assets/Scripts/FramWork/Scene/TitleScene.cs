using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FramWork.Scene
{
	public class TitleScene : MonoBehaviour
	{
		void Awake()
		{
			if( !FadeManager.IsActive() )
			{
				SceneManager.LoadScene( "Fade" , LoadSceneMode.Additive );
			}
			else
			{
				FadeManager.FadeIn( null );
			}
		}

		private void FixedUpdate()
		{
		}

		private void Update()
		{
			if( ! FadeManager.IsFadeEnd() )
			{
				return;
			}

			if( Input.GetKeyDown( KeyCode.Space ) )
			{
				FadeManager.FadeOut(()=> { SceneController.GetInstance().ChangeScene( "GameMain" ); } );
			}
		}

		private void LateUpdate()
		{
		}
	}
}