using UnityEngine;

public class LoadingBarDisplayer : MonoBehaviour 
{
	[SerializeField] float displayBarAfterSeconds;
	[SerializeField] int percentageToReach;

	void Start ()
	{
		Invoke("DisplayLoadingBar", displayBarAfterSeconds);
	}

	void DisplayLoadingBar()
	{
		UI_LoadingBar.Instance.DisplayLoadingBar(percentageToReach);
	}
}
