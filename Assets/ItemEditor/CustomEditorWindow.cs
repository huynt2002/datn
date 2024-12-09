using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreatorWindow : EditorWindow
{
    private MonoScript selectedScript;

    [MenuItem("Tools/Scriptable Object Creator")]
    public static void OpenWindow()
    {
        GetWindow<ScriptableObjectCreatorWindow>("Scriptable Object Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create a ScriptableObject", EditorStyles.boldLabel);

        selectedScript = (MonoScript)EditorGUILayout.ObjectField(
            "Script",
            selectedScript,
            typeof(MonoScript),
            false);

        // Check if the selected script is valid
        if (selectedScript != null)
        {
            var scriptType = selectedScript.GetClass();

            if (scriptType == null || !scriptType.IsSubclassOf(typeof(ScriptableObject)) || scriptType.IsAbstract)
            {
                EditorGUILayout.HelpBox(
                    "The selected script must be a non-abstract class that inherits from ScriptableObject.",
                    MessageType.Error);
            }
            else
            {
                if (GUILayout.Button("Create"))
                {
                    CreateScriptableObject(scriptType);
                }
            }
        }
        else
        {
            EditorGUILayout.HelpBox("Drag a ScriptableObject script to the field above.", MessageType.Info);
        }
    }

    private void CreateScriptableObject(System.Type type)
    {
        // Create an instance of the selected type
        ScriptableObject instance = ScriptableObject.CreateInstance(type);

        // Show save file dialog
        string path = EditorUtility.SaveFilePanelInProject(
            "Save ScriptableObject",
            type.Name + ".asset",
            "asset",
            "Choose a location to save the ScriptableObject"
        );

        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(instance, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = instance;
        }
    }
}
