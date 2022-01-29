using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimicManager
	{
		Dictionary<GimicType , Type> SubClassDic => new Dictionary<GimicType , Type>()
		{
			{ GimicType.Candlestick , typeof(MasuGimic_Candle) },
			{ GimicType.Curse , typeof(MasuGimic_Curse) },
			{ GimicType.Key , typeof(MasuGimic_Key) },
			{ GimicType.Goal , typeof(MasuGimic_Goal) },
		};

		List<MasuGimic> _masuGimicList = new List<MasuGimic>();
		Dictionary<string , Dictionary<GimicType ,int>> _indexDicDic = new Dictionary<string , Dictionary<GimicType , int>>();

		public MasuGimicManager( Transform transform , Func<Vector3 , Vector2Int> calcMasuByPos )
		{
			var masuGimicBehaviourAry = transform.GetComponentsInChildren<MasuGimicBehaviour>();
			foreach( var masuGimicBehaviour in masuGimicBehaviourAry )
			{
				var masu = calcMasuByPos( masuGimicBehaviour.transform.position );
				var str = ToDicKey( masu );
				
				var masuGimic = ( MasuGimic)Activator.CreateInstance( SubClassDic[ masuGimicBehaviour.GimicType ] , new object[]{ masuGimicBehaviour } );
				if( ! _indexDicDic.ContainsKey( str ) )
				{
					_indexDicDic.Add( str , new Dictionary<GimicType , int>() );
				}
				_indexDicDic[ str ].Add( masuGimicBehaviour.GimicType , _masuGimicList.Count );
				_masuGimicList.Add( masuGimic );
			}
		}

		string ToDicKey( Vector2Int masu )
		{
			return ( masu.x + "_" + masu.y );
		}

		public MasuGimic GetMasuGimic( Vector2Int masu , GimicType gimicType )
		{
			var str = ToDicKey( masu );
			if( !_indexDicDic.ContainsKey( str ) )
			{
				return null;
			}

			if( !_indexDicDic[str].ContainsKey( gimicType ) )
			{
				return null;
			}
			return _masuGimicList[ _indexDicDic[ str ][ gimicType ] ];
		}

		public List<MasuGimic> GetMasuGimicList( Vector2Int masu )
		{
			var str = ToDicKey( masu );
			if( !_indexDicDic.ContainsKey( str ) )
			{
				return null;
			}

			var dic = _indexDicDic[ str ];
			var ret = new List<MasuGimic>();
			foreach( var keyValue in dic )
			{
				ret.Add( _masuGimicList[ keyValue.Value ] );
			}

			return ret;
		}

		public void Update()
		{
			foreach( var masuGimic in _masuGimicList )
			{
				masuGimic.Update();
			}
		}
	}
}