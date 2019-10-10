using System.Collections;
using UnityEngine;
namespace NewMainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public float delay;
        public string sceneToLoadName;
        private bool clicked = false;
        void OnMouseDown()
        {
            if (!clicked)
            {
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

