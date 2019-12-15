using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObjectWhenLookedAt : RaycastTriggerEvent 
{
	[SerializeField] GameObject objectToEnable;

	public override void TriggerEvent() 
	{
		objectToEnable.SetActive(true);
	}
}
