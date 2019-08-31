using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwaper : MonoBehaviour {

    [HideInInspector] [SerializeField] private MeshRenderer meshRenderer;
    [HideInInspector] [SerializeField] private Material originalMaterial;
    [HideInInspector] [SerializeField] private Material material;
    private bool swaped = false;
    public bool Swaped
    {
        get { return swaped; }
    }
    public void LoadRenderer() 
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.sharedMaterial;
    }

    public void SetSwapMaterial(Material _material)
    {
        material = _material;
    }

    public void SetOriginalMaterial(Material _originalMaterial)
    {
        originalMaterial = _originalMaterial;
    }

    public void Swap()
    {
        if (swaped)
        {
            meshRenderer.sharedMaterial = originalMaterial;
        }
        else
        {
            meshRenderer.sharedMaterial = material;
        }
        swaped = !swaped;
    }

    void Update()
    {
        if (swaped)
        {
            if (meshRenderer.sharedMaterial != material)
            {
                meshRenderer.sharedMaterial = material;
            }
        }
    }

}
