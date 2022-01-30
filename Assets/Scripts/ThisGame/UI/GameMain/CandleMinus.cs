using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleMinus : MonoBehaviour
{
	Animation m_anim;
    // Start is called before the first frame update
    void Awake()
    {
        m_anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
		if(m_anim.isPlaying == false){
			gameObject.SetActive(false);
		}
    }
	public void Appear(){
		gameObject.SetActive(true);
		if(m_anim.isPlaying){
			m_anim.Stop();
		}
		m_anim.Play("CandleMinusIn");
	}
}
