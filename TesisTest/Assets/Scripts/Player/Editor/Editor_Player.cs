using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Player))]
public class Editor_Player : Editor {
    public override void OnInspectorGUI()
    {
        Player player = (Player)target;
        EditorGUILayout.BeginHorizontal();
        player.resetPosition = (Transform)EditorGUILayout.ObjectField("Game Start Position", player.resetPosition, typeof(Transform), true);
        if (player.resetPosition != null)
        {
            if (GUILayout.Button("Reset Position"))
            {
                player.transform.position = player.resetPosition.position;
                player.transform.rotation = player.resetPosition.rotation;
            }
        }
        EditorGUILayout.EndHorizontal();

        player.GrabbPoint = (GameObject)EditorGUILayout.ObjectField("Grabber point", player.GrabbPoint, typeof(GameObject), true);
    }

}
