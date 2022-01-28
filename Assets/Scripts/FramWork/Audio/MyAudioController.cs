using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MyAudioController : Singleton<MyAudioController>
{
	public enum SoundType
	{
		None,
		TitleBGM,
		HomeBGM,
		BattleBGM,
		PutCard,
		MoveChara,
		Confetti,
		Hit,
		SlideIn,
		SlideOut,
		EquipLevelUp,
		PlayerLevelUp,
		Gimic_Warp,
		Gimic_PowerUp,
		Gimic_Coin,
		Button_Default,

		Action_Slash,
		Action_Spear,
		Action_Arrow,
		Action_Bomb,
		Action_Wind,
		Action_Thunder,
		Action_Drain,
		Action_SmallBomb,
		Action_Seal,
		Action_Drain2,
		Spin,
	}
	class DelaySE
	{
		public int _frame = 0;
		public int _frameEnd;
		public SoundType _soundType;
	}

	readonly Dictionary<SoundType , string> _soundDic = new Dictionary<SoundType , string>() {
		{ SoundType.TitleBGM ,"Sound/BGM/WAV_Pure Feelings - Loop00"},
		{ SoundType.HomeBGM ,"Sound/BGM/WAV_Summer Around Nature - Loop00"},
		{ SoundType.BattleBGM ,"Sound/BGM/Downhill Chase LOOP"},
		{ SoundType.PutCard ,"Sound/se_putCard"},
		{ SoundType.Button_Default ,"Sound/se_buttonDefault"},
		{ SoundType.MoveChara ,"Sound/se_charaMove"},
		{ SoundType.Action_Slash ,"Sound/se_slash2"},
		{ SoundType.Action_Spear ,"Sound/se_spear"},
		{ SoundType.Action_Arrow ,"Sound/se_arrow"},
		{ SoundType.Action_Bomb ,"Sound/se_bomb"},
		{ SoundType.Action_Thunder ,"Sound/se_thunder"},
		{ SoundType.Action_Drain ,"Sound/se_drain"},
		{ SoundType.Action_Drain2 ,"Sound/se_drain2"},
		{ SoundType.Action_SmallBomb ,"Sound/se_meteo2"},
		{ SoundType.Action_Seal ,"Sound/se_seal"},
		{ SoundType.Action_Wind ,"Sound/se_wind"},
		{ SoundType.Hit ,"Sound/se_hit"},
		{ SoundType.Gimic_Warp ,"Sound/se_warp"},
		{ SoundType.Gimic_PowerUp ,"Sound/se_powerUp"},
		{ SoundType.Gimic_Coin,"Sound/se_coin"},
		{ SoundType.SlideIn ,"Sound/se_slideIn"},
		{ SoundType.SlideOut ,"Sound/se_slideOut"},
		{ SoundType.EquipLevelUp ,"Sound/se_levelUp_5"},
		{ SoundType.PlayerLevelUp ,"Sound/se_levelUp"},
		{ SoundType.Spin,"Sound/se_spin"},
		{ SoundType.Confetti,"Sound/cracker"},
	};

	const float BGMVolumeBase = 0.3f;
	const float SEVolumeBase = 0.3f;

	SoundType _loadTargetBGM;
	List<SoundType> _loadTargetSEList = new List<SoundType>();
	AudioListener _audioListener;
	AudioSource _audioSourceBGM;
	AudioSource _audioSourceSE;
	AudioClip _bgmAudioClip;
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

	public void AddLoadTarget_BGM( SoundType soundType )
	{
		_loadTargetBGM = soundType;
	}

	public void AddLoadTarget_SE( SoundType soundType )
	{
		_loadTargetSEList.Add( soundType );
	}

	public IEnumerator LoadAsync()
	{
		_loadTargetSEList.Add( SoundType.Button_Default );

		var resourceRequestDic = new Dictionary<SoundType , ResourceRequest >();

		if( _soundDic.ContainsKey( _loadTargetBGM ) )
		{
			resourceRequestDic.Add( _loadTargetBGM , Resources.LoadAsync<AudioClip>( _soundDic[ _loadTargetBGM ] ) );
		}

		foreach( var loadTarget in _loadTargetSEList )
		{
			if( _seAudioClipDic.ContainsKey( loadTarget ) )
			{
				continue;
			}
			if( ! _soundDic.ContainsKey( loadTarget ) )
			{
				continue;
			}

			resourceRequestDic.Add( loadTarget , Resources.LoadAsync<AudioClip>( _soundDic[ loadTarget ] ) );
		}

		bool isLoadResources = true;
		do
		{
			isLoadResources = false;
			foreach( var keyValue in resourceRequestDic )
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



		if( _loadTargetBGM != SoundType.None )
		{
			_bgmAudioClip = GameObject.Instantiate<AudioClip>( resourceRequestDic[ _loadTargetBGM ].asset as AudioClip );
			if( _audioSourceBGM.clip == null ||  _audioSourceBGM.clip.name != _bgmAudioClip .name )
			{
				_audioSourceBGM.clip = _bgmAudioClip;
				_audioSourceBGM.Play();
			}

		}

		foreach( var loadTarget in _loadTargetSEList )
		{
			if( _seAudioClipDic.ContainsKey( loadTarget ) )
			{
				continue;
			}
			if( !_soundDic.ContainsKey( loadTarget ) )
			{
				continue;
			}

			var audioClip = GameObject.Instantiate<AudioClip>(
				resourceRequestDic[ loadTarget ].asset as AudioClip
			);
			_seAudioClipDic.Add( loadTarget , audioClip );
		}

		_loadTargetSEList.Clear();
		_enable = true;
	}

	public void ClearSEList()
	{
		_seAudioClipDic.Clear();
		_enable = false;
	}


	public void PlaySE( SoundType soundType )
	{
		if( ! _enable )
		{
			return;
		}

		if( ! _seAudioClipDic.ContainsKey( soundType ) )
		{
			return;
		}
		_audioSourceSE.PlayOneShot( _seAudioClipDic[ soundType ] );
	}

	public void DelayPlaySE( SoundType soundType , int frame )
	{
		_delaySEList.Add( new DelaySE() {
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