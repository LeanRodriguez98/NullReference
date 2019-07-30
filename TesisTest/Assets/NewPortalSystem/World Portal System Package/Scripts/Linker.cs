﻿using UnityEngine;
using System.Collections;

public class Linker : MonoBehaviour {

	public GameObject ListenFor;
	public GameObject Portal1;
	public GameObject Portal2;

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == ListenFor || !ListenFor)
		{
			Portal1.GetComponent<portal>().partner = Portal2;
			Portal2.GetComponent<portal>().partner = Portal1;
		}
	}
}
