using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCaller : MonoBehaviour
{
	public Animator animator;
	public float timeToCallTrain = 5;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			Invoke("CallTrain", timeToCallTrain);
	}

	void CallTrain()
	{
		animator.SetTrigger("PlayerArrived");
	}
}
