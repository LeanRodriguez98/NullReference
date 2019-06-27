using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BetoScripts;

public class LeaverTriggerFlag : MonoBehaviour
{
	public Leaver leaver;
	public FlagSide flagSide;
	public enum FlagSide { Front, Back }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PickUpable"))
			leaver.InteractOnCubeCollision(flagSide);
	}

}
