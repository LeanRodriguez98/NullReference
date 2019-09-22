using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace NewMainMenu
{
    public class PlayButton : MonoBehaviour
    {
        public string sceneToLoadName;
        private bool clicked = false;
        void OnMouseDown()
        {
            if (!clicked)
            {
                MainMenu.instace.OnButtonClicked();
                StartCoroutine(LoadAsyncScene());
                clicked = true;
            }
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
