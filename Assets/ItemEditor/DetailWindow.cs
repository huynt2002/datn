using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class DetailWindow : EditorWindow
{
    private static string fileName;
    private Editor objectEditor;
    private static ScriptableObject selectedObject;
    public static void ShowWindow(ScriptableObject obj)
    {
        var window = GetWindow<DetailWindow>();
        window.titleContent = new GUIContent("ObjectDetail");
        selectedObject = obj;
        fileName = obj.name;
        window.Show();
    }

    private void OnGUI()
    {
        if (selectedObject != null)
        {
            fileName = EditorGUILayout.TextField("Asset name:", fileName);
            GUILayout.Label("Object Detail", EditorStyles.boldLabel);
            if (objectEditor == null || objectEditor.target != selectedObject)
                objectEditor = Editor.CreateEditor(selectedObject);

            // Draw the inspector for the ScriptableObject
            objectEditor.OnInspectorGUI();
        }

        if (GUILayout.Button("Save"))
        {
            RenameAsset();
            Close();
        }

        if (GUILayout.Button("Close"))
        {
            Close();
        }

    }
    private void RenameAsset()
    {
        if (string.IsNullOrEmpty(fileName))
        {
            Debug.LogError("New name cannot be empty.");
            return;
        }

        // Get the path of the selected object
        string assetPath = AssetDatabase.GetAssetPath(selectedObject);
        if (string.IsNullOrEmpty(assetPath))
        {
            Debug.LogError("Failed to find asset path.");
            return;
        }

        // Perform the rename
        string result = AssetDatabase.RenameAsset(assetPath, fileName);
        if (string.IsNullOrEmpty(result))
        {
            Debug.Log($"Successfully renamed asset to: {fileName}");
            AssetDatabase.SaveAssets();
        }
        else
        {
            Debug.LogError($"Failed to rename asset: {result}");
        }
    }
}


#endif
