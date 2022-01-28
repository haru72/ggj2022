using UnityEngine;
using System;

namespace MyParticle
{
	class Particle
	{
		GameObject _gameObject;
		ParticleSystem _particleSystem;
		
		MyAudioController.SoundType _soundType;
		float _soundTimer = 0;
		float _soundTime;
		Action _soundAction;

		bool _isActive = false;

		public void Init(
			GameObject gameObject ,
			MyAudioController.SoundType soundType,
			float soundTime
		){
			_gameObject = gameObject;

			_soundType = soundType;
			_soundTime = soundTime;

			_particleSystem = _gameObject.GetComponent<ParticleSystem>();
			Inactive();
		}

		public void Setup( Vector3 pos , Vector3 angle )
		{
			Setup( pos , angle , null );

		}

		public void Setup( Vector3 pos , Vector3 angle , Gradient gradient )
		{
			_gameObject.transform.rotation = Quaternion.Euler( angle.x , angle.y , angle.z );
			_gameObject.transform.position = pos;
			_gameObject.SetActive( true );
			_isActive = true;

			if( gradient != null )
			{
				var mainModule = _particleSystem.main;
				mainModule.startColor = gradient;
			}

			_particleSystem.Play();

			_soundTimer = 0;
			_soundAction = ()=> {
				if( _soundTimer >= _soundTime )
				{
					MyAudioController.GetInstance().PlaySE( _soundType );
					_soundAction = null;
				}
			};
		}

		public bool IsActive()
		{
			return _isActive;
		}

		public void Update()
		{
			if( ! _isActive )
			{
				return;
			}

			_soundTimer += Time.deltaTime;
			_soundAction?.Invoke();

			if( ! _particleSystem.isPlaying )
			{
				Debug.Log( "particle _soundTimer:" + _soundTimer + " _soundTime:" + _soundTime );
				Inactive();
			}

		}

		public void Inactive()
		{
			_gameObject.SetActive( false );
			_isActive = false;
			_particleSystem.Stop();
		}

		public void Destroy()
		{
			GameObject.Destroy( _gameObject );
		}

	}
}
