using UnityEngine;

public class SystemController : Singleton<SystemController>
{
	SystemBehaviour _systemBehaviour;
	public SystemBehaviour SystemBehaviour
	{
		get
		{
			return _systemBehaviour;
		}
	}


	protected override bool IsAddManager()
	{
		return false;
	}

	protected override void InitSub()
	{
		base.InitSub();

		var gameObject = new GameObject( "SystemBehaviour" );
		_systemBehaviour = gameObject.AddComponent<SystemBehaviour>();

		GameObject.DontDestroyOnLoad( gameObject );

	}

}