using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameMainManager : MonoBehaviour
{
	[SerializeField]
	GameObject m_gameUIObj;
	[SerializeField]
	GameObject m_candleNumObj;
	[SerializeField]
	GameObject m_tutorialObj;
	CandleNum m_candleNum;
	Tutorial m_tutorial;
    // Start is called before the first frame update
    void Start()
    {
        m_candleNum = m_candleNumObj.GetComponent<CandleNum>();
		m_tutorial = m_tutorialObj.GetComponent<Tutorial>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
