using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CandleNum : MonoBehaviour
{
	//TextMeshProUGUI m_text;
	Text m_text;
    // Start is called before the first frame update
    void Start()
    {
        //m_text = gameObject.GetComponent<TextMeshProUGUI>();
        m_text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetNum( int num )
    {
		m_text.text = num.ToString();
        if( num > 0 )
        {
            m_text.color = Color.black;
        }
        else
        {
            m_text.color = Color.red;
        }
    }
}
