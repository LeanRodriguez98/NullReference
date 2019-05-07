using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {

    public static Player instance;
    public GameObject GrabbPoint;
    public Collider mainCollider;
    public Portable playerPortable;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        fpc = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
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

    public void AutoWalk(float disableTime)
    {
        fpc.SetAutoWalk();
        fpc.Invoke("SetAutoWalk", disableTime);
    }

}
