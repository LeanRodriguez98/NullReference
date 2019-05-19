using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class ObjectDisabler : MonoBehaviour
	{
		public float m_timeToDisable = 0;
		public List<GameObject> m_objectsToDisable;

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
				Invoke("DisableObjects", m_timeToDisable);
		}

		private void DisableObjects()
		{
			for (int i = 0; i < m_objectsToDisable.Count; i++)
				m_objectsToDisable[i].SetActive(false);
		}
	}
}
