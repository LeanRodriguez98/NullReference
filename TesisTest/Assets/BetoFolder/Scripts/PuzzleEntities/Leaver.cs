using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		public List<DoorConnection> m_doorConnections;

		private Animator m_animator;
		private bool stickLeaningFoward;

        private AudioSource audioClip;

		public override void Start()
		{
			base.Start();
			m_animator = GetComponent<Animator>();
			stickLeaningFoward = false;
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= PlayerPrefs.GetFloat("VolumeLevel");
        }

		public override void Interact()
		{
			base.Interact();

			m_animator.SetTrigger("Interact");
			UpdateDoorConnectionsStates();
			stickLeaningFoward = !stickLeaningFoward;

            audioClip.Play();
        }

		private void UpdateDoorConnectionsStates()
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
			{
				bool currentDoorState = m_doorConnections[i].IsEnabled();
				m_doorConnections[i].SetIsEnabled(!currentDoorState);
			}
		}

		public void InteractOnCubeCollision(LeaverTriggerFlag.FlagSide flagSide)
		{
			if (flagSide == LeaverTriggerFlag.FlagSide.Back && !stickLeaningFoward ||
				flagSide == LeaverTriggerFlag.FlagSide.Front && stickLeaningFoward)
			{
				Interact();
			}
		}
	}
}
