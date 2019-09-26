using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetGraphsValues))]

public class Editor_SetGraphsValues : Editor
{
    public override void OnInspectorGUI()
    {
        SetGraphsValues graphs = (SetGraphsValues)target;
        if (GUILayout.Button("Set Graphs"))
        {
            graphs.SetGrahps();
        }
        base.OnInspectorGUI();
    }
}
