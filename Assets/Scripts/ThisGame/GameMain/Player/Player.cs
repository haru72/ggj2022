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
			void ZeroCandleCallBack();
			void FinishMove(Vector3 nowMasu);
		}

		GameObject GameObject { get; }
		IPlayer PlayerInterface { get; }

		[SerializeField] float m_moveSpeed = 3.0f;		// 移動スピード
		[SerializeField] float m_turnSpeed = 540.0f;    // 回転スピード
		[SerializeField] int m_candleNum = 10;			// ろうそく初期所持数

		bool m_isMove = false;		// 現在動いているかどうかのフラグ
		bool m_isTurn = false;		// 現在回転しているかどうかのフラグ
		Vector3 m_nextMasuPos;		// 移動先のマスの座標
		Quaternion m_NextMasuRot;   // 移動先のマスへの回転量

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

			if (m_candleNum == 0) PlayerInterface.ZeroCandleCallBack();
		}

		public int GetSetCandleNum
		{
			get { return m_candleNum; }
			set { if (0 < m_candleNum) m_candleNum = value; }
		}

		public Vector3 GetNowMasu
        {
            get { return GameObject.transform.position; } // プレイヤー座標を返す
        }

        public void Move(Vector3 nextMasu)
        {
			if (m_isMove) return; // 移動中は操作不可

			// 移動先のマスの座標
			m_nextMasuPos = GameObject.transform.position + nextMasu;
			if (!PlayerInterface.CanMove(m_nextMasuPos)) return; // 移動不可
			// 移動先のマスへの回転量
			m_NextMasuRot = Quaternion.LookRotation(m_nextMasuPos - GameObject.transform.position, Vector3.up);

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
				PlayerInterface.FinishMove(GameObject.transform.position);
			}
		}
	}
}
