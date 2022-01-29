using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
	[SerializeField]
	List<Image> m_images;
	[SerializeField]
	Text m_pageNow;
	[SerializeField]
	Text m_pageMax;
	int m_imageIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
		ShowPageNow();
        m_pageMax.text = m_images.Count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	/// <summary>
	/// チュートリアル画面開始
	/// </summary>
	public void Open(){
		gameObject.SetActive(true);
	}
	/// <summary>
	/// チュートリアル画面終了
	/// </summary>
	public void Close(){
		gameObject.SetActive(false);
	}
	/// <summary>
	/// 次のページへ移動
	/// </summary>
	public void NextPage(){
		if(m_imageIndex == m_images.Count-1){
			return;
		}
		m_images[m_imageIndex].gameObject.SetActive(false);
		++m_imageIndex;
		m_images[m_imageIndex].gameObject.SetActive(true);
		ShowPageNow();
	}
	/// <summary>
	/// 前のページへ移動
	/// </summary>
	public void PrevPage(){
		if(m_imageIndex == 0){
			return;
		}
		m_images[m_imageIndex].gameObject.SetActive(false);
		--m_imageIndex;
		m_images[m_imageIndex].gameObject.SetActive(true);
		ShowPageNow();
	}
	/// <summary>
	/// 現在のページ数を表示
	/// </summary>
	void ShowPageNow(){
		m_pageNow.text = (m_imageIndex+1).ToString();
	}
}
