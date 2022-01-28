using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameMainManager : MonoBehaviour
{
	[SerializeField]
	GameObject m_candleNumObj;
	CandleNum m_candleNum;
    // Start is called before the first frame update
    void Start()
    {
        m_candleNum = m_candleNumObj.GetComponent<CandleNum>();
    }

    // Update is called once per frame
    void Update()
    {
    }
	/// <summary>
	/// キャンドルの残り個数を設定
	/// </summary>
	/// <param name="num">残り個数</param>
	public void SetCandleNum(int num){
		m_candleNum.SetNum(num);
	}
}
