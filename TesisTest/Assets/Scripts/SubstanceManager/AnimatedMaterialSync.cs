using UnityEngine;
using Substance.Game;

public class AnimatedMaterialSync : MonoBehaviour {

    [System.Serializable]
    public struct TimeValues
    {
        public SubstanceGraph substanceGraph;
        public float speed;
        public string updateValueName;
    }
    public TimeValues[] substanceGraphsToUpdate;

	void Update () {
        for (int i = 0; i < substanceGraphsToUpdate.Length; i++)
        {
            substanceGraphsToUpdate[i].substanceGraph.SetInputFloat(substanceGraphsToUpdate[i].updateValueName, Time.timeSinceLevelLoad * substanceGraphsToUpdate[i].speed);
            substanceGraphsToUpdate[i].substanceGraph.QueueForRender();
        }
        Substance.Game.Substance.RenderSubstancesSync();
    }
}
