using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MyAudioController : Singleton<MyAudioController>
{
	public enum BGMType
	{
		None,
		Title,
		Game,
		Clear,
		EnumEnd,
	}
	public enum SoundType
	{
		None,
		Button_Default,
		GetKey,
		LightFire,
		Purify,
		Walk,

		EnumEnd,
	}
	class DelaySE
	{
		public int _frame = 0;
		public int _frameEnd;
		public SoundType _soundType;
	}
	readonly Dictionary<BGMType , string> _bgmStrDic = new Dictionary<BGMType , string>() {
		{ BGMType.Title ,"Sounds/TitleBGM"},
		{ BGMType.Game ,"Sounds/PlayingBGM"},
		{ BGMType.Clear ,"Sounds/ClearBGM"},
	};

	readonly Dictionary<SoundType , string> _seStrDic = new Dictionary<SoundType , string>() {
		{ SoundType.Button_Default ,"Sounds/GetKey"},
		{ SoundType.GetKey,"Sounds/GetKey"},
		{ SoundType.LightFire,"Sounds/LightFire"},
		{ SoundType.Purify,"Sounds/PurifyCurse"},
		{ SoundType.Walk,"Sounds/Walk"},
	};

	const float BGMVolumeBase = 0.2f;
	const float SEVolumeBase = 0.3f;

	List<BGMType> _loadTargetBGMList = new List<BGMType>();
	List<SoundType> _loadTargetSEList = new List<SoundType>();
	AudioListener _audioListener;
	AudioSource _audioSourceBGM;
	AudioSource _audioSourceSE;
	Dictionary<BGMType , AudioClip> _bgmAudioClipDic = new Dictionary<BGMType , AudioClip>();
	Dictionary<SoundType , AudioClip> _seAudioClipDic = new Dictionary<SoundType , AudioClip>();

	List<DelaySE> _delaySEList = new List<DelaySE>();

	bool _enable = false;

	protected override bool IsAddManager()
	{
		return false;
	}

	protected override void InitSub()
	{
		base.InitSub();

		var obj = new GameObject( "MyAudioController" );
		GameObject.DontDestroyOnLoad( obj );
		_audioListener = obj.AddComponent<AudioListener>();
		_audioSourceBGM = obj.AddComponent<AudioSource>();
		_audioSourceBGM.loop = true;
		_audioSourceBGM.playOnAwake = false;
		_audioSourceBGM.volume = BGMVolumeBase;
		_audioSourceSE = obj.AddComponent<AudioSource>();
		_audioSourceSE.loop = false;
		_audioSourceSE.playOnAwake = false;
		_audioSourceSE.volume = SEVolumeBase;
	}

	public void SetBGMVolume( float volume )
	{
		_audioSourceBGM.volume = BGMVolumeBase * volume;
	}
	public void SetSEVolume( float volume )
	{
		_audioSourceSE.volume = SEVolumeBase * volume;
	}

	public void AddLoadTarget_All()
	{
		int cnt = (int)BGMType.EnumEnd;
		for( int i = 0 ; i < cnt ; i++ )
		{
			_loadTargetBGMList.Add( (BGMType)i );
		}

		cnt = (int)SoundType.EnumEnd;
		for( int i = 0 ; i < cnt ; i++ )
		{
			_loadTargetSEList.Add( (SoundType)i );
		}
	}

	public void AddLoadTarget_BGM( BGMType bgmType )
	{
		_loadTargetBGMList.Add( bgmType );
	}

	public void AddLoadTarget_SE( SoundType soundType )
	{
		_loadTargetSEList.Add( soundType );
	}

	public IEnumerator LoadAsync()
	{
		if( !_loadTargetSEList.Contains( SoundType.Button_Default ) )
		{
			_loadTargetSEList.Add( SoundType.Button_Default );
		}

		var resourceRequestDic_BGM = new Dictionary<BGMType , ResourceRequest>();
		var resourceRequestDic_SE = new Dictionary<SoundType , ResourceRequest>();

		foreach( var loadTarget in _loadTargetBGMList )
		{
			if( _bgmStrDic.ContainsKey( loadTarget ) )
			{
				resourceRequestDic_BGM.Add( loadTarget , Resources.LoadAsync<AudioClip>( _bgmStrDic[ loadTarget ] ) );
			}
		}


		foreach( var loadTarget in _loadTargetSEList )
		{
			if( _seAudioClipDic.ContainsKey( loadTarget ) )
			{
				continue;
			}
			if( !_seStrDic.ContainsKey( loadTarget ) )
			{
				continue;
			}

			resourceRequestDic_SE.Add( loadTarget , Resources.LoadAsync<AudioClip>( _seStrDic[ loadTarget ] ) );
		}

		bool isLoadResources = true;
		do
		{
			isLoadResources = false;
			foreach( var keyValue in resourceRequestDic_SE )
			{
				var resourceRequest = keyValue.Value;
				if( !resourceRequest.isDone )
				{
					isLoadResources = true;
					break;
				}
			}
			yield return null;
		} while( isLoadResources );


		isLoadResources = false;
		do
		{
			isLoadResources = false;
			foreach( var keyValue in resourceRequestDic_BGM )
			{
				var resourceRequest = keyValue.Value;
				if( !resourceRequest.isDone )
				{
					isLoadResources = true;
					break;
				}
			}
			yield return null;
		} while( isLoadResources );


		foreach( var loadTarget in _loadTargetBGMList )
		{
			if( _bgmAudioClipDic.ContainsKey( loadTarget ) )
			{
				continue;
			}
			if( !_bgmStrDic.ContainsKey( loadTarget ) )
			{
				continue;
			}

			var audioClip = GameObject.Instantiate<AudioClip>(
				resourceRequestDic_BGM[ loadTarget ].asset as AudioClip
			);
			_bgmAudioClipDic.Add( loadTarget , audioClip );
		}


		foreach( var loadTarget in _loadTargetSEList )
		{
			if( _seAudioClipDic.ContainsKey( loadTarget ) )
			{
				continue;
			}
			if( !_seStrDic.ContainsKey( loadTarget ) )
			{
				continue;
			}

			var audioClip = GameObject.Instantiate<AudioClip>(
				resourceRequestDic_SE[ loadTarget ].asset as AudioClip
			);
			_seAudioClipDic.Add( loadTarget , audioClip );
		}

		_loadTargetBGMList.Clear();
		_loadTargetSEList.Clear();
		_enable = true;
	}

	public void ClearSEList()
	{
		_seAudioClipDic.Clear();
		_enable = false;
	}

	public void PlayBGM( BGMType bgmType )
	{
		if( !_enable )
		{
			return;
		}

		if( !_bgmAudioClipDic.ContainsKey( bgmType ) )
		{
			return;
		}

		_audioSourceBGM.clip = _bgmAudioClipDic[ bgmType ];
		_audioSourceBGM.Play();
	}

	public void PlaySE( SoundType soundType )
	{
		if( !_enable )
		{
			return;
		}

		if( !_seAudioClipDic.ContainsKey( soundType ) )
		{
			return;
		}
		_audioSourceSE.PlayOneShot( _seAudioClipDic[ soundType ] );
	}

	public void DelayPlaySE( SoundType soundType , int frame )
	{
		_delaySEList.Add( new DelaySE()
		{
			_soundType = soundType ,
			_frameEnd = frame
		} );
	}

	public void Update()
	{
		List<DelaySE> removeList = new List<DelaySE>();
		foreach( var delaySE in _delaySEList )
		{
			delaySE._frame++;
			if( delaySE._frame < delaySE._frameEnd )
			{
				continue;
			}

			PlaySE( delaySE._soundType );
			removeList.Add( delaySE );
		}

		foreach( var remove in removeList )
		{
			_delaySEList.Remove( remove );
		}
	}

}