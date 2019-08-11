using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlate : PuzzleTrigger
{

    public Cube activatorCube;


	void Update () {
        if (activatorCube != null)
        {
            if (activatorCube.isGrabbed)
            {
                IsTrigered = false;
                activatorCube = null;
                UpdateEntities();
            }
    	}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PuzzleObject" || other.gameObject.tag == "Player")
        {
            if (!IsTrigered)
            {
                IsTrigered = true;
                activatorCube = other.GetComponent<Cube>();
                UpdateEntities();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PuzzleObject" || other.gameObject.tag == "Player")
        {
            IsTrigered = false;
            activatorCube = null;
            UpdateEntities();
        }
    }

 
}
