using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMainMenu
{
    public class QuitButton : MonoBehaviour
    {
        public float delay;
        private bool clicked = false;
        private AudioSource audioSource;
        //private bool canPlayAudio = true;
        public AudioClip overClip;
        public AudioClip clickedClip;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseEnter()
        {
                audioSource.clip = overClip;
                audioSource.Play();
           // if (!audioSource.isPlaying && canPlayAudio)
           // {
           //     canPlayAudio = false;
           // }
            
        }

        private void OnMouseOver()
        {

        }
        private void OnMouseExit()
        {
            //canPlayAudio = true;
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
