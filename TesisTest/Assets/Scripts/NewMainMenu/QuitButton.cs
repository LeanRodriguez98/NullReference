using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
    public class QuitButton : MenuButton
    {
        public float delay;
        private bool clicked = false;
        public AudioClip overClip;
        public AudioClip clickedClip;


       

        private void OnMouseEnter()
        {
                audioSource.clip = overClip;
                audioSource.Play();
          
        }

       
        void OnMouseDown()
        {
            if (!clicked)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = clickedClip;
                    audioSource.Play();
                }
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
