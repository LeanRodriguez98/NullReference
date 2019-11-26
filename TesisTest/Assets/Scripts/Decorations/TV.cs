using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    [System.Serializable]
    public struct BethweenTime
    {
        [Range(1.0f, 120.0f)] public float minTime;
        [Range(1.0f, 120.0f)] public float maxTime;
    }
    public List<Material> materials;
    public bool randomStartMaterial;
    public BethweenTime changeTime;
    public GameObject noisePlane;
    [Range(0.2f,1.0f)] public float noiseDuration;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (changeTime.minTime > changeTime.maxTime)
            changeTime.minTime = changeTime.maxTime;
        noisePlane.SetActive(false);
        if (randomStartMaterial)
            ChangeMaterial();
        else
            Invoke("ChangeMaterial", Random.Range(changeTime.minTime, changeTime.maxTime));
    }

    public void ChangeMaterial()
    {
        List<Material> m = new List<Material>();
        foreach (Material mat in materials)
            if (mat.mainTexture.name != meshRenderer.material.mainTexture.name)
                m.Add(mat);
        meshRenderer.material = m[Random.Range(0, m.Count)];
        float time = Random.Range(changeTime.minTime, changeTime.maxTime);
        Invoke("ChangeMaterial", time);
        Invoke("DisplayNoiseOn", time - (noiseDuration / 2.0f));
    }

    public void DisplayNoiseOn()
    {
        noisePlane.SetActive(true);
        Invoke("DisplayNoiseOff",noiseDuration);
    }

    public void DisplayNoiseOff()
    {
        noisePlane.SetActive(false);
    }
}
