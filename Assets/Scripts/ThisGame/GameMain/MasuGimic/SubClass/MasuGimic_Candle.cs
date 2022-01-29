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
		bool _canTouch = true;
		Action _updateSub;
		GameObject LightObj { get; }

		public MasuGimic_Candle( MasuGimicBehaviour masuGimicBehaviour ) : base( masuGimicBehaviour )
		{
			LightObj = MasuGimicBehaviour.transform.Find( "Light" ).gameObject;
			LightObj.SetActive( false );
			//_material = LightObj.GetComponent<MeshRenderer>().material;
			//_baseEmissionColor = _material.GetColor( "EmissionColor" );

		}

		public override void Update()
		{
		}


		public override void Action()
		{
			LightObj.SetActive( true );

			_canTouch = false;
		}

		public override bool CanTouch()
		{
			return _canTouch;
		}

		public Vector3 Pos => MasuGimicBehaviour.transform.position;

		public void SetActiveLight( bool active )
		{
			LightObj.SetActive( active & (!_canTouch) );
		}

	}
}