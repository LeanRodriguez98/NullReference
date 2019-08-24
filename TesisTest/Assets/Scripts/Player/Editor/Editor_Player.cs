
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Player))]
public class Editor_Player : Editor {
    public Transform resetPosition;
    public override void OnInspectorGUI()
    {
        Player player = (Player)target;
        EditorGUILayout.BeginHorizontal();
        resetPosition = (Transform)EditorGUILayout.ObjectField("Game Start Position", resetPosition, typeof(Transform), true);
        if (resetPosition != null)
        {
            if (GUILayout.Button("Reset Position"))
            {
                player.ResetPosition(resetPosition);
            }
        }
        EditorGUILayout.EndHorizontal();

        player.GrabbPoint = (GameObject)EditorGUILayout.ObjectField("Grabber point", player.GrabbPoint, typeof(GameObject), true);
    }

}
