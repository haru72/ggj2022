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
		};

		Dictionary<string,MasuGimic> _masuGimicDic = new Dictionary<string, MasuGimic>();
		public MasuGimicManager( Transform transform , Func<Vector3 , Vector2Int> calcMasuByPos )
		{
			var masuGimicBehaviourAry = transform.GetComponentsInChildren<MasuGimicBehaviour>();
			foreach( var masuGimicBehaviour in masuGimicBehaviourAry )
			{
				var masu = calcMasuByPos( masuGimicBehaviour.transform.position );
				var str = ToDicKey( masu );
				
				var masuGimic = ( MasuGimic)Activator.CreateInstance( SubClassDic[ masuGimicBehaviour.GimicType ] , new object[]{ masuGimicBehaviour } );
				_masuGimicDic.Add( str , masuGimic );
			}
		}

		string ToDicKey( Vector2Int masu )
		{
			return ( masu.x + "_" + masu.y );
		}

		public MasuGimic GetMasuGimic( Vector2Int masu )
		{
			var str = ToDicKey( masu );
			if( ! _masuGimicDic.ContainsKey( str ) )
			{
				return null;
			}
			return _masuGimicDic[ str ];
		}

		public void Update()
		{
			foreach( var keyValue in _masuGimicDic )
			{
				keyValue.Value.Update();
			}
		}
	}
}