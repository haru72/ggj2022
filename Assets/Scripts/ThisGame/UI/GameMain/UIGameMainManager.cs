using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameMainManager : MonoBehaviour
{
	[SerializeField]
	GameObject m_gameUIObj;
	[SerializeField]
	GameObject m_candleNumObj;
	[SerializeField]
	GameObject m_tutorialObj;
	[SerializeField]
	SpeechBubble m_SpeechBubble;
	CandleNum m_candleNum;
	Tutorial m_tutorial;
    // Start is called before the first frame update
    void Start()
    {
        m_candleNum = m_candleNumObj.GetComponent<CandleNum>();
		Debug.Log( m_candleNum );
		m_tutorial = m_tutorialObj.GetComponent<Tutorial>();
		Debug.Log( m_tutorial );

		// TODO フェードのデバッグ
		//SceneManager.LoadScene("Fade", LoadSceneMode.Additive);
	}

	// Update is called once per frame
	void Update()
    {
		// TODO デバッグ用
#if false
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			SetCandleNum(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)){
			SetCandleNum(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha0)){
			SetCandleNum(10);
		}
		if(Input.GetKeyDown(KeyCode.A)){
			TutorialPrevPage();
		}
		if(Input.GetKeyDown(KeyCode.D)){
			TutorialNextPage();
		}
		if(Input.GetKeyDown(KeyCode.O)){
			TutorialOpen();
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			TutorialClose();
		}
		if(Input.GetKeyDown(KeyCode.G)){
			GameUIOpen();
		}
		if(Input.GetKeyDown(KeyCode.H)){
			GameUIClose();
		}
		if(Input.GetKeyDown(KeyCode.Z)){
			FadeManager.FadeOut();
		}
		if(Input.GetKeyDown(KeyCode.X)){
			FadeManager.FadeIn();
		}
		if(Input.GetKeyDown(KeyCode.C)){
			SpeechBubbleOpen(new Vector3(300, 160, 0), "おじいちゃんのロウソク 暖かいな……");
		}
		if(Input.GetKeyDown(KeyCode.V)){
			SpeechBubbleClose();
		}
#endif
    }
	/// <summary>
	/// ゲームUI開始
	/// </summary>
	public void GameUIOpen(){
		m_gameUIObj.SetActive(true);
	}
	/// <summary>
	/// ゲームUI終了
	/// </summary>
	public void GameUIClose(){
		m_gameUIObj.SetActive(false);
	}
	/// <summary>
	/// キャンドルの残り個数を設定
	/// </summary>
	/// <param name="num">残り個数</param>
	public void SetCandleNum(int num){
		m_candleNum.SetNum(num);
	}

	/// <summary>
	/// チュートリアル画面開始
	/// </summary>
	public void TutorialOpen(){
		m_tutorial.Open();
	}
	/// <summary>
	/// チュートリアル画面終了
	/// </summary>
	public void TutorialClose(){
		m_tutorial.Close();
	}
	/// <summary>
	/// 次のページへ移動
	/// </summary>
	public void TutorialNextPage(){
		m_tutorial.NextPage();
	}
	/// <summary>
	/// 前のページへ移動
	/// </summary>
	public void TutorialPrevPage(){
		m_tutorial.PrevPage();
	}

	/// <summary>
	/// 吹き出し開始
	/// </summary>
	/// <param name="pos">表示する座標</param>
	/// <param name="text">表示する文字列</param>
	public void SpeechBubbleOpen(Vector3 pos, string text){
		m_SpeechBubble.Open(pos, text);
	}

	/// <summary>
	/// 吹き出し終了
	/// </summary>
	public void SpeechBubbleClose(){
		m_SpeechBubble.Close();
	}
}
