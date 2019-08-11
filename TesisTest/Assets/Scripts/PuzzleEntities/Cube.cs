using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public bool isGrabbed = false;

    public void SetIsGrabbed(bool state)
    {
        isGrabbed = state;
    }
}
