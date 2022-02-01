using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MyParticle
{
	public class ParticleController : Singleton<ParticleController>
	{
		public enum ParticleId
		{
			Purify,
		}

		GameObject _gameObject;
		Dictionary<ParticleId , ParticleGroup> _particleGroupDic = new Dictionary<ParticleId , ParticleGroup>();

		protected override void InitSub()
		{
			base.InitSub();
			_gameObject = new GameObject( "ParticleController" );
		}

		public void GenerateParticleGroup( GenerateData generateData )
		{
			var particleGroup = new ParticleGroup();
			particleGroup.Init( generateData._particleGroupInitData , _gameObject );
			_particleGroupDic.Add( generateData._groupId , particleGroup );
		}

		/// <summary>
		/// アクティブにできるパーティクルがあるか
		/// </summary>
		/// <param name="groupId"></param>
		/// <returns></returns>
		public bool CanActiveParticle( ParticleId groupId )
		{
			if( ! _particleGroupDic.ContainsKey( groupId ) )
			{
				return false;
			}
			return _particleGroupDic[groupId].CanActiveParticle();
		}

		/// <summary>
		/// アクティブにできるパーティクルがあれば、アクティブにする
		/// </summary>
		/// <param name="groupId"></param>
		/// <param name="pos"></param>
		public void ActiveParticle( ParticleId groupId , Vector3 pos , float angle )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[groupId].ActiveParticle( pos , angle );
		}
		public void ActiveParticle( ParticleId groupId , Vector3 pos , float angle , Gradient gradient )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ActiveParticle( pos , angle , gradient );
		}

		public void ActiveParticle( ParticleId groupId , Vector3 pos , Vector3 angle )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ActiveParticle( pos , angle );
		}

		public void ActiveParticle( ParticleId groupId , Vector3 pos , Vector3 angle , Gradient gradient )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ActiveParticle( pos , angle , gradient );
		}

		/// <summary>
		/// アクティブにできるパーティクルがない場合、一番古いパーティクルを再起動する
		/// </summary>
		/// <param name="groupId"></param>
		/// <param name="pos"></param>
		public void ForceActiveParticle( ParticleId groupId , Vector3 pos , float angle )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[groupId].ForceActiveParticle( pos , angle );
		}
		public void ForceActiveParticle( ParticleId groupId , Vector3 pos , float angle , Gradient gradient )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ForceActiveParticle( pos , angle , gradient );
		}

		public void ForceActiveParticle( ParticleId groupId , Vector3 pos , Vector3 angle )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ForceActiveParticle( pos , angle );
		}

		public void ForceActiveParticle( ParticleId groupId , Vector3 pos , Vector3 angle , Gradient gradient )
		{
			if( !_particleGroupDic.ContainsKey( groupId ) )
			{
				return;
			}
			_particleGroupDic[ groupId ].ForceActiveParticle( pos , angle , gradient );
		}

		public void Update()
		{
			foreach( var particleGroup in _particleGroupDic )
			{
				particleGroup.Value.Update();
			}
		}
	}
}
