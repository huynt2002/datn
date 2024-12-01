using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class ScriptEditor : EditorWindow
{
    private string className = "NewScriptableObject";
    private string folderPath = "Assets/ScriptableObjects"; // Default folder for saving script
    private string scriptContent = "";
    [MenuItem("Tools/ScriptableObject Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<ScriptEditor>("Script Gen");
    }

    private void OnGUI()
    {
        GUILayout.Label("ScriptableObject Class Generator", EditorStyles.boldLabel);

        className = EditorGUILayout.TextField("Class Name", className);
        folderPath = EditorGUILayout.TextField("Folder Path", folderPath);

        if (GUILayout.Button("Generate Script"))
        {
            GenerateScript();
        }
    }

    private void GenerateScript()
    {
        if (string.IsNullOrEmpty(className) || string.IsNullOrEmpty(folderPath))
        {
            Debug.LogError("Class name or folder path cannot be empty.");
            return;
        }

        // Create folder if it doesn't exist
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string scriptFilePath = Path.Combine(folderPath, className + ".cs");

        // Check if the file already exists
        if (File.Exists(scriptFilePath))
        {
            Debug.LogWarning("Script file already exists. Skipping generation.");
            return;
        }

        // Define the content of the ScriptableObject class
        scriptContent = GenerateScriptContent();

        // Write the content to the file
        File.WriteAllText(scriptFilePath, scriptContent);

        // Refresh the asset database to reflect the new file
        AssetDatabase.Refresh();

        Debug.Log($"Script {className} generated successfully at {scriptFilePath}");
    }

    private string GenerateScriptContent()
    {
        return
            "using UnityEngine;\n\n" +
            "public class " + className + " : ScriptableObject\n" +
            "{\n" +
            "    // Add your fields here\n" +
            "    // Example: public int someValue;\n\n" +
            "    // Add initialization or custom methods here\n" +
            "}";
    }
}
#endif
