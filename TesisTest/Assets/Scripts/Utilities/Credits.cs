using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public string mainMenuSceneName;
    public KeyCode backToMainMenuKey;

    public void AE_BackToMainMenu()
    {
        Utilities.LoadScene(mainMenuSceneName);
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(backToMainMenuKey))
        {
            AE_BackToMainMenu();
        }
    }
}
