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
	[SerializeField] float loadingAmountPerFrame;

	Animator animator;
	float currentFillAmount;
	float desiredPercentageToReach;

	void Start () 
	{
		animator = GetComponent<Animator>();
		desiredPercentageToReach = 0;
		currentFillAmount = 0;
		loadingBarFill.fillAmount = currentFillAmount;
	}
	
	void FixedUpdate () 
	{
		if (currentFillAmount < desiredPercentageToReach)
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
		desiredPercentageToReach = percentageToReach;
	}
}
