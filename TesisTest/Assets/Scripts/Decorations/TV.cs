using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    [System.Serializable]
    public struct BethweenTime
    {
        [Range(0.0f, 120.0f)] public float minTime;
        [Range(0.0f, 120.0f)] public float maxTime;
    }
    public List<Material> materials;
    public bool randomStartMaterial;
    public BethweenTime changeTime;
    private MeshRenderer my_Renderer;

    void Start()
    {
        my_Renderer = GetComponent<MeshRenderer>();
        if (changeTime.minTime > changeTime.maxTime)
            changeTime.minTime = changeTime.maxTime;
        if (randomStartMaterial)
            ChangeMaterial();
        else
            Invoke("ChangeMaterial", Random.Range(changeTime.minTime, changeTime.maxTime));
    }

    public void ChangeMaterial()
    {
        List<Material> m = new List<Material>();
        foreach (Material mat in materials)
            if (mat.mainTexture.name != my_Renderer.material.mainTexture.name)
                m.Add(mat);
        my_Renderer.material = m[Random.Range(0, m.Count)];
        Invoke("ChangeMaterial", Random.Range(changeTime.minTime, changeTime.maxTime));
    }

}
