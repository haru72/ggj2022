using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public enum GimicType
	{
		Curse,
		Candlestick,
	}
	public class MasuGimicBehaviour : MonoBehaviour
	{
		[SerializeField]
		GimicType _gimicType = GimicType.Curse;

		public GimicType GimicType => _gimicType;

	}
}