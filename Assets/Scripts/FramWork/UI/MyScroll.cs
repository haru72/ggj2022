using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// scroll view を操作するクラス
/// 
/// オブジェクトのサイズと数からContentのサイズを割り出す
/// 
/// </summary>
public class MyScroll : MonoBehaviour
{
	public interface IRecord
	{
		void SetupGameObject( GameObject gameObject );
	}

	ScrollRect _scrollRect;
	GameObject _contentObj;
	GameObject _recordBaseObj;
	List<IRecord> _recordList;
	List<GameObject> _recordObjList = new List<GameObject>();

	public void Init( GameObject recordBaseObj )
	{
		_scrollRect = transform.GetComponent<ScrollRect>();
		_contentObj = transform.Find( "Viewport" ).Find( "Content" ).gameObject;
		_recordBaseObj = recordBaseObj;
	}

	public void SetRecordList( List<IRecord> recordList )
	{
		_recordList = recordList;

		var recordHeight = _recordBaseObj.GetComponent<RectTransform>().sizeDelta.y;
		var contentRect = _contentObj.GetComponent<RectTransform>();
		contentRect.sizeDelta = new Vector2( contentRect.sizeDelta.x , recordHeight * _recordList.Count );

		for( int i = 0 ; i < _recordObjList.Count ; i++ )
		{
			GameObject.Destroy( _recordObjList[i] );
		}
		_recordObjList.Clear();

		for( int i = 0 ; i < _recordList.Count ; i++ )
		{
			GameObject obj = GameObject.Instantiate<GameObject>( _recordBaseObj );
			obj.transform.SetParent( _contentObj.transform , false );
			obj.transform.localPosition = new Vector3( obj.transform.localPosition.x , -_recordObjList.Count * recordHeight - recordHeight/2 , obj.transform.localPosition.z );
			_recordObjList.Add( obj );
		}


		for( int i = 0 ; i < _recordList.Count ; i++ )
		{
			_recordObjList[ i ].SetActive( true );
			_recordList[ i ].SetupGameObject( _recordObjList[ i ] );
		}

	}
	public void AddRecord( IRecord record )
	{
		_recordList.Add( record );
		
		var recordHeight = _recordBaseObj.GetComponent<RectTransform>().sizeDelta.y;

		var obj = GameObject.Instantiate<GameObject>( _recordBaseObj );
		obj.transform.SetParent( _contentObj.transform , false );
		obj.transform.localPosition = new Vector3( obj.transform.localPosition.x , -_recordObjList.Count * recordHeight - recordHeight / 2 , obj.transform.localPosition.z );
		_recordObjList.Add( obj );
		obj.SetActive( true );
		record.SetupGameObject( obj );


		var contentRect = _contentObj.GetComponent<RectTransform>();
		contentRect.sizeDelta = new Vector2( contentRect.sizeDelta.x , recordHeight * _recordList.Count );

		
		for( int i = 0 ; i < _recordObjList.Count ; i++ )
		{
			var recordObj = _recordObjList[i];
			recordObj.transform.localPosition = new Vector3( recordObj.transform.localPosition.x , -i * recordHeight - recordHeight / 2 , recordObj.transform.localPosition.z );
		}
		

	}

	public void SetScrollRate( float rate )
	{
		_scrollRect.verticalNormalizedPosition = 1f-rate;
	}

	public Vector3 GetFirstRecordPos()
	{
		return _recordObjList[ 0 ].transform.position;
	}

	public Vector3 GetRecordPos( int index )
	{
		return _recordObjList[ index ].transform.position;
	}

	public Vector3 GetLastRecordPos()
	{
		return _recordObjList[ _recordObjList.Count - 1 ].transform.position;
	}

	public int GetRecordCount()
	{
		return _recordList.Count;
	}

	public IRecord GetRecord( int index )
	{
		return _recordList[ index ];
	}
}