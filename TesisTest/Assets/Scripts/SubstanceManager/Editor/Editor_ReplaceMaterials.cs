using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ReplaceMaterials))]
public class Editor_ReplaceMaterials : Editor
{
    public override void OnInspectorGUI()
    {
        ReplaceMaterials replaceMaterials = (ReplaceMaterials)target;
        if (GUILayout.Button("Replace"))
        {
            replaceMaterials.Replace();
        }
        base.OnInspectorGUI();
    }
}
