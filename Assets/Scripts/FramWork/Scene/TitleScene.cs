using System.Collections;
using UnityEngine;

namespace Assets.Scripts.FramWork.Scene
{
	public class TitleScene : MonoBehaviour
	{

		void Awake()
		{
		}

		private void FixedUpdate()
		{
		}

		private void Update()
		{
			if( Input.GetKeyDown( KeyCode.Space ) )
			{
				SceneController.GetInstance().ChangeScene( "GameMain" );
			}
		}

		private void LateUpdate()
		{
		}
	}
}