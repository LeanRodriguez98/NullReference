using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum Lenguges
    {
        English,
        Spanish,
        Count
    }
    public SO_GameOptions gameOptions;
    #region Singleton
    private static GameManager instance;
	public static GameManager GetInstance()
	{
		return instance;
	}
    #endregion

    private void Awake()
	{
		instance = this;
        Utilities.LoadGame(gameOptions);
		if(gameOptions.lenguage == (int)Lenguges.English/*PlayerPrefs.GetInt("SubtitleLenguage") == 0*/)
		{
            aivaObjective = SubtitleManager.instance.GetAudio("AIVAObjective").englishSubtitles;
			coffeeObjective = SubtitleManager.instance.GetAudio("CoffeObjective").englishSubtitles;
		}
		else
		{
		    aivaObjective = SubtitleManager.instance.GetAudio("AIVAObjective").spanishSubtitles;
			coffeeObjective = SubtitleManager.instance.GetAudio("CoffeObjective").spanishSubtitles;
		}
	}

	public GameObject playerUI;
	private string aivaObjective;

	public string GetAivaObjective()
	{
		return aivaObjective;
	}

	private string coffeeObjective;

	public string GetCoffeObjective()
	{
		return coffeeObjective;
	}

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
