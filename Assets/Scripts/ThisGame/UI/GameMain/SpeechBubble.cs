using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
	[SerializeField]
	Text m_text;

	Animation m_anim;

	enum eState{
		None,
		In,
		Main,
		Out
	}
	eState m_state = eState.None;
    // Start is called before the first frame update
    void Start()
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
				m_text.text = "";
			}
			break;
		}
    }

	/// <summary>
	/// 開始
	/// </summary>
	/// /// <param name="pos">表示する座標</param>
	/// <param name="text">表示する文字列</param>
	public void Open(Vector3 pos, string text){
		m_state = eState.In;
		m_anim.Play("SpeechBubbleIn");
		transform.localPosition = pos;
		m_text.text = text;
	}

	/// <summary>
	/// 終了
	/// </summary>
	public void Close(){
		m_state = eState.Out;
		m_anim.Play("SpeechBubbleOut");
	}
}
