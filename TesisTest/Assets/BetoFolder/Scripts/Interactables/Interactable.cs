using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	public bool CanInteract { get; set; }

	private Shader standard;
	private Shader outline;

	public virtual void Start()
	{
		CanInteract = true;
	}
	public virtual void Interact() { }

}
