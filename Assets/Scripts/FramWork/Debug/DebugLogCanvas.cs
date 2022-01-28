using UnityEngine;
using UnityEngine.UI;

public class DebugLogCanvas : Singleton<DebugLogCanvas>
{
	DebugLogBehaviour _debugLogBehaviour;

	protected override bool IsAddManager()
	{
		return false;
	}

	protected override void InitSub()
	{
		if( _debugLogBehaviour == null )
		{
			_debugLogBehaviour = GameObject.Instantiate<DebugLogBehaviour>( Resources.Load<DebugLogBehaviour>( "DebugLogCanvas" ) );
			GameObject.DontDestroyOnLoad( _debugLogBehaviour.gameObject );
			_debugLogBehaviour.Init();
			_debugLogBehaviour.gameObject.SetActive( false );
		}
	}
	
	public void Open()
	{
		_debugLogBehaviour.gameObject.SetActive( true );
	}

	public void Close()
	{
		_debugLogBehaviour.gameObject.SetActive( false );
	}

	public void Update()
	{
		/*
		if( Input.GetKeyDown( KeyCode.D ) )
		{
			Open();
		}
		*/
	}

	public void AddStr( string str )
	{
		_debugLogBehaviour.AddStr( str );
	}

}