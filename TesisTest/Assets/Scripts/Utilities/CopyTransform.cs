using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CopyTransform : MonoBehaviour {
    public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
        transform.localScale = target.transform.localScale;

      
    }
}
