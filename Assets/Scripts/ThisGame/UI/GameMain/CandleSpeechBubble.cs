using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleSpeechBubble : MonoBehaviour
{
	[SerializeField]
	GameObject m_canUse;
	[SerializeField]
	GameObject m_canNotUse;

	Animation m_anim;
	enum eState{
		None,
		In,
		Main,
		Out
	}
	eState m_state = eState.None;
    // Start is called before the first frame update
    void Awake()
    {
		m_anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
		switch(m_state){
		case eState.In:
			if(m_anim.isPlaying == false){
				m_state = eState.Main;
			}
			break;
		case eState.Out:
			if(m_anim.isPlaying == false){
				m_state = eState.None;
				m_canUse.SetActive(false);
				m_canNotUse.SetActive(false);
			}
			break;
		}
    }

	/// <summary>
	/// ろうそく使用できるマークを開始
	/// </summary>
	/// <param name="pos">表示する座標</param>
	public void CanUseOpen(Vector3 pos){
		m_canNotUse.SetActive(false);
		transform.localPosition = pos;
		m_canUse.SetActive(true);
		m_state = eState.In;
		m_anim.Play("CandleSpeechBubbleIn");
	}

	/// <summary>
	/// ろうそく使用できないマークを開始
	/// </summary>
	/// <param name="pos">表示する座標</param>
	public void CanNotUseOpen(Vector3 pos){
		m_canUse.SetActive(false);
		transform.localPosition = pos;
		m_canNotUse.SetActive(true);
		m_state = eState.In;
		m_anim.Play("CandleSpeechBubbleIn");
	}

	/// <summary>
	/// 終了
	/// </summary>
	public void Close(){
		m_state = eState.Out;
		m_anim.Play("CandleSpeechBubbleOut");
	}
}
