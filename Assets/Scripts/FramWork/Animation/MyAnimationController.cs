using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MyAnimationController : Singleton<MyAnimationController>
{
	List<MyAnimation> _myAnimationList = new List<MyAnimation>();

	public void Add( MyAnimation myAnimation )
	{
		_myAnimationList.Add( myAnimation );
	}

	public void Remove( MyAnimation myAnimation )
	{
		_myAnimationList.Remove( myAnimation );
	}

	public void Update()
	{
		foreach( var myAnimation in _myAnimationList )
		{
			myAnimation.Update();
		}
	}
}