using UnityEngine;

public class UI_EndScreen : MonoBehaviour
{
    public string sceneName;

	public void GoToMainMenu()
	{
        Utilities.LoadScene(sceneName);
	}
}
