using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Pause : MonoBehaviour {

    public KeyCode PauseButton = KeyCode.P;
    public GameObject[] ObjectsToTurnOn;
    public GameObject[] ObjectsToTurnOff;

    void Start()
    {
        for (int i = 0; i < ObjectsToTurnOn.Length; i++)
        {
            ObjectsToTurnOn[i].SetActive(false);
        }
    }


    void Update () {
        if (Input.GetKeyDown(PauseButton))
        {
            if (Time.timeScale == 0)
            {
                SetPause(false);
            }
            else
            {
                SetPause(true);
            }
        }
	}

    private void SetPause(bool pauseState)
    {
        for (int i = 0; i < ObjectsToTurnOn.Length; i++)
        {
            ObjectsToTurnOn[i].SetActive(pauseState);
        }

        for (int i = 0; i < ObjectsToTurnOff.Length; i++)
        {
            ObjectsToTurnOff[i].SetActive(!pauseState);
        }

        Cursor.visible = pauseState;
        if (pauseState)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }

    //This funcions are called by animation events in "Resume Button Animation"

    public void Resume()
    {
        SetPause(false);
    }

    public void ToMenu()
    {
        Utilities.LoadScene("MainMenu");
    }

}
