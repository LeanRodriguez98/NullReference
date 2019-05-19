using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
	public List<GameObject> m_objectsToEnable;

	private bool m_playerIsOnTrigger;

	private void Start()
	{
		m_playerIsOnTrigger = false;
	}

	void Update ()
	{
		if (m_playerIsOnTrigger)
			for (int i = 0; i < m_objectsToEnable.Count; i++)
				m_objectsToEnable[i].SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			m_playerIsOnTrigger = true;
	}
}
