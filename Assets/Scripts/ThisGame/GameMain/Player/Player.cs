using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace.PlayerSpace
{
	public class Player
	{
		public interface IPlayer
		{
			bool CanMove( Vector3 nextMasu );
		}

		public enum E_PlayerDirection // プレイヤーの向き
		{
			Forward,
			Left,
			Right,
			Back,
		}

		GameObject GameObject { get; }
		IPlayer PlayerInterface { get; }

		const int m_Masu = 1; // マスの大きさ（仮）
		E_PlayerDirection m_eNowDirection;
		Vector3 m_nowMasu;
		Vector3 m_nextMasu;
		bool m_isMove = false; // 現在動いているかどうかのフラグ

		public Player( GameObject gameObject , IPlayer playerInterface )
		{
			GameObject = gameObject;
			PlayerInterface = playerInterface;
			m_eNowDirection = E_PlayerDirection.Forward; // 最初は前を向いている
        }

		public void Update()
		{
			if (m_isMove) MoveNextMasu();
		}

		public void Move(E_PlayerDirection eNextDirection)
        {
			m_nowMasu = GameObject.transform.position;
			m_nextMasu = new Vector3(m_nowMasu.x, m_nowMasu.y, m_nowMasu.z + m_Masu);
			if (PlayerInterface.CanMove(m_nextMasu))
			{
				// 移動可能なら移動フラグを立てる＆必要なら回転
				m_isMove = true;
				if (eNextDirection != m_eNowDirection) RotateNextMasu();
			}
        }

		private void RotateNextMasu()
        {

        }

		private void MoveNextMasu()
		{
			// プレイヤーの移動
			m_nowMasu = Vector3.MoveTowards(m_nowMasu, m_nextMasu, Time.deltaTime);
			// 移動し終わったら移動フラグを下ろす
			if (m_nowMasu == m_nextMasu) m_isMove = false;
        }
	}
}
