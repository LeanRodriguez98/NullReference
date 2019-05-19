using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PressurePlate : PuzzleDoorTrigger
	{
		public List<PuzzleDoor> m_puzzleDoors;
		public Animator m_animator;

		private void OnTriggerStay(Collider other)
		{
			IsBeingPressed(true);
		}

		private void OnTriggerExit(Collider other)
		{
			IsBeingPressed(false);
		}

		private void IsBeingPressed(bool isBeingPressed)
		{
			m_animator.SetBool("isPressed", isBeingPressed);
			m_isEnabled = isBeingPressed;
			UpdateDoorsStates();
		}

		void UpdateDoorsStates()
		{
			for (int i = 0; i < m_puzzleDoors.Count; i++)
				m_puzzleDoors[i].UpdateState();
		}
	}
}

