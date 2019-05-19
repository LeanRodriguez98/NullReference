using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PuzzleDoor : MonoBehaviour
	{
		public List<PuzzleDoorTrigger> m_doorTriggers;

		private Animator m_animator;

		private void Start()
		{
			m_animator = GetComponent<Animator>();
		}

		public void UpdateState()
		{
			m_animator.SetBool("AllTriggersEnabled", AllTriggersEnabled());
		}

		private bool AllTriggersEnabled()
		{
			for (int i = 0; i < m_doorTriggers.Count; i++)
				if (m_doorTriggers[i].GetIsEnabled() == false)
					return false;
			return true;
		}
	}
}
