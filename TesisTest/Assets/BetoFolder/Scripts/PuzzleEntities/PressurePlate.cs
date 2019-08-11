using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PressurePlate : MonoBehaviour
	{
		public List<DoorConnection> m_doorConnections;
		public Animator m_animator;
        public Cube activatorCube;

        private AudioSource audioClip;

        private void Start()
        {
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= GameManager.GetInstance().gameOptions.volume; //PlayerPrefs.GetFloat("VolumeLevel");
        }


        void Update()
        {
            if (activatorCube != null)
            {
                if (activatorCube.isGrabbed)
                {
                    activatorCube = null;
                    IsBeingPressed(false);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            audioClip.Play();
            activatorCube = other.GetComponent<Cube>();

        }

        private void OnTriggerStay(Collider other)
		{
			IsBeingPressed(true);
		}

		private void OnTriggerExit(Collider other)
		{
			IsBeingPressed(false);
            if (other.gameObject == activatorCube.gameObject)
            {
                activatorCube = null;
            }
        }

        private void IsBeingPressed(bool isBeingPressed)
		{
			m_animator.SetBool("isPressed", isBeingPressed);
			EnableDoorConnections(isBeingPressed);
        }

		void EnableDoorConnections(bool enabled)
		{
			for (int i = 0; i < m_doorConnections.Count; i++)
				m_doorConnections[i].SetIsEnabled(enabled);
		}
	}
}

