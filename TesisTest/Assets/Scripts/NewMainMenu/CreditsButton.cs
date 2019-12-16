using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NewMainMenu
{
    public class CreditsButton : MenuButton
    {

        public float delay;
        public string creditsSceneName;
        private bool clicked = false;


        private void OnEnable()
        {
            clicked = false;
        }

        private void OnMouseEnter()
        {
            audioSource.Play();
        }

        void OnMouseDown()
        {
            if (!clicked)
            {
                Invoke("ChangeToCredits", delay);
                clicked = true;
            }
        }

        private void ChangeToCredits()
        {
            Utilities.LoadScene(creditsSceneName);
        }
    }
}