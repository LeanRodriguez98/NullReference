using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause_Resume : MonoBehaviour {

    public void OnResume()
    {
        UI_Pause pause= GetComponentInParent<UI_Pause>();
        pause.Resume();
    }
}
