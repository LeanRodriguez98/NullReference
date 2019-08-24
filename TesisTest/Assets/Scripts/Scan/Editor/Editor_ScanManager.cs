using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScanManager))]
public class Editor_ScanManager : Editor {

    public override void OnInspectorGUI()
    {

        ScanManager scanManager = (ScanManager)target;
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Meshes", GUILayout.Height(30)))
        {
            scanManager.LoadRenderers();
        }
        if (GUILayout.Button("Remove Meshes", GUILayout.Height(30)))
        {
            scanManager.RemoveMeshes();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Material Swaper", GUILayout.Height(30)))
        {
            scanManager.AddMaterialSwaper();
        }
        if (GUILayout.Button("Remove Material Swaper", GUILayout.Height(30)))
        {
            scanManager.RemoveMaterialSwaper();
        }
        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);
  
        if (GUILayout.Button("Reload All", GUILayout.Height(80)))
        {
            scanManager.LoadRenderers();
            scanManager.AddMaterialSwaper();
        }

        GUILayout.Space(15);


        base.OnInspectorGUI();

    }
}
