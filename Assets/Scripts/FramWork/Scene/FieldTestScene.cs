using System.Collections;
using UnityEngine;

namespace Assets.Scripts.FramWork.Scene
{
	public class FieldTestScene : MonoBehaviour
	{

		FielldTestSpace.FieldTest _fieldTest;
		void Awake()
		{
			_fieldTest = new FielldTestSpace.FieldTest( gameObject );
		}

		private void FixedUpdate()
		{
		}

		private void Update()
		{
			_fieldTest.Update();
		}

		private void LateUpdate()
		{
		}
	}
}