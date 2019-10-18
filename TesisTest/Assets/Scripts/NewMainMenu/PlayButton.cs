using System.Collections;
using UnityEngine;
namespace NewMainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public float delay;
        public string sceneToLoadName;
        private bool clicked = false;
        private AudioSource audioSource;
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void OnMouseDown()
        {
            if (!clicked)
            {
                audioSource.Play();
                MainMenu.instace.OnButtonClicked();
                Invoke("LoadScene", delay);
                clicked = true;
            }
        }
        private void LoadScene()
        {
            Utilities.LoadScene(sceneToLoadName);
        }

    }
}

