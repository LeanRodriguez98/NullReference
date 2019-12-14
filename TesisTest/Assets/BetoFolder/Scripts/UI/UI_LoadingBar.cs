using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingBar : MonoBehaviour 
{
	#region Singleton
	public static UI_LoadingBar Instance;
	private void Awake()
	{
		Instance = this;
	}
	#endregion

	[SerializeField] Image loadingBarFill;
	[SerializeField] float timeToStartLoading;
	[SerializeField] float loadingAmountPerFrame;

	Animator animator;
	float currentFillAmount;
	float desiredPercentageToReach;

	float tempPercentage;
	bool runningFadeInAnimation;

	void Start () 
	{
		animator = GetComponent<Animator>();
		tempPercentage = 0;
		desiredPercentageToReach = 0;
		currentFillAmount = 0;
		runningFadeInAnimation = false;
		loadingBarFill.fillAmount = currentFillAmount;
	}
	
	void Update () 
	{
		if (!runningFadeInAnimation) return;

		if (currentFillAmount < desiredPercentageToReach)
		{
			currentFillAmount += loadingAmountPerFrame;
			loadingBarFill.fillAmount = currentFillAmount * 0.01f;
		}
		else
		{
			runningFadeInAnimation = false;
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
		runningFadeInAnimation = true;
	}
}
