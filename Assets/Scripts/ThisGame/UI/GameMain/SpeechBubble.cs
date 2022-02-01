using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
	[SerializeField]
	TMPro.TextMeshProUGUI m_text;

	Animation m_anim;

	float m_timer = 0;
	float m_timeEnd = 0;
	System.Action m_timerCntAction;

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
		m_timerCntAction?.Invoke();

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

	void Update_ToClose()
	{
		m_timer += Time.deltaTime;

		if( m_timer > m_timeEnd )
		{
			m_timerCntAction = null;
			Close();
		}
	}


	/// <summary>
	/// 開始
	/// </summary>
	/// <param name="pos">表示する座標</param>
	/// <param name="text">表示する文字列</param>
	public void Open(Vector3 pos, string text){
		m_state = eState.In;
		m_anim.Play("SpeechBubbleIn");
		transform.localPosition = pos;
		m_text.text = text;

		m_timerCntAction = Update_ToClose;
		m_timer = 0;
		m_timeEnd = text.Length * 0.5f;
	}

	/// <summary>
	/// 終了
	/// </summary>
	public void Close(){
		if( m_state == eState.None || m_state == eState.Out )
		{
			return;
		}
		m_state = eState.Out;
		m_anim.Play("SpeechBubbleOut");
	}
}
