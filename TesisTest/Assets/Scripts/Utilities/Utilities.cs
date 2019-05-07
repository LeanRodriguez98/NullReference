using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;

namespace UnityEngine
{
    public static class Utilities
    {
        #region ClearConsole
        public static void ClearConsole()
        {
            Type.GetType("UnityEditor.LogEntries,UnityEditor.dll")
                .GetMethod("Clear", BindingFlags.Static | BindingFlags.Public)
                .Invoke(null, null);
        }
        #endregion

        #region ReloadScene
        public static void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion

        #region ExitGame
        public static void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        #endregion

        #region ParametricInvoke
        public static void ParametricInvoke(this MonoBehaviour behaviour, string method, object options, float delay)
        {
            behaviour.StartCoroutine(_invoke(behaviour, method, delay, options));
        }

        private static IEnumerator _invoke(this MonoBehaviour behaviour, string method, float delay, object options)
        {
            if (delay > 0f)
            {
                yield return new WaitForSeconds(delay);
            }

            Type instance = behaviour.GetType();
            MethodInfo mthd = instance.GetMethod(method);
            mthd.Invoke(behaviour, new object[] { options });

            yield return null;
        }
        #endregion

    }
}