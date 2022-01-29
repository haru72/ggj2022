using System;
using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Candle : MasuGimic
	{
		const byte MaxByteForOverexposedColor = 191;
		Material _material;
		Color _baseEmissionColor;
		float _onIntensity;
		const float LightingSetupEndTime = 1.5f;
		float _timer = 0;
		Action _updateSub;

		public MasuGimic_Candle( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
			_material = MasuGimicBehaviour.GetComponentInChildren<MeshRenderer>().material;
			//_baseEmissionColor = _material.GetColor( "EmissionColor" );

		}

		public override void Update()
		{
		}


		public override void Action()
		{
		}

		public override bool CanTouch()
		{
			return true;
		}

	}
}