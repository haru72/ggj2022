using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogBehaviour : MonoBehaviour
{
	const float LineSize = 60;
	RectTransform _contentRectTransform;
	TMPro.TMP_InputField _inputField;
	Button _closeButton;

	string _str = "";

	public void Init()
    {

#if DEBUG

		_contentRectTransform = transform.
			Find( "Scroll View" ).
			Find( "Viewport" ).
			Find( "Content" ).
			GetComponent<RectTransform>();
		_inputField = _contentRectTransform.Find( "InputField" ).GetComponent<TMPro.TMP_InputField>();
		_closeButton = transform.Find( "CloseButton" ).GetComponent<Button>();
		_closeButton.onClick.AddListener(()=> {
			gameObject.SetActive( false );
		} );

		Application.logMessageReceived += HandleLog;

#endif

	}

	// Update is called once per frame
	void Update()
    {
        
    }

	void HandleLog( string logString , string stackTrace , LogType type )
	{
		if( type != LogType.Error )
		{
			return;
		}

		
		try
		{
			gameObject.SetActive( true );
		}
		catch
		{
		}
	}


	public void AddStr( string str )
	{

#if DEBUG


		_str += str + "\n";
		try
		{
			if( gameObject.activeSelf )
			{
			} else
			{
				SetupText( _str + "\n" );
			}
		} catch
		{
		}

#endif
	}


	public void SetupText( string text )
	{
		_inputField.text = text;

		var ary = text.Split('\n');
		int lineNum = ary.Length;

		_contentRectTransform.sizeDelta = new Vector2(
			_contentRectTransform.sizeDelta.x ,
			LineSize * lineNum
		);
	}

}
