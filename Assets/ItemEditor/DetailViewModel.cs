using UnityEditor;
using UnityEngine;
public class DetailViewModel
{
    public ScriptableObject selectedObject;

    public void SaveData(string fileName)
    {
        if (!selectedObject)
        {
            return;
        }
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
