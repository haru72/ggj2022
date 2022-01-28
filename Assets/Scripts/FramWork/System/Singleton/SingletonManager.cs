using System;
using System.Collections.Generic;

public class SingletonManager : Singleton<SingletonManager>
{
	List<System.Action> _releaseFuncList = new List<System.Action>();

	public void Add( object obj , System.Action temp )
	{
		if( obj.GetType() == typeof( SingletonManager ) )
		{
			return;
		}
		_releaseFuncList.Add( temp );
	}

	public void ReleaseAll()
	{

		for( int i = 0 ; i < _releaseFuncList.Count ; i++ )
		{
			_releaseFuncList[i]();
		}

		_releaseFuncList.Clear();
	}
}
