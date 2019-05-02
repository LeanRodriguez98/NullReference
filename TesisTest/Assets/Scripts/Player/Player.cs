using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static Player instance;
    public GameObject GrabbPoint;


    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Utilities.ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Utilities.ExitGame();
        }
	}


}
