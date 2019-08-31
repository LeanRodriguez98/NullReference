using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(PresurePlatesMaterials))]
public class Editor_PresurePlatesMaterials : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PresurePlatesMaterials presurePlatesMaterials = (PresurePlatesMaterials)target;
        if (GUILayout.Button("Set Graphs"))
        {
            presurePlatesMaterials.SetGraphs();
        }
    }
}