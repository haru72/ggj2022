using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMainSpace.CleanSpace
{
	public class CleanController
	{
		Transform Transform { get; }
		List<List<Clean>> _cleanListList = new List<List<Clean>>();
		int _index = 0;

		public CleanController( Transform transform , int num )
		{
			var objBase = transform.Find( "Clean" ).gameObject;
			objBase.SetActive(false);
			for( int n = 0 ;n < 2 ; n++ )
			{
				var list = new List<Clean>();
				_cleanListList.Add( list );
				for( int i = 0 ; i < num ; i++ )
				{
					var obj = GameObject.Instantiate<GameObject>( objBase );
					obj.SetActive( false );
					list.Add( new Clean( obj ) );
				}
			}
		}

		public void Setup( List<Vector3> posList )
		{
			for( int i = 0 ; i < posList.Count ; i++ )
			{
				var clean = _cleanListList[ _index ][ i ];
				clean.Setup( posList[ i ] );
			}

			_index++;
			if( _index >= _cleanListList.Count )
			{
				_index = 0;
			}
		}

		public void Update()
		{
			foreach( var cleanList in _cleanListList )
			{
				foreach( var clean in cleanList )
				{
					clean.Update();
				}
			}
		}
	}
}