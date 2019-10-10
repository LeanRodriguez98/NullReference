using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string sceneToLoadName;
    // Use this for initialization
    void Start () {
		
        Utilities.LoadScene(sceneToLoadName);
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
