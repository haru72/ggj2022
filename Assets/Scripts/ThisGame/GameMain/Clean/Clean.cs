using System.Collections;
using UnityEngine;

namespace GameMainSpace.CleanSpace
{
	public class Clean
	{
		GameObject GameObject { get; }
		float _timer = 0;
		const float EndTime = 0.3f;
		const float UpdateEffectEndTime_1 = 0.15f;
		const float UpdateEffectEndTime_2 = 0.25f;

		bool IsActive => GameObject.activeSelf;

		System.Action _updateEffectAction;

		MeshRenderer MeshRenderer { get; }

		public Clean( GameObject gameObject )
		{
			GameObject = gameObject;
			MeshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
		}

		public void Setup( Vector3 pos ) 
		{
			GameObject.SetActive( true );
			GameObject.transform.position = pos + Vector3.zero;

			_updateEffectAction = UpdateEffect_1;
			_timer = 0;
		}

		public void Update()
		{
			if( ! IsActive )
			{
				return;
			}
			_timer += Time.deltaTime;
			if( _timer >= EndTime )
			{
				_timer = EndTime;
				GameObject.SetActive( false );
			}

			_updateEffectAction?.Invoke();
		}

		void UpdateEffect_1()
		{
			MeshRenderer.material.SetFloat( "Alpha" , _timer / UpdateEffectEndTime_1 );
			if( _timer >= UpdateEffectEndTime_1 )
			{
				_updateEffectAction = UpdateEffect_2;
			}
		}
		void UpdateEffect_2()
		{
			if( _timer >= UpdateEffectEndTime_2 )
			{
				_updateEffectAction = UpdateEffect_3;
				MyParticle.ParticleController.GetInstance().ForceActiveParticle(
					MyParticle.ParticleController.ParticleId.Purify ,
					GameObject.transform.position + new Vector3( 0 , 0.4f , 0 ) ,
					0
				);
			}
		}
		void UpdateEffect_3()
		{
			MeshRenderer.material.SetFloat( "Alpha" , 1f - ( (_timer - UpdateEffectEndTime_2) / EndTime ) );
			_updateEffectAction = null;
		}


	}
}