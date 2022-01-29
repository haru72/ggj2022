using System;
using System.Collections;
using UnityEngine;

namespace GameMainSpace.MasuGimicSpace
{
	public class MasuGimic_Curse : MasuGimic
	{
		Material _material;
		bool _isDelete = false;
		const float DeleteEndTime = 1.5f;
		float _timer = 0;
		Action _updateSub;

		public MasuGimic_Curse( MasuGimicBehaviour masuGimicBehaviour ) :base( masuGimicBehaviour )
		{
			_material = MasuGimicBehaviour.GetComponentInChildren<MeshRenderer>().material;
		}

		public override void Update()
		{
			_updateSub?.Invoke();
		}

		void UpdateDelete()
		{
			_timer += Time.deltaTime;
			if( _timer >= DeleteEndTime )
			{
				_timer = DeleteEndTime;
				_updateSub = null;
			}
			_material.SetFloat( "Rate" , _timer / DeleteEndTime * 50 );
		}

		public override void Action()
		{
			_isDelete = true;
			_updateSub = UpdateDelete;
		}

		public override bool CanTouch()
		{
			return !_isDelete;
		}

	}
}