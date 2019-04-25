using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PortalCallFunction))]
public class Editor_PortalCallFunction : Editor {
    private PortalCallFunction callFunction;
    private SerializedObject so_PortalCallFunction;

    private SerializedProperty CalledFunctions;
    private void OnEnable()
    {
        callFunction = (PortalCallFunction)target;
        so_PortalCallFunction = new SerializedObject(target);
        CalledFunctions = so_PortalCallFunction.FindProperty("methodsToCall");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
       /* so_PortalCallFunction.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(CalledFunctions,true);
        so_PortalCallFunction.ApplyModifiedProperties();*/
       /*  if (GUILayout.Button("Find Methods"))
        {
            callFunction.FindMethods();
        }*/
    }

}


