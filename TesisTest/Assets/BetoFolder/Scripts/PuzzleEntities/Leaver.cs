﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		public List<DoorConnection> m_doorConnections;
        
		private AudioSource audioClip;
		private Animator animator;
		private ParticleSystem particles;
		private bool stickLeaningLeft;
		
		public override void Start()
		{
			base.Start();
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");
			
			animator = GetComponent<Animator>();
			particles = GetComponentInChildren<ParticleSystem>();
			stickLeaningLeft = true;
        }

		public override void Interact()
		{
			base.Interact();

			UpdateDoorConnectionsStates();
            audioClip.Play();
			animator.SetTrigger("Interact");
        }

		private void UpdateDoorConnectionsStates()
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
			{
				bool currentDoorState = m_doorConnections[i].IsEnabled();
				m_doorConnections[i].SetIsEnabled(!currentDoorState);
			}
			animator.ResetTrigger("Interact");
		}

		public void OnActivatedByTrigger(LeaverTrigger.LeaverSide leaverSide)
		{
			if (stickLeaningLeft && leaverSide == LeaverTrigger.LeaverSide.Left)
			{
				Interact();
				stickLeaningLeft = false;
			}
			else if (!stickLeaningLeft && leaverSide == LeaverTrigger.LeaverSide.Right)
			{
				Interact();
				stickLeaningLeft = true;
			}
		}
	}
}
