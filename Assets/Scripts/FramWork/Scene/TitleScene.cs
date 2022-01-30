﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.FramWork.Scene
{
	public class TitleScene : MonoBehaviour
	{

		Coroutine _coroutine;
		void Awake()
		{
			if( !FadeManager.IsActive() )
			{
				_coroutine = StartCoroutine( StartupCoroutine() );
				SceneManager.LoadScene( "Fade" , LoadSceneMode.Additive );
			}
			else
			{
				FadeManager.FadeIn( null );
			}
		}

		IEnumerator StartupCoroutine()
		{
			MyAudioController.GetInstance().AddLoadTarget_All();
			yield return MyAudioController.GetInstance().LoadAsync();
			_coroutine = null;
			MyAudioController.GetInstance().PlayBGM( MyAudioController.BGMType.Title );
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

			if( _coroutine != null )
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