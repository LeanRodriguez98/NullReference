using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class ObjectDisabler : MonoBehaviour
	{
		public List<GameObject> m_objectsToDisable;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				DisableObjects();
		}

		private void DisableObjects()
		{
			for (int i = 0; i < m_objectsToDisable.Count; i++)
				m_objectsToDisable[i].SetActive(false);
		}
	}
}
