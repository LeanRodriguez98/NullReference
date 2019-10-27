using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjectEnabler : MonoBehaviour
{

    [Tooltip("To use this functionality, please use the \"Load Childs\" button")] public bool LoadAsync = false;
    [Range(1, 10)] public uint objectsEnabledPerFrame = 1;
    public bool m_enableOnAwake;
    public float m_enableAfterSeconds;
    public List<GameObject> m_objectsToEnable;

    private void Awake()
    {
        if (m_enableOnAwake)
            Invoke("EnableObjects", m_enableAfterSeconds);

    }

    public void LoadChilds()
    {
        
        List<GameObject> auxList = new List<GameObject>();
        for (int i = 0; i < m_objectsToEnable.Count; i++)
        {
            Transform[] t = m_objectsToEnable[i].GetComponentsInChildren<Transform>();
            foreach (Transform tr in t)
            {
                if (tr.gameObject.activeSelf)
                {
                    auxList.Add(tr.gameObject);
                }
            }
        }

        foreach (GameObject gameObject in auxList)
        {
            m_objectsToEnable.Add(gameObject);
        }

        for (int i = 0; i < m_objectsToEnable.Count; i++)
        {
            if (m_objectsToEnable[i].activeSelf)
            {
                m_objectsToEnable[i].SetActive(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Invoke("EnableObjects", m_enableAfterSeconds);
    }

    private void EnableObjects()
    {

        if (LoadAsync)
        {
            StartCoroutine(EnableObjectsAsync());
        }
        else
        {
            for (int i = 0; i < m_objectsToEnable.Count; i++)
            {
                m_objectsToEnable[i].SetActive(true);
            }
        }
    }

    IEnumerator EnableObjectsAsync()
    {
        for (int i = 0; i < m_objectsToEnable.Count; i++)
        {
            if (m_objectsToEnable[i] != null)
            {

                m_objectsToEnable[i].SetActive(true);
                //Debug.Log(Time.timeSinceLevelLoad.ToString() + ": " + m_objectsToEnable[i].name);
                if (i % objectsEnabledPerFrame == 0)
                {
                    yield return null;
                }
            }
        }

        Destroy(this);
    }
}
