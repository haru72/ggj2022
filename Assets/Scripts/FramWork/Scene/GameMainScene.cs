using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainScene : MonoBehaviour , GameMainSpace.GameMainData.IDefine
{
	[SerializeField]
	int _startCandleNum = 10;
	[SerializeField]
	int _cleanRange = 2;
	[SerializeField]
	float _playerMoveSpeed = 10.0f;
	[SerializeField]
	float _playerTurnSpeed = 2000.0f;
	int GameMainSpace.GameMainData.IDefine.StartCandleNum => _startCandleNum;
	int GameMainSpace.GameMainData.IDefine.CleanRange => _cleanRange;

	float GameMainSpace.GameMainData.IDefine.PlayerMoveSpeed => _playerMoveSpeed;
	float GameMainSpace.GameMainData.IDefine.PlayerTurnSpeed => _playerTurnSpeed;

	GameMainSpace.GameMain _gameMain;
	void Awake()
	{
		Application.targetFrameRate = 60;
		_gameMain = new GameMainSpace.GameMain( this );
	}

	private void FixedUpdate()
	{
	}

	private void Update()
	{
		_gameMain.Update();
	}

	private void LateUpdate()
	{
	}

}
