using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	static InputManager m_instance;
	public static InputManager Instance { get { return m_instance; } }
	const float THRESHOLD = 0.5f;	// 閾値
	delegate bool PressFunc();

	class TriggerData{
		bool m_oldPress = false;
		bool m_nowPress = false;
		PressFunc m_func;
		public bool isTrigger{get;set;}
		public TriggerData(PressFunc func){
			m_func = func;
			isTrigger = false;
		}
		public void Update(){
			m_oldPress = m_nowPress;
			m_nowPress = m_func();
		}
		public bool IsTrigger(){
			return m_nowPress &&  m_oldPress==false;
		}
	}
	List<TriggerData> m_triggerData = new List<TriggerData>{
		new TriggerData(IsPressRight),
		new TriggerData(IsPressLeft),
		new TriggerData(IsPressUp),
		new TriggerData(IsPressDown),
		new TriggerData(IsPressAction),
	};
	enum eKeyType{
		Right = 0,
		Left,
		Up,
		Down,
		Action
	}
    // Start is called before the first frame update
    void Start()
    {
		DontDestroyOnLoad(gameObject);
		m_instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
		foreach(var item in m_triggerData){
			item.Update();
		}
		/*
		if(IsTriggerRight()){
			Debug.Log("TriggerRight");
		}
		if(IsTriggerLeft()){
			Debug.Log("TriggerLeft");
		}
		if(IsTriggerUp()){
			Debug.Log("TriggerUp");
		}
		if(IsTriggerDown()){
			Debug.Log("TriggerDown");
		}
		if(IsTriggerAction()){
			Debug.Log("TriggerAction");
		}
		*/
    }
	public static bool IsTriggerRight(){
		if(m_instance == null){return false;}
		return m_instance.m_triggerData[(int)eKeyType.Right].IsTrigger();
	}
	public static bool IsTriggerLeft(){
		if(m_instance == null){return false;}
		return m_instance.m_triggerData[(int)eKeyType.Left].IsTrigger();
	}
	public static bool IsTriggerUp(){
		if(m_instance == null){return false;}
		return m_instance.m_triggerData[(int)eKeyType.Up].IsTrigger();
	}
	public static bool IsTriggerDown(){
		if(m_instance == null){return false;}
		return m_instance.m_triggerData[(int)eKeyType.Down].IsTrigger();
	}
	public static bool IsTriggerAction(){
		if(m_instance == null){return false;}
		return m_instance.m_triggerData[(int)eKeyType.Action].IsTrigger();
	}
	public static bool IsPressRight(){
		return Input.GetAxis("Horizontal") >= THRESHOLD;
	}
	public static bool IsPressLeft(){
		return Input.GetAxis("Horizontal") <= -THRESHOLD;
	}
	public static bool IsPressUp(){
		return Input.GetAxis("Vertical") >= THRESHOLD;
	}
	public static bool IsPressDown(){
		return Input.GetAxis("Vertical") <= -THRESHOLD;
	}
	public static bool IsPressAction(){
		return Input.GetAxis("Action") >= THRESHOLD;
	}
}
