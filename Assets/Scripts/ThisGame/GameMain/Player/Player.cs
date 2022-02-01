using UnityEngine;
using System;
using System.Collections.Generic;

namespace GameMainSpace.PlayerSpace
{
	public class Player
	{
		public interface IPlayer
		{
			float MoveSpeed { get; }		//移動速度
			float TurnSpeed { get; }		//回転速度
			bool CanMove( Vector3 nextMasu );
			void FinishMove( Vector3 nowMasu );
		}

		GameObject GameObject { get; }
		IPlayer PlayerInterface { get; }
		public Vector3 Pos => GameObject.transform.position;
		public void SetPos( Vector3 pos )
		{
			GameObject.transform.position = pos + Vector3.zero;
		}

		int m_candleNum = 10;           // ろうそく所持数
		int m_keyNum = 0;
		bool m_isMove = false;		// 現在動いているかどうかのフラグ
		bool m_isTurn = false;		// 現在回転しているかどうかのフラグ
		Vector3 m_nextMasuPos;		// 移動先のマスの座標
		Quaternion m_NextMasuRot;   // 移動先のマスへの回転量

		MyAnimation MyAnimation { get; }

		public Player( GameObject gameObject , IPlayer playerInterface )
		{
			GameObject = gameObject;
			PlayerInterface = playerInterface;

			MyAnimation = new MyAnimation();
			MyAnimation.Init( gameObject.GetComponentInChildren<Animator>() );

		}

		public void Update()
		{
			if ( m_isTurn )
			{
				TurnNextMasu();
			}
			else
			{
				if (m_isMove) MoveNextMasu();
			}
		}

		public int GetSetCandleNum
		{
			get { return m_candleNum; }
			set { m_candleNum = value; }
		}
		public int GetSetKeyNum
		{
			get { return m_keyNum; }
			set { m_keyNum = value; }
		}


		public Vector3 GetNowMasu
        {
            get { return GameObject.transform.position; } // プレイヤー座標を返す
        }

		public Vector3 GetForward
		{
			get { return GameObject.transform.forward; } // プレイヤーの正面方向を返す
		}

		public bool CanAction()
		{
			return MyAnimation.IsPlaying( "Stay" );
		}

		public void LightupCandle()
		{
			MyAudioController.GetInstance().PlaySE( MyAudioController.SoundType.LightFire );
			MyAnimation.Play( "Light" , 1 );
		}
		public void Damage()
		{
			MyAnimation.Play( "Damage" , 1 );
		}
		public void Pickup()
		{
			MyAudioController.GetInstance().PlaySE( MyAudioController.SoundType.GetKey );
			MyAnimation.Play( "PickUp" , 1 );
		}

		public void PickupChandle()
		{
			//MyAudioController.GetInstance().PlaySE( MyAudioController.SoundType.GetKey );
			MyAnimation.Play( "PickUp" , 1 );
		}

		public void Dead()
		{
			MyAnimation.Play( "Dead" , 1 );
		}
		public void Purify()
		{
			MyAudioController.GetInstance().PlaySE( MyAudioController.SoundType.Purify );
			MyAnimation.Play( "Purify" , 1 );
		}

		public void Move(Vector3 nextMasu)
        {
			if (m_isMove) return; // 移動中は操作不可


			// 移動先のマスの座標
			m_nextMasuPos = nextMasu + Vector3.zero;
			// 移動先のマスへの回転量
			m_NextMasuRot = Quaternion.LookRotation(m_nextMasuPos - GameObject.transform.position, Vector3.up);

			if( GameObject.transform.rotation != m_NextMasuRot )
			{
				m_isTurn = true;
				MyAnimation.Play( "Walk" );
				//MyAnimation.SetBool( "IsWalk" , true );
			}
			else if( PlayerInterface.CanMove( m_nextMasuPos ) )
			{
				m_isMove = true;
				MyAnimation.Play( "Walk");
				//MyAnimation.SetBool( "IsWalk" , true );
			}
		}

		private void TurnNextMasu()
		{

			if (GameObject.transform.rotation != m_NextMasuRot)
			{
				// プレイヤーの回転
				GameObject.transform.rotation = Quaternion.RotateTowards(GameObject.transform.rotation, m_NextMasuRot, PlayerInterface.TurnSpeed * Time.deltaTime);
			}
			else
			{
				// 回転し終わったら回転フラグを下ろす
				m_isTurn = false;
				//MyAnimation.SetBool( "IsWalk" , false );
				MyAnimation.Play( "Stay");
				PlayerInterface.FinishMove(GameObject.transform.position);
			}
		}

		private void MoveNextMasu()
		{
			if (GameObject.transform.position != m_nextMasuPos)
			{
				// プレイヤーの移動
				GameObject.transform.position = Vector3.MoveTowards(GameObject.transform.position, m_nextMasuPos, PlayerInterface.MoveSpeed * Time.deltaTime);
			}
			else
			{
				// 移動し終わったら移動フラグを下ろす
				m_isMove = false;
				PlayerInterface.FinishMove(GameObject.transform.position);
			//	MyAnimation.SetBool( "IsWalk" , false );
				MyAnimation.Play( "Stay");
			}
		}

	}
}
