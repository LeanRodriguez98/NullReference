using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScanManager))]
public class Editor_ScanManager : Editor {

    public override void OnInspectorGUI()
    {

        ScanManager scanManager = (ScanManager)target;

        if (GUILayout.Button("Load Meshes"))
        {
            scanManager.LoadRenderers();
        }

        if (GUILayout.Button("Remove Meshes"))
        {
            scanManager.RemoveMeshes();
        }

        if (GUILayout.Button("Add Material Swaper"))
        {
            scanManager.AddMaterialSwaper();
        }

        if (GUILayout.Button("Remove Material Swaper"))
        {
            scanManager.RemoveMaterialSwaper();
        }

        GUILayout.Space(20);
  
        if (GUILayout.Button("Reload All", GUILayout.Height(100)))
        {
            scanManager.LoadRenderers();
            scanManager.AddMaterialSwaper();
        }

        GUILayout.Space(15);


        base.OnInspectorGUI();

    }
}
