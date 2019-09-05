using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class Leaver : Interactable
	{
		public List<DoorConnection> m_doorConnections;
        
		private AudioSource audioClip;
		private ParticleSystem particles;
		
		public override void Start()
		{
			base.Start();
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");

			particles = GetComponentInChildren<ParticleSystem>();
        }

		public override void Interact()
		{
			base.Interact();

			UpdateDoorConnectionsStates();
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

		private void OnTriggerEnter(Collider other) 
		{
			if (other.CompareTag("PickUpable"))
			{
				Interact();
				particles.Play();
			}
		}
	}
}
