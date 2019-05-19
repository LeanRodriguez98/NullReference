using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BetoScripts
{
	public class DoorConnection : MonoBehaviour
	{
		public PuzzleDoor m_puzzleDoor;
		public Material m_enabledColor;
		public Material m_disabledColor;

		[SerializeField]
		private ConnectionType m_connectionType;
		public enum ConnectionType { Pressure_Plate, Leaver }

		[SerializeField]
		private bool m_isEnabled;

		private MeshRenderer m_meshRenderer;

		void Start()
		{
			m_meshRenderer = GetComponent<MeshRenderer>();
			if (m_isEnabled)
				m_meshRenderer.material = m_enabledColor;
			else
				m_meshRenderer.material = m_disabledColor;
		}

		public void SetIsEnabled(bool isEnabled)
		{
			m_isEnabled = isEnabled;

			if (m_isEnabled)
				m_meshRenderer.material = m_enabledColor;
			else
				m_meshRenderer.material = m_disabledColor;

			m_puzzleDoor.UpdateState();
		}

		public bool IsEnabled()
		{
			return m_isEnabled;
		}

		public ConnectionType GetConnectionType()
		{
			return m_connectionType;
		}
	}
}
