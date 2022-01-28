using System;
using System.Collections.Generic;

public class Singleton<T> where T : new()
{
	static T _instance;

	/// <summary>
	/// managerに登録する
	/// 登録されたシングルトンは自動的に削除される
	/// </summary>
	/// <returns></returns>
	virtual protected bool IsAddManager()
	{
		return true;
	}

	/// <summary>
	/// 生成時にのみ呼び出す
	/// </summary>
	/// <returns></returns>
	protected void Init()
	{
		InitSub();
	}

	virtual protected void InitSub()
	{
	}

	static public T GetInstance()
	{
		if( _instance == null )
		{
			_instance = new T();
			var singleton = _instance as Singleton<T>;
			singleton.Init();
			if( singleton.IsAddManager() )
			{
				SingletonManager.GetInstance().Add( _instance , () =>
				{
					singleton.Release();
				} );
			}

		}
		return _instance;
	}

	protected Singleton()
	{
	}


	public void Release()
	{
		_instance = (T)(object)null;
	}

}
