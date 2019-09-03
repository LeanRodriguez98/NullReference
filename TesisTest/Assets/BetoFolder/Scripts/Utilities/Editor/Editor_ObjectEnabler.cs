using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(ObjectEnabler))]
public class Editor_ObjectEnabler : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectEnabler objectEnabler = (ObjectEnabler)target;
        if (GUILayout.Button("Load Childs"))
        {
            objectEnabler.LoadChilds();
        }
        base.OnInspectorGUI();
    }
}
