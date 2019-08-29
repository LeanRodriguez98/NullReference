using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CopyTransform : MonoBehaviour {
    public GameObject target;

	private Camera playerCamera;
	private Camera myCamera;

	void Start () 
	{
		playerCamera = Camera.main;
		myCamera = GetComponent<Camera>();
	}
	
	void LateUpdate () 
	{
        transform.position = target.transform.position;
        transform.rotation = target.transform.rotation;
        transform.localScale = target.transform.localScale;

		myCamera.fieldOfView = playerCamera.fieldOfView;
    }
}
