using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour {

    [HideInInspector] public bool IsTrigered;
    [HideInInspector] public List<PuzzleEntity> entitiesList;
    

    public void UpdateEntities()
    {
        for (int i = 0; i < entitiesList.Count; i++)
        {
            entitiesList[i].UpdateState();
        }
    }
}
 