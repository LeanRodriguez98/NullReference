using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ScanManager : MonoBehaviour {
    
    [SerializeField] private List<MeshRenderer> scaneRenderers = new List<MeshRenderer>();
    [SerializeField] private List<MaterialSwaper> materialSwapers = new List<MaterialSwaper>();
    [Space(20)]
    public Material scanMaterial;
    [Space(5)]
    public KeyCode SwapKey = KeyCode.Z;
    [Space(5)]
    public string[] excludedLayers;
    public void LoadRenderers()
    {
        RemoveMeshes();
        GameObject[] go = GetAllObjectsInScene().ToArray();
        for (int i = 0; i < go.Length; i++)
        {
            bool excludedLayer = false;

            for (int j = 0; j < excludedLayers.Length; j++)
            {
                if (go[i].layer == LayerMask.NameToLayer(excludedLayers[j])) //  excludedLayers[j])
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
            materialSwapers.Add(ms);
        }
    }

    public void RemoveMaterialSwaper()
    {
        GameObject[] go = GetAllObjectsInScene().ToArray();
        for (int i = 0; i < go.Length; i++)
        {
            if (go[i].GetComponent<MaterialSwaper>() != null)
            {
                DestroyImmediate(go[i].GetComponent<MaterialSwaper>());

            }
        }
        materialSwapers.Clear();
    }

    private List<GameObject> GetAllObjectsInScene()
    {
        List<GameObject> objectsInScene = new List<GameObject>();

        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
        {
            if (go.hideFlags != HideFlags.None)
                continue;

            if (PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab)
                continue;

            objectsInScene.Add(go);
        }
        return objectsInScene;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
    }


    private void Update()
    {
        if (Input.GetKeyDown(SwapKey))
        {
            foreach (MaterialSwaper ms in materialSwapers)
            {
                ms.Swap();
            }
        }
    }
}
