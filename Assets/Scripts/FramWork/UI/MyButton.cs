using UnityEngine;
using UnityEngine.UI;

public class MyButton : MonoBehaviour
{
	[SerializeField]
	MyAudioController.SoundType _soundType = MyAudioController.SoundType.Button_Default;

	private void Awake()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(()=> {
			MyAudioController.GetInstance().PlaySE( _soundType );
		} );
	}
}