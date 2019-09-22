using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
            StartCoroutine(LoadAsyncScene());
        }
        private IEnumerator LoadAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoadName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
