using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public string sceneToLoadName;
    public bool LockCursor = false;
    // Use this for initialization
    void Start () {
		
        Utilities.LoadScene(sceneToLoadName);
        if (LockCursor)
        {
            Cursor.visible = false;
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
