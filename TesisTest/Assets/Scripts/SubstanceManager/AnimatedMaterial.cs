using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Substance.Game;

public class AnimatedMaterial : MonoBehaviour
{
	[System.Serializable]
	public struct TimeValues
	{
		public SubstanceGraph substanceGraph;
		public float speed;
		public string updateValueName;
	}
    [Range(1, 10)] public uint renderUpdateTime = 1;
    private uint auxRenderUpdateTime = 0;
    public TimeValues[] substanceGraphsToUpdate;

   
	public void Start()
	{
        StartCoroutine(UpdateMaterials());
    }

    

    IEnumerator UpdateMaterials()
    {
        for (int i = 0; i < substanceGraphsToUpdate.Length; i++)
        {
            substanceGraphsToUpdate[i].substanceGraph.SetInputFloat(substanceGraphsToUpdate[i].updateValueName, substanceGraphsToUpdate[i].substanceGraph.GetInputFloat(substanceGraphsToUpdate[i].updateValueName) + (Time.deltaTime * substanceGraphsToUpdate[i].speed));
            substanceGraphsToUpdate[i].substanceGraph.QueueForRender();


             auxRenderUpdateTime++;
             if (auxRenderUpdateTime >= renderUpdateTime)
             {
                yield return null;
                auxRenderUpdateTime = 0;
             }
        }

        Substance.Game.Substance.RenderSubstancesAsync();

        StartCoroutine(UpdateMaterials());
    }



}
