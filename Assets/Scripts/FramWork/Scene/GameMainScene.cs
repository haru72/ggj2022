using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMainScene : MonoBehaviour
{
	GameMainSpace.GameMain _gameMain;
	void Awake()
	{
		_gameMain = new GameMainSpace.GameMain( gameObject );
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
