using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectController : Singleton<ObjectController>
{
	List<ObjectUtility.ObjectCommonAction> _objectCommonActionList = new List<ObjectUtility.ObjectCommonAction>();

	public void Add( ObjectUtility.ObjectCommonAction objectCommonAction )
	{
		_objectCommonActionList.Add( objectCommonAction );
	}

	public void Remove( ObjectUtility.ObjectCommonAction objectCommonAction )
	{
		_objectCommonActionList.Remove( objectCommonAction );
	}


}
