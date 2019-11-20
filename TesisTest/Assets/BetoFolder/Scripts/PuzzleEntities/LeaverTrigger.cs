using UnityEngine;
using BetoScripts;

public class LeaverTrigger : MonoBehaviour 
{
	[SerializeField] Leaver leaver;

	private void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUpable"))
			leaver.Interact();
	}
}
