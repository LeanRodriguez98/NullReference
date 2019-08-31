using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class SubstancePlatform : MonoBehaviour {

   // public Substance.Game.Substance substance;
    public Substance.Game.SubstanceGraph graph;
    [Range(0.0f,1.0f)] public float radius;
    MeshRenderer[] meshes;
    // Use this for initialization
    void Start () {
        meshes = GetComponentsInChildren<MeshRenderer>();
       /* substance.DuplicateGraph(substance.graphs[0]);
        graph = substance.graphs[substance.graphs.Count - 1];
        substance.CommitMaterialToGraph(substance.graphs.Count - 1, new Material(substance.graphs[0].material));
        List<Texture2D> texs2D = new List<Texture2D>();
        foreach (Texture2D tex2D in substance.graphs[0].generatedTextures)
        {
            texs2D.Add(tex2D);
        }
        substance.CommitGeneratedTexturesToGraph(substance.graphs.Count - 1, texs2D);*/
        for (int i = 0; i < meshes.Length; i++)
        {
            meshes[i].material = graph.material;
        }


        Debug.Log(graph.material.name);
      
	}
	
	// Update is called once per frame
	void Update () {
        graph.SetInputFloat("Hue", radius);
        graph.QueueForRender();
         Substance.Game.Substance.RenderSubstancesSync();
    
    }
}
