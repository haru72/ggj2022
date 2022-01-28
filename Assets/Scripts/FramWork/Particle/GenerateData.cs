using UnityEngine;

namespace MyParticle
{
	public class GenerateData
	{
		public class ParticleGroupInitData
		{
			public GameObject _particleBaseObj;
			public int _maxNum;
			public MyAudioController.SoundType _soundType = MyAudioController.SoundType.None;
			public float _soundTime = 0;
		}

		public ParticleController.ParticleId _groupId;
		public ParticleGroupInitData _particleGroupInitData = new ParticleGroupInitData();

	}
}
