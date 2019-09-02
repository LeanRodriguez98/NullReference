using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PressurePlate : MonoBehaviour
	{
		public List<DoorConnection> m_doorConnections;
		public Animator m_animator;
        private Cube activatorCube;
        [HideInInspector] public AnimatedMaterial.TimeValues timeValue;
        private AudioSource audioClip;
        [Range(0.0f, 0.48f)] public float openRadius;
        private void Start()
        {
            audioClip = GetComponent<AudioSource>();
            audioClip.volume *= GameManager.GetInstance().gameOptions.soundsVolume; //PlayerPrefs.GetFloat("VolumeLevel");
        }

        public void SetAnimatedMaterial( AnimatedMaterial.TimeValues _animatedMaterial)
        {
            MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].material = _animatedMaterial.substanceGraph.material;
            }
            timeValue = _animatedMaterial;
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

            if (timeValue.substanceGraph != null)
            {
                timeValue.substanceGraph.SetInputFloat(timeValue.updateValueName, Time.timeSinceLevelLoad * timeValue.speed);
                timeValue.substanceGraph.SetInputFloat("Cube_on", openRadius);
                timeValue.substanceGraph.QueueForRender();
                Substance.Game.Substance.RenderSubstancesSync();
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
            if (activatorCube != null)
            {
                if (other.gameObject == activatorCube.gameObject)
                {
                    activatorCube = null;
                }
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

