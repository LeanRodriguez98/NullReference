using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEntity : MonoBehaviour {

    public List<PuzzleTrigger> triggerList;
    [HideInInspector] public int cantTriggers;

    public virtual void Start()
    {
        for (int i = 0; i < triggerList.Count; i++)
        {
            triggerList[i].entitiesList.Add(this);
        }
    }
    public virtual void UpdateState() { }
}
