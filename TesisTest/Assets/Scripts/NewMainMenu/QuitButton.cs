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
        private bool canPlayAudio = true;
        public AudioClip overClip;
        public AudioClip clickedClip;


        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseOver()
        {
            if (!audioSource.isPlaying && canPlayAudio)
            {
                audioSource.clip = overClip;
                audioSource.Play();
                canPlayAudio = false;
            }

        }
        private void OnMouseExit()
        {
            canPlayAudio = true;
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
