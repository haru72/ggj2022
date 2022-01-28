using UnityEngine;
using System;
using System.IO;

public class LogFileCoontroller : Singleton<LogFileCoontroller>
{
	string _fileName;


	protected override bool IsAddManager()
	{
		return false;
	}

	public void Setup( string fileName )
	{
#if DEBUG

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
//		Setup_PC( fileName );
#endif

#endif
	}

	void Setup_PC( string fileName )
	{
		string filePath = Application.dataPath + @"\MyLog\";
		_fileName = filePath + fileName;
		Debug.Log( "_fileName:" + _fileName );

		var tmpPathStr = "";
		var tmpStrAry = _fileName.Split( '\\' );
		for( int i = 0 ; i < tmpStrAry.Length - 1 ; i++ )
		{
			tmpPathStr += tmpStrAry[ i ];
			Debug.Log( "tmpPathStr:" + tmpPathStr );
			if( !Directory.Exists( tmpPathStr ) )
			{
				Directory.CreateDirectory( tmpPathStr );
			}
			tmpPathStr += '\\';

		}

		File.CreateText( _fileName );

	}



	public void AddLine( string str )
	{
#if DEBUG

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
//		AddLine_PC( str );
#endif

#endif
	}

	void AddLine_PC( string str )
	{
		File.AppendAllText( _fileName , str + "\n" );
	}

}