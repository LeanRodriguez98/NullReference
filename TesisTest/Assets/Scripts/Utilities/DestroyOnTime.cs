using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, Time.fixedDeltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
