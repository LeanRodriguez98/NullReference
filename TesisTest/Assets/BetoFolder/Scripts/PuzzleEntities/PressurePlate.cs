using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PressurePlate : MonoBehaviour
	{
		public Animator m_animator;

		private void OnTriggerStay(Collider other)
		{
			Debug.Log("Plate pressed");
			m_animator.SetBool("isPressed", true);
		}

		private void OnTriggerExit(Collider other)
		{
			Debug.Log("Plate unpressed");
			m_animator.SetBool("isPressed", false);
		}
	}
}

