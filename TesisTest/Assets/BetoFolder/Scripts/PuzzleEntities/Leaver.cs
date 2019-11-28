﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		[SerializeField] GameObject leaverTrigger_1;
		[SerializeField] GameObject leaverTrigger_2;

		public List<DoorConnection> m_doorConnections;
        
		private AudioSource audioClip;
		private Animator animator;
		private ParticleSystem particles;
		
		public override void Start()
		{
			base.Start();
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");
			
			animator = GetComponent<Animator>();
			particles = GetComponentInChildren<ParticleSystem>();
        }

		public override void Interact()
		{
			base.Interact();

			UpdateDoorConnectionsStates();
            audioClip.Play();
			animator.SetTrigger("Interact");

			leaverTrigger_1.SetActive(!leaverTrigger_1.activeInHierarchy);
			leaverTrigger_2.SetActive(!leaverTrigger_2.activeInHierarchy);
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
	}
}
