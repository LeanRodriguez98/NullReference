using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingBar : MonoBehaviour 
{
	[SerializeField] Image loadingBarFill;
	[SerializeField] float timeToStartLoading;
	[SerializeField] float loadingAmountPerFrame;

	Animator animator;
	float currentFillAmount;
	float desiredPercentageToReach;

	float tempPercentage;

	void Start () 
	{
		animator = GetComponent<Animator>();
		tempPercentage = 0;
		desiredPercentageToReach = 0;
		currentFillAmount = 0;
	}
	
	void Update () 
	{
		if (currentFillAmount <= desiredPercentageToReach)
		{
			currentFillAmount += loadingAmountPerFrame;
			loadingBarFill.fillAmount = currentFillAmount * 0.01f;
		}
		else
		{
			animator.SetBool("OnDisplay", false);
		}
	}

	public void DisplayLoadingBar(int percentageToReach)
	{
		animator.SetBool("OnDisplay", true);

		tempPercentage = percentageToReach;
		Invoke("SetDesiredPercentage", timeToStartLoading);
	}

	void SetDesiredPercentage()
	{
		desiredPercentageToReach = tempPercentage;
	}
}
