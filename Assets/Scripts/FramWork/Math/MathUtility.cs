using UnityEngine;

public class MathUtility
{
	/// <summary>
	/// AとBの距離取得
	/// </summary>
	/// <param name="posA"></param>
	/// <param name="posB"></param>
	/// <returns></returns>
	static public float CalcRangeAtoB( Vector3 posA , Vector3 posB )
	{
		Vector3 v = posB - posA;
		return v.magnitude;
	}

	/// <summary>
	/// Aの向いている方向にBがあるかチェック向き
	/// </summary>
	/// <param name="posA"></param>
	/// <param name="dirA">Aの向き(normalized済み)</param>
	/// <param name="range">向きの範囲</param>
	/// <param name="posB"></param>
	/// <returns></returns>
	static public bool IsMatchDirection( Vector3 posA , Vector3 dirA , float range , Vector3 posB )
	{
		var dirB = (posB - posA).normalized;
		var chkDir = dirA - dirB;
		if(
			-range <= chkDir.x && chkDir.x <= range &&
			-range <= chkDir.y && chkDir.y <= range &&
			- range <= chkDir.z && chkDir.z <= range
		)
		{
			return true;
		}

		return false;
	}

	//2D外積
	static public float Cross2D( Vector2 vecA , Vector2 vecB )
	{
		return vecA.x * vecB.y - vecA.y * vecB.x;
	}



	/// <summary>
	/// 表裏チェック
	/// v1とv2が同じ面にあるか
	/// </summary>
	/// <param name="vBase"></param>
	/// <param name="v1"></param>
	/// <param name="v2"></param>
	/// <returns></returns>
	static public bool IsMatchFrontOrBaack( Vector2 vBase , Vector2 v1 , Vector2 v2 )
	{
		var cross = Cross2D( vBase , v1 ) * Cross2D( vBase , v2 );
		return cross > 0;
	}


	public class HermiteCurve
	{
        public class Param
        {
            public Vector3 _pos;
            public Vector3 _vec;
        }

        /// <summary>
        /// 指定の条件でのポイントの取得。
        /// </summary>
        static Vector3 CubicHermiteCurve( Vector3 p0 , Vector3 v0 , Vector3 p1 , Vector3 v1 , float t )
        {
            float t2 = t * t;
            float t3 = t * t * t;

            return ( 2f * p0 - 2f * p1 + v0 + v1 ) * t3 + ( -3f * p0 + 3f * p1 - 2f * v0 - v1 ) * t2 + v0 * t + p0;
        }

        Param _start;
        Param _end;

        /// <summary>
        /// 点とベクトルの設定。
        /// </summary>
        public void Setup( Param start , Param end )
        {
            _start = start;
            _end = end;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="timer">0~1</param>
		/// <returns></returns>
        public Vector3 CalcPos( float timer )
        {
            return HermiteCurve.CubicHermiteCurve(
					_start ._pos,
					_start ._vec,
					_end._pos ,
					_end._vec ,
					timer
			);
        }
    }


	public class Horming
	{
		Vector3 _position;
		Vector3 _velocity;
		float _maxCentripetalAccel;	//
		float _damping;
		float _propulsion;  //推進力
		float _accel = 0;
		float _accellAdd = 0;

		public void Setup(
			Vector3 position ,
			float speed ,
			Vector3 velocity ,	//speed + 方向
			float curvatureRadius ,	//値が小さいほど、鋭角に曲がる
			float damping,	//空気抵抗
			float accellAdd
		)
		{
			_position = position;
			_velocity = velocity;
			// 速さv、半径rで円を描く時、その向心力はv^2/r。これを計算しておく。
			_maxCentripetalAccel = speed * speed / curvatureRadius;
			_damping = damping;
			// 終端速度がspeedになるaccelを求める
			// v = a / kだからa=v*k
			_propulsion = speed * damping;
			_accellAdd = accellAdd;
			_accel = 1;
		}

		public void Update( float deltaTime , Vector3 target )
		{
			var toTarget = target - _position;
			var vn = _velocity.normalized;
			var dot = Vector3.Dot( toTarget , vn );
			var centripetalAccel = toTarget - ( vn * dot );
			var centripetalAccelMagnitude = centripetalAccel.magnitude;
			if( centripetalAccelMagnitude > 1f )
			{
				centripetalAccel /= centripetalAccelMagnitude;
			}
			var force = centripetalAccel * _maxCentripetalAccel;
			force += vn * _propulsion;
			force -= _velocity * _damping;
			_velocity += force * deltaTime;
			_position += _velocity * deltaTime;

			_accel += _accellAdd;
		}

		public Vector3 GetPos()
		{
			return _position;
		}

	}
}
