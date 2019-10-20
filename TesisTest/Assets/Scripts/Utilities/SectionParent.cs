using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SectionParent : MonoBehaviour
{

#if UNITY_EDITOR

    private void OnEnable()
    {
        if (!Application.isPlaying)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>(true))
            {
                t.gameObject.SetActive(true);
            }
        }
    }

    private void OnDisable()
    {
        if (!Application.isPlaying)
        {
            foreach (Transform t in GetComponentsInChildren<Transform>(true))
            {
                t.gameObject.SetActive(false);
            }
        }
    }
#endif
}
