using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BetoScripts
{
	public class UI_Player : MonoBehaviour
	{
		[Header("Animator")]
		public Animator UIAivaAnimator;

		[Header("Objectives")]
		public Animator objectivesAnimator;
		//public Text currentObjective;
		public float timeBetweenObjectivesDisplay = 2f;

		[Header("Player actions")]
		public GameObject interactUI;
		public Text interactText;
		
		public GameObject objectGrabbedUI;
		public Text rightClickAction;
		public Text leftClickAction;

		private string pressToThrow;
		private string pressToDrop;
		private string holdToAim;
		private string releaseToCancel;

		private bool m_lookingAtInteractable;

		public enum PlayerInteractionState
		{
			Idle,
			LookingAtInteractable,
			GrabbingObject,
			AimingToThrowObject
		}
		public PlayerInteractionState playerState { get; private set; }

		#region Singleton
		private static UI_Player instance;

		public static UI_Player GetInstance()
		{
			return instance;
		}
		private void Awake()
		{
			instance = this;
		}
		#endregion 

		void Start()
		{
			interactUI.SetActive(false);
			objectGrabbedUI.SetActive(false);

            if (GameManager.GetInstance().gameOptions.lenguage == (int)GameManager.Lenguges.English /*PlayerPrefs.GetInt("SubtitleLenguage") == 0*/)
            {
                interactText.text = SubtitleManager.instance.GetAudio("PressUI").englishSubtitles;
                pressToThrow = SubtitleManager.instance.GetAudio("ThrowUI").englishSubtitles;
                pressToDrop = SubtitleManager.instance.GetAudio("DropUI").englishSubtitles;
				holdToAim = SubtitleManager.instance.GetAudio("AimUI").englishSubtitles;
				releaseToCancel = SubtitleManager.instance.GetAudio("CancelThrowUI").englishSubtitles;
			}
            else
            {
                interactText.text = SubtitleManager.instance.GetAudio("PressUI").spanishSubtitles;
                pressToThrow = SubtitleManager.instance.GetAudio("ThrowUI").spanishSubtitles;
                pressToDrop = SubtitleManager.instance.GetAudio("DropUI").spanishSubtitles;
				holdToAim = SubtitleManager.instance.GetAudio("AimUI").spanishSubtitles;
				releaseToCancel = SubtitleManager.instance.GetAudio("CancelThrowUI").spanishSubtitles;
			}


            m_lookingAtInteractable = false;
			//currentObjective.text = GameManager.GetInstance().GetAivaObjective();
			SetInteractionState(PlayerInteractionState.Idle);

			Aiva.OnRestartEvent += OnObjectiveComplete;
			Aiva.OnRestartEvent += OnAivaRestart;

			CoffeeMug.OnMugFound += OnObjectiveComplete;
		}

		private void OnObjectiveComplete()
		{
			//objectivesAnimator.SetTrigger("OnComplete");
		}

		private void OnAivaRestart()
		{
			// Init finish loading bar animation

			//Invoke("DisplayCoffeeObjective", timeBetweenObjectivesDisplay);
		}

		private void DisplayCoffeeObjective()
		{
			//currentObjective.gameObject.SetActive(false);
			//currentObjective.text = GameManager.GetInstance().GetCoffeObjective();
//
			//currentObjective.gameObject.SetActive(true);
		}

		public void SetInteractionState(PlayerInteractionState playerState)
		{
			this.playerState = playerState;
			
			switch (playerState)
			{
				case PlayerInteractionState.Idle:
					interactUI.SetActive(false);
					objectGrabbedUI.SetActive(false);
					break;

				case PlayerInteractionState.LookingAtInteractable:
					interactUI.SetActive(true);
					objectGrabbedUI.SetActive(false);
					break;

				case PlayerInteractionState.GrabbingObject:
					interactUI.SetActive(false);
					objectGrabbedUI.SetActive(true);
					leftClickAction.text = pressToDrop;
					rightClickAction.text = holdToAim;
					break;

				case PlayerInteractionState.AimingToThrowObject:
					interactUI.SetActive(false);
					objectGrabbedUI.SetActive(true);
					leftClickAction.text = pressToThrow;
					rightClickAction.text = releaseToCancel;
					break;
			}
		}

		public void DisplayPlayerInteractUI(bool isInteractable)
		{
			interactUI.SetActive(isInteractable);
		}

		public void SetAivaUIDisplay(bool display)
		{
			UIAivaAnimator.SetBool("DisplayAiva", display);
		}

		private void OnDestroy() 
		{
			Aiva.OnRestartEvent -= OnAivaRestart;
			Aiva.OnRestartEvent -= OnObjectiveComplete;

			CoffeeMug.OnMugFound -= OnObjectiveComplete;
		} 
	}
}
