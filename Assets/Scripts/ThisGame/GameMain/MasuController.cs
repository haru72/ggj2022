using System.Collections;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace.PanelSpace
{
	public class MasuController
	{
		const float PanelSize = 2;
		Vector3 StartPos { get; }
		public MasuController( Vector3 startPos )
		{
			StartPos = startPos;
		}

		public Vector2Int CalcMasuByPos( Vector3 pos )
		{
			int x = Mathf.FloorToInt( ( pos.x + StartPos.x ) / PanelSize );
			int z = Mathf.FloorToInt( ( pos.z + StartPos.z ) / PanelSize );
			return new Vector2Int( x , z );
		}

		public Vector3 CalcPosByMasu( Vector2Int masu )
		{
			float x = masu.x * PanelSize + StartPos.x;
			float z = masu.y * PanelSize + StartPos.z;
			return new Vector3( x , 0 , z );
		}
	}
}