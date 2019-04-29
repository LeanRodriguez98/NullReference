using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : PuzzleEntity {
    public GameObject door;
    public bool defaultState = false;
	new void Start () {
        base.Start();
        cantTriggers = 0;
        door.gameObject.SetActive(!defaultState);

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

            if (cantTriggers == triggerList.Count)
            {
                door.gameObject.SetActive(defaultState);
            }
            else
            {
                door.gameObject.SetActive(!defaultState);
            }
                        
            cantTriggers = 0;
        }
    }

}
