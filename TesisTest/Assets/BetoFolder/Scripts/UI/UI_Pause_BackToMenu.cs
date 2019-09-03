using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause_BackToMenu : MonoBehaviour {

    public void OnBackToMenu()
    {
        UI_Pause pause = GetComponentInParent<UI_Pause>();
        pause.ToMenu();
    }
}
