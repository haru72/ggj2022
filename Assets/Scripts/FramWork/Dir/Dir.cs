using UnityEngine;

static public class DirUtility
{
	public enum eDir
	{
		None,
		Up,
		Down,
		Left,
		Right,
	}

	static public eDir Inverse( eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				return eDir.Down;
			case eDir.Down:
				return eDir.Up;
			case eDir.Left:
				return eDir.Right;
			case eDir.Right:
				return eDir.Left;
		}

		return eDir.None;
	}

	static public eDir ToRightDir( eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				return eDir.Right;
			case eDir.Down:
				return eDir.Left;
			case eDir.Left:
				return eDir.Up;
			case eDir.Right:
				return eDir.Down;
		}

		return eDir.None;
	}

	static public eDir ToLeftDir( eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				return eDir.Left;
			case eDir.Down:
				return eDir.Right;
			case eDir.Left:
				return eDir.Down;
			case eDir.Right:
				return eDir.Up;
		}

		return eDir.None;
	}

	static public Vector3 ToMoveVec( eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				return new Vector3( 0 , 0 , 1 );
			case eDir.Down:
				return new Vector3( 0 , 0 , -1 );
			case eDir.Left:
				return new Vector3( -1 , 0 , 0 );
			case eDir.Right:
				return new Vector3( 1 , 0 , 0 );
		}
		return new Vector3();
	}

	static public eDir VecToDir( Vector3 vec )
	{
		var lenX = vec.x * vec.x;
		var lenZ = vec.z * vec.z;
		if( lenX > lenZ )
		{
			if( vec.x > 0 )
			{
				return eDir.Right;
			} else
			{
				return eDir.Left;
			}
		}

		if( vec.z > 0 )
		{
			return eDir.Up;
		} else
		{
			return eDir.Down;
		}

	}

	static public bool IsSameDir( Vector3 vec , eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				if( vec.z > 0 )
				{
					return true;
				}
				return false;
			case eDir.Down:
				if( vec.z < 0 )
				{
					return true;
				}
				return false;
			case eDir.Left:
				if( vec.x < 0 )
				{
					return true;
				}
				return false;
			case eDir.Right:
				if( vec.x > 0 )
				{
					return true;
				}
				return false;
		}

		return false;
	}


	/// <summary>
	/// Dirの方向に顔を向けたいときの角度取得
	/// </summary>
	/// <param name="dir"></param>
	/// <returns></returns>
	static public int DirToAngle( eDir dir )
	{
		switch( dir )
		{
			case eDir.Up:
				return 0;
			case eDir.Down:
				return 180;
			case eDir.Left:
				return 270;
			case eDir.Right:
				return 90;
		}

		return 0;
	}

	static public eDir AngleToDir( float angle )
	{
		if( angle < 0 )
		{
			angle += 360;
		}

		if( 45 <= angle && angle < 135 )
		{
			return eDir.Right;
		} else if( 135 <= angle && angle < 225 )
		{
			return eDir.Down;
		} else if( 225 <= angle && angle < 315 )
		{
			return eDir.Left;
		} else
		{
			return eDir.Up;
		}
	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="dir"></param>
	/// <param name="oldDir"></param>
	/// <param name="timer"> 0~1 </param>
	/// <returns>角度</returns>
	static public float RotateYToDir( DirUtility.eDir dir , DirUtility.eDir oldDir , float timer )
	{
		return Quaternion.Lerp(
			Quaternion.Euler( 0 , DirUtility.DirToAngle( oldDir ) , 0 ) ,
			Quaternion.Euler( 0 , DirUtility.DirToAngle( dir ) , 0 ) ,
			timer
		).eulerAngles.y;
	}

}