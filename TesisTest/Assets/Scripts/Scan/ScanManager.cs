using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ScanManager : MonoBehaviour
{

    [SerializeField] private List<MeshRenderer> scaneRenderers = new List<MeshRenderer>();
    [SerializeField] private List<MaterialSwaper> materialSwapers = new List<MaterialSwaper>();
    [Space(20)]
    public Material scanMaterial;
    public Material InteractMaterial;
    [Space(5)]
    public KeyCode SwapKey = KeyCode.Z;
    [Space(5)]
    public string[] scanExcludedLayers;
    public string[] interactLayers;

    public void LoadRenderers()
    {
        RemoveMeshes();
        GameObject[] go = Utilities.GetAllObjectsInScene().ToArray();
        for (int i = 0; i < go.Length; i++)
        {
            bool excludedLayer = false;

            for (int j = 0; j < scanExcludedLayers.Length; j++)
            {
                if (go[i].layer == LayerMask.NameToLayer(scanExcludedLayers[j])) //  excludedLayers[j])
                {
                    excludedLayer = true;
                }
            }

            if (go[i].GetComponent<MeshRenderer>() != null && !excludedLayer)
            {
                scaneRenderers.Add(go[i].GetComponent<MeshRenderer>());
            }
        }
    }

    public void RemoveMeshes()
    {
        scaneRenderers.Clear();
    }
    public void AddMaterialSwaper()
    {
        RemoveMaterialSwaper();
        foreach (MeshRenderer go in scaneRenderers)
        {
            go.gameObject.AddComponent<MaterialSwaper>();
            MaterialSwaper ms = go.gameObject.GetComponent<MaterialSwaper>();
            ms.LoadRenderer();

            ms.SetSwapMaterial(scanMaterial);

            for (int j = 0; j < interactLayers.Length; j++)
            {
                if (go.gameObject.layer == LayerMask.NameToLayer(interactLayers[j]))
                {
                    ms.SetSwapMaterial(InteractMaterial);
                }
            }


            materialSwapers.Add(ms);
        }
    }

    public void RemoveMaterialSwaper()
    {
        GameObject[] go = Utilities.GetAllObjectsInScene().ToArray();
        for (int i = 0; i < go.Length; i++)
        {
            if (go[i].GetComponent<MaterialSwaper>() != null)
            {
                DestroyImmediate(go[i].GetComponent<MaterialSwaper>());

            }
        }
        materialSwapers.Clear();
    }

   


    private void Update()
    {
        if (Input.GetKeyDown(SwapKey))
        {
            foreach (MaterialSwaper ms in materialSwapers)
            {
                if (ms.gameObject != null)
                {
                    ms.Swap();
                }
            }
        }
    }
}
