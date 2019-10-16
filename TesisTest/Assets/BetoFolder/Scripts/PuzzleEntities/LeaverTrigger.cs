using UnityEngine;
using BetoScripts;

public class LeaverTrigger : MonoBehaviour 
{
	[SerializeField] Leaver leaver;
	[SerializeField] LeaverSide leaverSide;

	public enum LeaverSide { Left, Right }

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUpable"))
			leaver.OnActivatedByTrigger(leaverSide);
	}
}
