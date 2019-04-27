using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PuzzleEntity {
    public GameObject door;
  
	new void Start () {
        base.Start();
        cantTriggers = 0;
    }
	
	void Update () {
     
    }

    public override void UpdateState()
    {
        if (triggerList != null)
        {
            for (int i = 0; i < triggerList.Count; i++)
            {
                if (triggerList[i].IsTrigered)
                {
                    cantTriggers++;
                }
            }
            if (cantTriggers % 2 == 0)
            {
                door.gameObject.SetActive(true);
            }
            else
            {
                door.gameObject.SetActive(false);
            }
            cantTriggers = 0;
        }
    }

}
