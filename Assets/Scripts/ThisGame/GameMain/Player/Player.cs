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

		public enum E_MoveDirection // プレイヤーの移動方向
		{
			Forward,
			Right,
			Left,
			Back,
		}

		GameObject GameObject { get; }
		IPlayer PlayerInterface { get; }

		[SerializeField] float m_moveSpeed = 3.0f;		// 移動スピード
		[SerializeField] float m_turnSpeed = 360.0f;	// 回転スピード

		const int m_Masu = 1;		// マスの大きさ（仮）

		bool m_isMove = false;		// 現在動いているかどうかのフラグ
		bool m_isTurn = false;		// 現在回転しているかどうかのフラグ
		Vector3 m_nextMasuPos;		// 移動先のマスの座標
		Quaternion m_NextMasuRot;	// 移動先のマスへの回転量

		public Player( GameObject gameObject , IPlayer playerInterface )
		{
			GameObject = gameObject;
			PlayerInterface = playerInterface;
        }

		public void Update()
		{
			if (m_isTurn)
			{
				TurnNextMasu();
			}
			else
			{
				if (m_isMove) MoveNextMasu();
			}
		}

		public void Move(E_MoveDirection eNextDirection)
        {
			if (m_isMove) return; // 移動中は操作不可

			// 移動先のマスの座標を求める
			Vector3 m_moveRight = new Vector3(m_Masu, 0f, 0f);
			Vector3 m_moveForward = new Vector3(0f, 0f, m_Masu);
			if (eNextDirection == E_MoveDirection.Forward)
			{
				m_nextMasuPos = GameObject.transform.position + m_moveForward;	// 前
			}
			else if (eNextDirection == E_MoveDirection.Right)
			{
				m_nextMasuPos = GameObject.transform.position + m_moveRight;	// 右
			}
			else if (eNextDirection == E_MoveDirection.Left)
			{
				m_nextMasuPos = GameObject.transform.position - m_moveRight;	// 左
			}
			else if (eNextDirection == E_MoveDirection.Back)
			{
				m_nextMasuPos = GameObject.transform.position - m_moveForward;	// 後ろ
			}

			if (PlayerInterface.CanMove(m_nextMasuPos)) return; // 移動不可のマス

			m_NextMasuRot = Quaternion.LookRotation(GameObject.transform.position - m_nextMasuPos, Vector3.up);
			m_isMove = true;
			m_isTurn = true;
		}

		private void TurnNextMasu()
		{

			if (GameObject.transform.rotation != m_NextMasuRot)
			{
				// プレイヤーの回転
				GameObject.transform.rotation = Quaternion.RotateTowards(GameObject.transform.rotation, m_NextMasuRot, m_turnSpeed * Time.deltaTime);
			}
			else
			{
				// 回転し終わったら回転フラグを下ろす
				m_isTurn = false;
			}
		}

		private void MoveNextMasu()
		{
			if (GameObject.transform.position != m_nextMasuPos)
			{
				// プレイヤーの移動
				GameObject.transform.position = Vector3.MoveTowards(GameObject.transform.position, m_nextMasuPos, m_moveSpeed * Time.deltaTime);
			}
			else
			{
				// 移動し終わったら移動フラグを下ろす
				m_isMove = false;
			}
		}
	}
}
