using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using UnityEditor;

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
        #region LoadScene

        public static void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
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
        #region SaveGame

        private static bool IsSavedFile()
        {
            return Directory.Exists(Application.persistentDataPath + "/game_save");
        }

        public static void SaveGame(ScriptableObject soToSave)
        {
            if (!IsSavedFile())
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
            }
            if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data");
            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/" + soToSave.name + "_data/" + soToSave.name + "_save.txt");
            var json = JsonUtility.ToJson(soToSave);
            bf.Serialize(file, json);
            file.Close();
        }

        public static void LoadGame(ScriptableObject soToLoad)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data");
            }
            BinaryFormatter bf = new BinaryFormatter();
            if (File.Exists(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data/" + soToLoad.name + "_save.txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/game_save/" + soToLoad.name + "_data/" + soToLoad.name + "_save.txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), soToLoad);
                file.Close();
            }
        }

        #endregion


        public static List<GameObject> GetAllObjectsInScene()
        {
            List<GameObject> objectsInScene = new List<GameObject>();
#if UNITY_EDITOR

            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (go.hideFlags != HideFlags.None)
                    continue;

                if (PrefabUtility.GetPrefabType(go) == PrefabType.Prefab || PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefab)
                    continue;

                objectsInScene.Add(go);
            }
#endif

            return objectsInScene;
        }
    }
}