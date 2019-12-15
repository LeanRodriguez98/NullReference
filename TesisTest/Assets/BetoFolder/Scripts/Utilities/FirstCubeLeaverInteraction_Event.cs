using UnityEngine;

public class FirstCubeLeaverInteraction_Event : MonoBehaviour 
{
	[SerializeField] GameObject playerLearnedCubeLeaverMachanic;

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag("PickUpable"))
			playerLearnedCubeLeaverMachanic.SetActive(true);
	}
}
