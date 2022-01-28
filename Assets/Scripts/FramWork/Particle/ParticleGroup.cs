using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
namespace MyParticle
{
	class ParticleGroup
	{
		List<Particle> _particleList = new List<Particle>();
		List<Particle> _activeParticleList = new List<Particle>();
		List<Particle> _enactiveParticleList = new List<Particle>();

		public void Init( GenerateData.ParticleGroupInitData initData , GameObject parentObj )
		{
			for( int i = 0 ; i < initData._maxNum ; i++ )
			{
				var particle = new Particle();
				var obj = GameObject.Instantiate<GameObject>( initData._particleBaseObj );
				obj.transform.SetParent( parentObj.transform , false );
				particle.Init( obj , initData._soundType , initData._soundTime );
				_particleList.Add( particle );
				_enactiveParticleList.Add( particle );
			}
		}

		/// <summary>
		/// パーティクルをアクティブにできるか
		/// </summary>
		/// <returns></returns>
		public bool CanActiveParticle()
		{
			return _enactiveParticleList.Count > 0;
		}

		/// <summary>
		/// アクティブにできるパーティクルがあるなら、アクティブにする
		/// </summary>
		/// <param name="pos"></param>
		public void ActiveParticle( Vector3 pos , float angle )
		{
			ActiveParticle( pos , new Vector3( 0 , angle , 0 ) );
		}

		public void ActiveParticle( Vector3 pos , Vector3 angle )
		{
			if( !CanActiveParticle() )
			{
				return;
			}
			var particle = _enactiveParticleList[ 0 ];
			particle.Setup( pos , angle );
			_activeParticleList.Add( particle );
			_enactiveParticleList.Remove( particle );
		}

		public void ActiveParticle( Vector3 pos , float angle , Gradient gradient )
		{
			ActiveParticle( pos , new Vector3( 0 , angle , 0 ) , gradient );
		}

		public void ActiveParticle( Vector3 pos , Vector3 angle , Gradient gradient )
		{
			if( !CanActiveParticle() )
			{
				return;
			}
			var particle = _enactiveParticleList[ 0 ];
			particle.Setup( pos , angle , gradient );
			_activeParticleList.Add( particle );
			_enactiveParticleList.Remove( particle );
		}


		/// <summary>
		/// アクティブにできるパーティクルがない場合、一番古いパーティクルを再起動する
		/// </summary>
		/// <param name="pos"></param>
		public void ForceActiveParticle( Vector3 pos , float angle )
		{
			ForceActiveParticle( pos , new Vector3( 0 , angle , 0 ) );
		}

		public void ForceActiveParticle( Vector3 pos , Vector3 angle )
		{
			if( _enactiveParticleList.Count > 0 )
			{
				var particle = _enactiveParticleList[ 0 ];
				particle.Setup( pos , angle );
				_activeParticleList.Add( particle );
				_enactiveParticleList.Remove( particle );
			} else
			{
				var particle = _activeParticleList[ 0 ];
				particle.Inactive();
				particle.Setup( pos , angle );
				_activeParticleList.Remove( particle );
				_activeParticleList.Add( particle );
			}
		}

		public void ForceActiveParticle( Vector3 pos , float angle , Gradient gradient )
		{
			ForceActiveParticle( pos , new Vector3( 0 , angle , 0 ) , gradient );
		}

		public void ForceActiveParticle( Vector3 pos , Vector3 angle , Gradient gradient )
		{
			if( _enactiveParticleList.Count > 0 )
			{
				var particle = _enactiveParticleList[ 0 ];
				particle.Setup( pos , angle , gradient );
				_activeParticleList.Add( particle );
				_enactiveParticleList.Remove( particle );
			}
			else
			{
				var particle = _activeParticleList[ 0 ];
				particle.Inactive();
				particle.Setup( pos , angle , gradient );
				_activeParticleList.Remove( particle );
				_activeParticleList.Add( particle );
			}
		}


		public void Update()
		{
			var removeList = new List<Particle>();
			foreach( var particle in _activeParticleList )
			{
				particle.Update();
				if( ! particle.IsActive() )
				{
					//パーティクルが非アクティブになったら、登録するリストを変更
					removeList.Add( particle );
					_enactiveParticleList.Add( particle );
				}
			}

			foreach( var remove in removeList )
			{
				_activeParticleList.Remove( remove );
			}
		}

		public void Enactive()
		{
			foreach( var particle in _activeParticleList )
			{
				_enactiveParticleList.Add( particle );
			}
			_activeParticleList.Clear();
		}

		public void Destroy()
		{
			foreach( var particle in _particleList )
			{
				particle.Destroy();
			}

			_particleList.Clear();
			_activeParticleList.Clear();
			_enactiveParticleList.Clear();
		}

	}
}
