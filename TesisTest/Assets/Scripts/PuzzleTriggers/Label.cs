using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : PuzzleTrigger
{
    public GameObject labelPivot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "InteractPoint")
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UpdateLabel();

            }
        }
    }

    private void UpdateLabel()
    {
        IsTrigered = !IsTrigered;
        if (IsTrigered)
        {
            labelPivot.transform.Rotate(0, 0, -60);
        }
        else
        {
            labelPivot.transform.Rotate(0, 0, 60);
        }
        UpdateEntities();

    }

}
