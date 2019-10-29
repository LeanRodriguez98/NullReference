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
        public AudioClip overClip;
        public AudioClip playClip;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnMouseEnter()
        {
            audioSource.clip = overClip;
            audioSource.Play();
        }

        void OnMouseDown()
        {
            if (!clicked)
            {
                audioSource.clip = playClip;
                audioSource.Play();
                MainMenu.instace.OnButtonClicked();
                Invoke("LoadScene", playClip.length + delay);
                clicked = true;
            }
        }
        private void LoadScene()
        {
            Utilities.LoadScene(sceneToLoadName);
        }

    }
}

