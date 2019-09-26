using UnityEngine;
using Substance.Game;
public class SetGraphsValues : MonoBehaviour
{

    [System.Serializable]
    public struct Value
    {
        public SubstanceGraph substanceGraph;
        public float value;
        public string updateValueName;
    }

    public Value[] subtanceToSet;

    void Start()
    {
        SetGrahps();
    }
    public void SetGrahps()
    {
        for (int i = 0; i < subtanceToSet.Length; i++)
        {
            subtanceToSet[i].substanceGraph.SetInputFloat(subtanceToSet[i].updateValueName,subtanceToSet[i].value);
            subtanceToSet[i].substanceGraph.QueueForRender();
        }
        Substance.Game.Substance.RenderSubstancesSync();
    }
}
