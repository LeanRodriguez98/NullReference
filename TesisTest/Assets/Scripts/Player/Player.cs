using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour {

    public static Player instance;
    public GameObject GrabbPoint;
    public Collider mainCollider;
    public Portable playerPortable;

	private Animator animator;

    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;
    private void Awake()
    {
        instance = this;
    }

    void Start () {
        fpc = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Utilities.ReloadScene();
        }

        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    Utilities.ExitGame();
        //}
	}

	public void EnableOnAnimation()
	{
		fpc.SetOnAnimation(true);
	}

	public void DisableOnAnimation()
	{
		fpc.SetOnAnimation(false);
	}

	public void DisableAnimator()
	{
		animator.enabled = false;
	}

    public void AutoWalk(float disableTime)
    {
        fpc.SetAutoWalk();
        fpc.Invoke("SetAutoWalk", disableTime);
    }

}
