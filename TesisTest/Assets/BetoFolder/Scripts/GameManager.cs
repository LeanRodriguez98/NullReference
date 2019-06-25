using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region Singleton
	private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}
	private void Awake()
	{
		instance = this;
	}
	#endregion

	public GameObject playerUI;
	public string aivaObjective;
	public string coffeeObjective;

	public bool RestartedAIVA { get; set; }
	public bool CoffeeMugFound { get; set; }

	public Player player;

	void Start ()
	{
		// The beginning cinematics takes 6 seconds...
		// Prototype trash code
		Invoke("EnablePlayerUI", 6);

		RestartedAIVA = false;
		CoffeeMugFound = false;
	}
	
	void EnablePlayerUI()
	{
		playerUI.SetActive(true);
	}
}
