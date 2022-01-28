using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InputController : Singleton<InputController>
{
	Flick _flick = new Flick();

	class InputButtonWorker
	{
		Type _type;

		Action<object , Action> _actionPressMain;
		Action<object , Action> _actionTriggerMain;
		Action<object , Action> _actionReleaseMain;

		Dictionary<object , Action> _actionPressDic = new Dictionary<object , Action>();
		Dictionary<object , Action> _actionTriggerDic = new Dictionary<object , Action>();
		Dictionary<object , Action> _actionReleaseDic = new Dictionary<object , Action>();

		public InputButtonWorker( Type type )
		{
			_type = type;
		}

		public void Update()
		{
			if( _actionPressMain != null )
			{
				foreach( var actionKeyVal in _actionPressDic )
				{
					_actionPressMain( actionKeyVal.Key , actionKeyVal.Value );
				}
			}

			if( _actionTriggerMain != null )
			{
				foreach( var actionKeyVal in _actionTriggerDic )
				{
					_actionTriggerMain( actionKeyVal.Key , actionKeyVal.Value );
				}
			}

			if( _actionReleaseMain != null )
			{
				foreach( var actionKeyVal in _actionReleaseDic )
				{
					_actionReleaseMain( actionKeyVal.Key , actionKeyVal.Value );
				}
			}
		}

		public void SetPressActionMain( Action<object , Action> action )
		{
			_actionPressMain = action;
		}

		public void SetTriggerActionMain( Action<object , Action> action )
		{
			_actionTriggerMain = action;
		}

		public void SetReleaseActionMain( Action<object , Action> action )
		{
			_actionReleaseMain = action;
		}

		public bool IsMatchType( Type type )
		{
			return _type == type;
		}

		public void AddPressAction( object obj , Action action )
		{
			if( _actionPressDic.ContainsKey( obj ) )
			{
				return;
			}
			_actionPressDic.Add( obj , action );
		}

		public void AddTriggerAction( object obj , Action action )
		{
			if( _actionTriggerDic.ContainsKey( obj ) )
			{
				return;
			}
			_actionTriggerDic.Add( obj , action );
		}

		public void AddReleaseAction( object obj , Action action )
		{
			if( _actionReleaseDic.ContainsKey( obj ) )
			{
				return;
			}
			_actionReleaseDic.Add( obj , action );
		}

	}

	InputButtonWorker _inputButtonWorkerByKeyCode = new InputButtonWorker( typeof( KeyCode ) );
	InputButtonWorker _inputButtonWorkerByInputManager = new InputButtonWorker( typeof( string ) );

	Dictionary<string , Action<float>> _inputAxisActionDic = new Dictionary<string , Action<float>>();

	protected override void InitSub()
	{
		_inputButtonWorkerByKeyCode.SetPressActionMain( ( obj , action ) =>
		{
			if( Input.GetKey( (KeyCode)obj ) )
			{
				action();
			}
		} );

		_inputButtonWorkerByKeyCode.SetTriggerActionMain( ( obj , action ) =>
		{
			if( Input.GetKeyDown( (KeyCode)obj ) )
			{
				action();
			}
		} );

		_inputButtonWorkerByKeyCode.SetReleaseActionMain( ( obj , action ) =>
		{
			if( Input.GetKeyUp( (KeyCode)obj ) )
			{
				action();
			}
		} );


		_inputButtonWorkerByInputManager.SetPressActionMain( ( obj , action ) =>
		{
			if( Input.GetButton( (string)obj ) )
			{
				action();
			}
		} );

		_inputButtonWorkerByInputManager.SetTriggerActionMain( ( obj , action ) =>
		{
			if( Input.GetButtonDown( (string)obj ) )
			{
				action();
			}
		} );

		_inputButtonWorkerByInputManager.SetReleaseActionMain( ( obj , action ) =>
		{
			if( Input.GetButtonUp( (string)obj ) )
			{
				action();
			}
		} );

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="obj">keycode , ProjectSetting -> Inputで設定したやつ</param>
	/// <param name="action"></param>
	public void AddPressAction( object obj , Action action )
	{
		var type = obj.GetType();
		if( _inputButtonWorkerByKeyCode.IsMatchType( type ) )
		{
			_inputButtonWorkerByKeyCode.AddPressAction( obj , action );
		}
		if( _inputButtonWorkerByInputManager.IsMatchType( type ) )
		{
			_inputButtonWorkerByInputManager.AddPressAction( obj , action );
		}
	}

	public void AddTriggerAction( object obj , Action action )
	{
		var type = obj.GetType();
		if( _inputButtonWorkerByKeyCode.IsMatchType( type ) )
		{
			_inputButtonWorkerByKeyCode.AddTriggerAction( obj , action );
		}
		if( _inputButtonWorkerByInputManager.IsMatchType( type ) )
		{
			_inputButtonWorkerByInputManager.AddTriggerAction( obj , action );
		}
	}

	public void AddReleaseAction( object obj , Action action )
	{
		var type = obj.GetType();
		if( _inputButtonWorkerByKeyCode.IsMatchType( type ) )
		{
			_inputButtonWorkerByKeyCode.AddReleaseAction( obj , action );
		}
		if( _inputButtonWorkerByInputManager.IsMatchType( type ) )
		{
			_inputButtonWorkerByInputManager.AddReleaseAction( obj , action );
		}
	}

	public void AddAxisAction( string str , Action<float> action )
	{
		_inputAxisActionDic.Add( str , action );
	}

	public Vector2 GetTouchPos()
	{
		return Input.mousePosition;
	}

	public bool IsFlickToDir( Flick.FlickDir dir )
	{
		return _flick.IsFlickToDir( dir );
	}



	public void Update()
	{
		_flick.Update();

		_inputButtonWorkerByKeyCode.Update();
		_inputButtonWorkerByInputManager.Update();

		foreach( var inputAxisAction in _inputAxisActionDic )
		{
			var axisName = inputAxisAction.Key;
			inputAxisAction.Value( Input.GetAxis( axisName ) );
		}


	}

}
