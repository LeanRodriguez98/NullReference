using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : PuzzleTrigger
{

	void Start () {
		
	}
	
	void Update () {

  	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PuzzleObject" || other.gameObject.tag == "Player")
        {
            if (!IsTrigered)
            {
                IsTrigered = true;
                UpdateEntities();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PuzzleObject" || other.gameObject.tag == "Player")
        {
            IsTrigered = false;
            UpdateEntities();
        }
    }

 
}
