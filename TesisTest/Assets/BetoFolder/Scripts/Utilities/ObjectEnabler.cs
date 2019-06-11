using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{
	public bool m_enableOnAwake;
	public float m_enableAfterSeconds;
	public List<GameObject> m_objectsToEnable;

	private void Awake()
	{
		if (m_enableOnAwake)
			Invoke("EnableObjects", m_enableAfterSeconds);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			Invoke("EnableObjects", m_enableAfterSeconds);
	}

	private void EnableObjects()
	{
		for (int i = 0; i < m_objectsToEnable.Count; i++)
			m_objectsToEnable[i].SetActive(true);
	}
}
