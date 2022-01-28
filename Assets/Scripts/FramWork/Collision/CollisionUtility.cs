using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollisionUtility
{

	static public bool IsGoOverAtoB( float posA , float movedA , float posB )
	{
		if(
			( ( movedA > 0 ) && ( posA >= posB ) && ( ( posA - movedA ) < posB) ) ||
			( ( movedA < 0 ) && ( posA <= posB ) && ( ( posA - movedA ) > posB) )
		){
			return true;
		}
		return false;
	}

	static public bool IsHitPointToBall( Vector3 point , Vector3 ballCenter , float radius )
	{
		var vec = ballCenter - point;
		if( vec.x * vec.x + vec.y * vec.y + vec.z * vec.z <= radius * radius )
		{
			return true;
		}
		return false;
	}


	static public bool IsHitBallToBall( Vector3 ballCenterA , float radiusA , Vector3 ballCenterB , float radiusB )
	{
		var vec = ballCenterA - ballCenterB;
		float radius = radiusA + radiusB;
		if( vec.x * vec.x + vec.y * vec.y + vec.z * vec.z <= radius * radius )
		{
			return true;
		}
		return false;
	}
	
	static public bool IsHitPointToBall2D( Vector2 point , Vector2 ballCenter , float radius )
	{
		var vec = ballCenter - point;
		if( vec.x * vec.x + vec.y * vec.y <= radius * radius )
		{
			return true;
		}
		return false;
	}

	static public bool IsHitBallToBall2D( Vector2 ballCenterA , float radiusA , Vector2 ballCenterB , float radiusB )
	{
		var vec = ballCenterA - ballCenterB;
		float radius = radiusA + radiusB;
		if( vec.x * vec.x + vec.y * vec.y <= radius * radius )
		{
			return true;
		}
		return false;
	}
	static public bool IsHitPointToBox( Vector3 point , Vector3 boxCenter , Vector3 boxSize )
	{
		if( (point.x >= boxCenter.x - boxSize.x) && (point.x <= boxCenter.x + boxSize.x) &&
			(point.y >= boxCenter.y - boxSize.y) && (point.y <= boxCenter.y + boxSize.y) &&
			(point.z >= boxCenter.z - boxSize.z) && (point.z <= boxCenter.z + boxSize.z)
		){
			return true;
		}
		return false;
	}


	static public bool IsHitPointToBox2( Vector3 point , Vector3 boxStart , Vector3 boxEnd )
	{
		if( ( point.x >= boxStart.x ) && ( point.x <= boxEnd.x ) &&
			( point.y >= boxStart.y ) && ( point.y <= boxEnd.y ) &&
			( point.z >= boxStart.z ) && ( point.z <= boxEnd.z )
		)
		{
			return true;
		}
		return false;
	}

	static public bool IsHitBoxAToB( Vector3 boxCenterA , Vector3 boxSizeA , Vector3 boxCenterB , Vector3 boxSizeB )
	{
		if( (boxCenterA.x + boxSizeA.x >= boxCenterB.x - boxSizeB.x) && (boxCenterA.x - boxSizeA.x <= boxCenterB.x + boxSizeB.x) &&
			(boxCenterA.y + boxSizeA.y >= boxCenterB.y - boxSizeB.y) && (boxCenterA.y - boxSizeA.y <= boxCenterB.y + boxSizeB.y) &&
			(boxCenterA.z + boxSizeA.z >= boxCenterB.z - boxSizeB.z) && (boxCenterA.z - boxSizeA.z <= boxCenterB.z + boxSizeB.z)
		)
		{
			return true;
		}
		return false;
	}

	/// <summary>
	/// 線分と線分の交点取得
	/// </summary>
	/// <param name="ans"></param>
	/// <param name="lineStartPos_1"></param>
	/// <param name="lineEndPos_1"></param>
	/// <param name="lineStartPos_2"></param>
	/// <param name="lineEndPos_2"></param>
	/// <returns></returns>
	static public bool CalcCrossLinToLine2D(
		out Vector2 ans ,
		Vector2 lineStartPos_1 , Vector2 lineEndPos_1 ,
		Vector2 lineStartPos_2 , Vector2 lineEndPos_2
	)
	{
		const float eps = 0.00001f;
		var vec_1 = lineEndPos_1 - lineStartPos_1;
		var vec_2 = lineEndPos_2 - lineStartPos_2;
		var vec_1STo2S = lineStartPos_2 - lineStartPos_1;
		float cross_v1_v2 = MathUtility.Cross2D( vec_1 , vec_2 );
		if( -eps < cross_v1_v2 && cross_v1_v2 < eps )
		{
			// 平行状態
			ans = new Vector2();
			return false;
		}

		float Crs_v_v1 = MathUtility.Cross2D( vec_1STo2S , vec_1 );
		float Crs_v_v2 = MathUtility.Cross2D( vec_1STo2S , vec_2 );

		float t1 = Crs_v_v2 / cross_v1_v2;
		float t2 = Crs_v_v1 / cross_v1_v2;

		if( t1 + eps < 0 || t1 - eps > 1 || t2 + eps < 0 || t2 - eps > 1 )
		{
			// 交差していない
			ans = new Vector2();
			return false;
		}

//		ans = lineStartPos_1 + vec_1 * t1*0.8f;
		ans = lineStartPos_1 + vec_1 * t1;
//		ans = lineStartPos_1;
		return true;
	}


	/// <summary>
	/// 線分2に対して線分1がぶつかったとき、超えた分を壁ズリさせて取得
	/// </summary>
	/// <param name="ans"></param>
	/// <param name="lineStartPos_1"></param>
	/// <param name="lineEndPos_1"></param>
	/// <param name="lineStartPos_2"></param>
	/// <param name="lineEndPos_2"></param>
	/// <returns></returns>
	static public bool CalcCollisionLinToLine2D(
		out Vector2 ans ,
		Vector2 lineStartPos_1 , Vector2 lineEndPos_1 ,
		Vector2 lineStartPos_2 , Vector2 lineEndPos_2
	)
	{
		const float eps = 0.00001f;
		var vec_1 = lineEndPos_1 - lineStartPos_1;
		var vec_2 = lineEndPos_2 - lineStartPos_2;
		var vec_1STo2S = lineStartPos_2 - lineStartPos_1;
		float cross_v1_v2 = MathUtility.Cross2D( vec_1 , vec_2 );
		if( -eps < cross_v1_v2 && cross_v1_v2 < eps )
		{
			// 平行状態
			ans = new Vector2();
			return false;
		}

		float Crs_v_v1 = MathUtility.Cross2D( vec_1STo2S , vec_1 );
		float Crs_v_v2 = MathUtility.Cross2D( vec_1STo2S , vec_2 );

		float t1 = Crs_v_v2 / cross_v1_v2;
		float t2 = Crs_v_v1 / cross_v1_v2;

		if( t1 + eps < 0 || t1 - eps > 1 || t2 + eps < 0 || t2 - eps > 1 )
		{
			// 交差していない
			ans = new Vector2();
			return false;
		}

		ans = lineStartPos_1 + vec_1 * t1;
		//		ans = lineStartPos_1 + vec_1 * t1*0.8f;
		//		ans = lineStartPos_1;

		var normZ = CalcNormVec( lineStartPos_2 , lineEndPos_2 , lineStartPos_1 );
		var norm = CalcNormVec( lineStartPos_2 , lineEndPos_2 , normZ + new Vector3( lineStartPos_2.x , lineStartPos_2.y , 0 ) );
		var norm2D = new Vector2( norm.x , norm.y ).normalized;

		var wallScratch = (vec_1 - Vector3.Dot( vec_1 , norm2D ) * norm2D).normalized;

		var ansToEnd1 = lineEndPos_1 - ans;
		var ansToEnd1Len = Mathf.Sqrt( ansToEnd1.x * ansToEnd1.x + ansToEnd1.y * ansToEnd1.y );
		//ans += wallScratch * ansToEnd1Len;
		if( Vector3.Dot( vec_1.normalized , vec_2.normalized ) > 0 )
		{
			ans += vec_2.normalized * ansToEnd1Len;
		} else
		{
			ans -= vec_2.normalized * ansToEnd1Len;
		}
		return true;
	}

	/// <summary>
	/// 四角形と点の押し出し処理
	/// 中心から辺に向かって押し出す
	/// </summary>
	/// <param name="ans"></param>
	/// <param name="centerPos"></param>
	/// <param name="vertexList"></param>
	/// <param name="point"></param>
	/// <returns></returns>
	static public bool CalcBoxSidePosByCenterToPointVec( out Vector2 ans , Vector2 centerPos , Vector2 halfSize , Vector2 point )
	{
		var minPos = centerPos - halfSize;
		var maxPos = centerPos + halfSize;

		if( minPos.x <= point.x && point.x <= maxPos.x &&
			minPos.y <= point.y && point.y <= maxPos.y
		)
		{
		} else
		{
			ans = new Vector2();
			return false;
		}


		var near = new Vector2();
		var dif = new Vector2();
//		var difMin = minPos - point;
//		var difMax = maxPos - point;
		var centerToPointVec = point - centerPos;

		if( centerToPointVec.x > 0 )
		{
			near.x = maxPos.x;
		}
		else
		{
			near.x = minPos.x;
		}

		if( centerToPointVec.y > 0 )
		{
			near.y = maxPos.y;
		}
		else
		{
			near.y = minPos.y;
		}


		dif = near - point;
		ans = new Vector2();
		if( dif.x * dif.x >= dif.y * dif.y )
		{
			ans.x = point.x;
			ans.y = near.y;
		}
		else
		{
			ans.x = near.x;
			ans.y = point.y;
		}

		return true;
	}

	/// <summary>
	/// 線分と平面の交点取得
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="vertexList"></param>
	/// <returns></returns>
	static public bool CalcCrossLineToPlane(
		out Vector3 ans ,
		Vector3 lineStartPos , Vector3 lineEndPos ,
		List<Vector3> vertexList
	){
		var normVec = CalcNormVec( vertexList[2] , vertexList[0] , vertexList[1] );
		var chk = CalcCrossLineToPlane( out ans , lineStartPos , lineEndPos , vertexList[0] , normVec );
		if( ! chk )
		{
			return false;
		}
		chk = IsExistPointOnPlane( ans , vertexList );
		return chk;
	}

	/// <summary>
	/// 線分と平面の交点取得
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="onPlanePos">平面上の点</param>
	/// <param name="planeNormVec">平面の法線</param>
	/// <returns></returns>
	static public bool CalcCrossLineToPlane(
		out Vector3 ans,
		Vector3 lineStartPos , Vector3 lineEndPos ,
		Vector3 onPlanePos , Vector3 vecPlaneNorm
	){
		ans = new Vector3();
		if( ! IsCrossLineToPlane( lineStartPos , lineEndPos , onPlanePos , vecPlaneNorm ) )
		{
			return false;
		}

		var range1 = CalcRangeByPointToPlane( lineStartPos , onPlanePos , vecPlaneNorm );
		var range2 = CalcRangeByPointToPlane( lineEndPos , onPlanePos , vecPlaneNorm );

		var d1 = Vector3.Distance( new Vector3() , range1 );
		var d2 = Vector3.Distance( new Vector3() , range2 );

		var a = 0f;
		if( (d1 + d2) != 0 )
		{
			a = d1 / (d1 + d2);
		}


		//onPlanePosを原点とする
		var vecPlaneToStart = lineStartPos - onPlanePos;
		var vecPlaneToEnd = lineEndPos - onPlanePos;

		var tempVec1 = vecPlaneToStart * (1f-a);
		var tempVec2 = vecPlaneToEnd * a;

		var toCrossPoint = tempVec1 + tempVec2;
		ans = onPlanePos + toCrossPoint;

		return true;
	}


	/// <summary>
	/// 法線ベクトル算出
	/// </summary>
	/// <param name="pos1"></param>
	/// <param name="pos2"></param>
	/// <param name="pos3"></param>
	/// <returns></returns>
	static public Vector3 CalcNormVec( Vector3 pos1 , Vector3 pos2 , Vector3 pos3 )
	{
		var vec1 = pos2 - pos1;
		var vec2 = pos3 - pos1;
		return Vector3.Cross( vec1 , vec2 );
	}

	/// <summary>
	/// 無限に広がる平面と線分とのあたり判定
	/// </summary>
	/// <param name="lineStartPos"></param>
	/// <param name="lineEndPos"></param>
	/// <param name="onPlanePos">平面上の点</param>
	/// <param name="planeNormVec">平面の法線</param>
	/// <returns></returns>
	static public bool IsCrossLineToPlane(
		Vector3 lineStartPos , Vector3 lineEndPos , Vector3 onPlanePos ,Vector3 vecPlaneNorm )
	{
		//onPlanePosを原点とする
		var vecPlaneToStart = lineStartPos - onPlanePos;
		var vecPlaneToEnd = lineEndPos - onPlanePos;

		//同じ方向にないことをチェック
		var dot1 = Vector3.Dot( vecPlaneToStart , vecPlaneNorm );
		var dot2 = Vector3.Dot( vecPlaneToEnd , vecPlaneNorm );

		if( dot1 == dot2 )
		{
			return false;
		}

		bool chk = dot1 * dot2 <= 0;

		return chk;
	}

	/// <summary>
	/// 点と平面の距離算出
	/// </summary>
	/// <param name="pointPos"></param>
	/// <param name="onPlanePos"></param>
	/// <param name="vecPlaneNorm"></param>
	/// <returns></returns>
	static public Vector3 CalcRangeByPointToPlane( Vector3 pointPos , Vector3 onPlanePos , Vector3 vecPlaneNorm )
	{
		//onPlanePosを原点とする
		var vecPlaneToPoint = pointPos - onPlanePos;

		var dot = Vector3.Dot( vecPlaneNorm , vecPlaneToPoint );

		var dotAbs = Mathf.Abs( dot );
		var retX = 0f;
		var retY = 0f;
		var retZ = 0f;
		if( vecPlaneNorm.x != 0 )
		{
			retX = dotAbs / Mathf.Abs( vecPlaneNorm.x );
		}
		if( vecPlaneNorm.y != 0 )
		{
			retY = dotAbs / Mathf.Abs( vecPlaneNorm.y );
		}
		if( vecPlaneNorm.z != 0 )
		{
			retZ = dotAbs / Mathf.Abs( vecPlaneNorm.z );
		}

		var ret = new Vector3( retX , retY , retZ );

		return ret;
	}

	/// <summary>
	/// 点が平面上に存在するかチェック
	/// </summary>
	/// <param name="pointPos"></param>
	/// <param name="vertexList">平面の頂点。右回りか左回りか統一する</param>
	/// <returns></returns>
	static public bool IsExistPointOnPlane( Vector3 pointPos , List<Vector3> vertexList )
	{
		var vertex1 = vertexList[0];
		var vertex2 = vertexList[1];

		var normResult = CalcNormVec( vertex2 , vertex1 , pointPos );

		for(int i = 1 ; i < vertexList.Count ; i++ )
		{
			vertex1 = vertexList[i];
			if( i + 1 >= vertexList.Count )
			{
				vertex2 = vertexList[0];
			}
			else
			{
				vertex2 = vertexList[i+1];
			}

			var tmpNormResult = CalcNormVec( vertex2 , vertex1 , pointPos );
			//法線の向きが違うとき、点は平面の外にある
			float resX = normResult.x * tmpNormResult.x;
			float resY = normResult.y * tmpNormResult.y;
			float resZ = normResult.z * tmpNormResult.z;

			float resXAbs = Mathf.Abs( resX );
			float resYAbs = Mathf.Abs( resY );
			float resZAbs = Mathf.Abs( resZ );
			float chk = 0f;
			if( resXAbs >= resYAbs && resXAbs >= resZAbs )
			{
				chk = resX;
			} else if( resYAbs >= resZAbs )
			{
				chk = resY;
			} else
			{
				chk = resZ;
			}

			if( chk < 0 )
			{
				return false;
			}

		}

		return true;
	}

	/// <summary>
	/// 線分と平面の枠の交点取得
	/// </summary>
	/// <param name="ans"></param>
	/// <param name="lineStartPos_1"></param>
	/// <param name="lineEndPos_1"></param>
	/// <param name="vertexList"></param>
	/// <returns></returns>
	static public bool CalcCrossLineToPolygonFrame2D(
		out Vector2 ans,
		Vector2 lineStartPos_1 , Vector2 lineEndPos_1 ,
		List<Vector2> vertexList
	){
		for( int i = 0 ; i < vertexList.Count-1 ; i++ )
		{
			if( CalcCrossLinToLine2D( out ans , lineStartPos_1 , lineEndPos_1 , vertexList[i] , vertexList[i + 1] ) )
			{
				return true;
			}
		}

		if( CalcCrossLinToLine2D( out ans , lineStartPos_1 , lineEndPos_1 , vertexList[vertexList.Count - 1] , vertexList[0] ) )
		{
			return true;
		}

		ans = new Vector3();
		return false;
	}

	/// <summary>
	/// 線分と平面の枠の交点をもとに壁ズリ後の結果取得
	/// </summary>
	/// <param name="ans"></param>
	/// <param name="lineStartPos_1"></param>
	/// <param name="lineEndPos_1"></param>
	/// <param name="vertexList"></param>
	/// <returns></returns>
	static public bool CalcCollisionLineToPolygonFrame2D(
		out Vector2 ans ,
		Vector2 lineStartPos_1 , Vector2 lineEndPos_1 ,
		List<Vector2> vertexList
	)
	{
		for( int i = 0 ; i < vertexList.Count ; i++ )
		{
			var nextIndex = (i + 1) % vertexList.Count;
			Vector2 resultCollision;
			if( CalcCollisionLinToLine2D( out resultCollision , lineStartPos_1 , lineEndPos_1 , vertexList[i] , vertexList[nextIndex] ) )
			{
				/*
				var next2Index = (i + 2) % vertexList.Count;
				if( CalcCrossLinToLine2D(
					out ans ,
					lineStartPos_1 ,
					resultCollision ,
					vertexList[nextIndex] ,
					vertexList[next2Index] )
				)
				{
					return true;
				} else
				{
					var backIndex = (i - 1 + vertexList.Count) % vertexList.Count;
					if( CalcCrossLinToLine2D(
						out ans ,
						lineStartPos_1 ,
						resultCollision ,
						vertexList[backIndex] ,
						vertexList[i] )
					)
					{
						return true;
					}
				}
				*/
				ans = resultCollision;
				return true;
			}
		}

		ans = new Vector3();
		return false;
	}
}
