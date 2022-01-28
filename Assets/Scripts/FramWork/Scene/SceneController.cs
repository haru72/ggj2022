using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
	protected override bool IsAddManager()
	{
		return false;
	}

	public void ChangeScene( string sceneName )
	{
		SingletonManager.GetInstance().ReleaseAll();
		SceneManager.LoadScene( sceneName );
	}

}
