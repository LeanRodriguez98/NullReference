﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_EndScreen : MonoBehaviour
{
	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}
}
