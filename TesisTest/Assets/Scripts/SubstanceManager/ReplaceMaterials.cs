using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceMaterials : MonoBehaviour
{
    public Material originalMaterial;
    public Material materialToReplace;

    public void Replace()
    {
        if (originalMaterial != null && materialToReplace != null)
        {
            GameObject[] all = Utilities.GetAllObjectsInScene().ToArray();
            for (int i = 0; i < all.Length; i++)
            {
                if (all[i].GetComponent<Renderer>() != null)
                {
                    if (all[i].GetComponent<Renderer>().sharedMaterial == originalMaterial)
                    {
                        all[i].GetComponent<Renderer>().sharedMaterial = materialToReplace;
                    }
                }
            }
        }
    }
}
