using UnityEngine;
using UnityEditor;
namespace NewMainMenu
{
    [CustomEditor(typeof(MainMenu))]
    public class Editor_MainMenu : Editor
    {
        public override void OnInspectorGUI()
        {
            MainMenu mainMenu = (MainMenu)target;
            if (GUILayout.Button("SetShardsAnimations"))
            {
                mainMenu.SetMenuShardsAnimations();
            }
            if (GUILayout.Button("SetButtonsData"))
            {
                mainMenu.SetButtonsData();
            }

            base.OnInspectorGUI();
        }
    }
}