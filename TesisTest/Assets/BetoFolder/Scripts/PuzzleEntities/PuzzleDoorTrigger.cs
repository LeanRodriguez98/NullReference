using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class PuzzleDoorTrigger : MonoBehaviour
	{
		protected bool m_isEnabled;

		void Start()
		{
			m_isEnabled = false;
		}

		public bool GetIsEnabled()
		{
			return m_isEnabled;
		}
	}
}
