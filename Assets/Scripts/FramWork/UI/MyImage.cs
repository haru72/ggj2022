using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent( typeof( RawImage ) )]
public class MyImage : MonoBehaviour
{
	public enum UVAnimType
	{
		None,
		UAnim,
		VAnim,
	}

	[SerializeField]
	UVAnimType _uvAnimType = UVAnimType.None;
	[SerializeField][Range( 0 , 1 )]
	float _offsetAdd = 0;
	float _offset = 0;
	RawImage _rawImage;

	Action _updateAction = null;

	void Start()
	{
		_rawImage = GetComponent<RawImage>();

		if( _uvAnimType == UVAnimType.UAnim )
		{
			_updateAction = UpdateAnimU;
		}
		if( _uvAnimType == UVAnimType.VAnim )
		{
			_updateAction = UpdateAnimV;
		}
	}

	void Update()
    {
		if( _updateAction != null )
		{
			_updateAction();
		}

    }

	void UpdateAnimU()
	{
		_offset += _offsetAdd;
		if( _offset >= 1 )
		{
			_offset = 0;
		}

		var uvRect = _rawImage.uvRect;
		uvRect.x = _offset;
		_rawImage.uvRect = uvRect;
	}

	void UpdateAnimV()
	{
		_offset += _offsetAdd;
		if( _offset >= 1 )
		{
			_offset = 0;
		}

		var uvRect = _rawImage.uvRect;
		uvRect.y = _offset;
		_rawImage.uvRect = uvRect;
	}
}
