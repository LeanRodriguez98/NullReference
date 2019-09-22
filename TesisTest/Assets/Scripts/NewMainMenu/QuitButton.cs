using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
    public class QuitButton : MonoBehaviour
    {
        public float delay;
        private bool clicked = false;
        void OnMouseDown()
        {
            if (!clicked)
            {
                MainMenu.instace.OnButtonClicked();
                Invoke("Exit", delay);
                clicked = true;
            }
        }

        private void Exit()
        {
            Utilities.ExitGame();
        }
    }
}
