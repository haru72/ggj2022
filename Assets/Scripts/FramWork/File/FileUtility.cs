using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileUtility : Singleton<FileUtility>
{
	public const char Separator_1 = '\n';
	public const char Separator_2 = '\t';

	/// <summary>
	/// Assetsと同じ階層のOutputFileに保存
	/// </summary>
	/// <param name="fileName"></param>
	/// <param name="text"></param>
	public void WriteFileToOutputFolder( string fileName , string text )
	{
		var now = DateTime.Now;
		var writer = new StreamWriter( "OutputFile/" + fileName + now.ToFileTime() + ".txt" , true );
		writer.Write( text);
		writer.Flush();
		writer.Close();
	}


	public string ReadFile( string filePath )
	{
		var str = Resources.Load<TextAsset>( filePath ).text;
		return str;
	}

	public string ReadFile2( string filePath )
	{
		string ret = "";
		var fileInfo = new FileInfo( filePath );
		try
		{
			// 一行毎読み込み
			using( StreamReader sr = new StreamReader( fileInfo.OpenRead() , System.Text.Encoding.UTF8 ) )
			{
				ret = sr.ReadToEnd();
			}
		}
		catch( Exception e )
		{
		}

		return ret;

	}


	public List<List<string>> ReadFileStrToMasterDataStrList( string filePath )
	{
		var fileStr = ReadFile( filePath );
		return ReadFileStrToMasterDataStrListSub( fileStr );
	}



	public List<List<string>> ReadFileStrToMasterDataStrListSub( string fileStr )
	{
		var retList = new List<List<string>>();

		fileStr = fileStr.Replace( "\r" , "" );

		var masterDataStrAry = fileStr.Split( Separator_1 );
		foreach( var masterDataStr in masterDataStrAry )
		{
			if( String.IsNullOrEmpty( masterDataStr ) )
			{
				continue;
			}

			if( masterDataStr.Contains("@@@") )
			{
				continue;
			}

			var tempList = new List<string>();
			var masterDataOneStrAry = masterDataStr.Split( Separator_2 );
			foreach( var masterDataOneStr in masterDataOneStrAry )
			{
				tempList.Add( masterDataOneStr );
			}
			retList.Add( tempList );
		}

		return retList;
	}

}