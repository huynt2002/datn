using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class MainViewModel
{
    public MonoScript selectedScript;
    public Type scriptType;
    public ScriptableObject selectedItem;
    ItemPool itemPool;
    public List<ScriptableObject> items = new List<ScriptableObject>();
    public List<ScriptableObject> filterItems = new List<ScriptableObject>();

    public void CreateScriptableObject(System.Type type)
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
            selectedItem = instance;

            if (scriptType == typeof(ItemStats))
            {
                itemPool.items.Add((ItemStats)instance);
            }

            LoadAllItems();
        }
    }

    public void DeleteItem(ScriptableObject item)
    {
        itemPool.items.Remove((ItemStats)item);
        string path = AssetDatabase.GetAssetPath(item);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        selectedItem = null;
        Selection.activeObject = null;
        LoadAllItems();
    }


    public void LoadAllItems()
    {
        if (scriptType == null) { return; }
        items = AssetDatabase.FindAssets("t:" + scriptType.Name)
        .Select(guid => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid)))
        .ToList();
        filterItems = new List<ScriptableObject>(items);
        if (scriptType == typeof(ItemStats))
        {
            itemPool = AssetDatabase.FindAssets("t:ItemPool")
                     .Select(guid => AssetDatabase.LoadAssetAtPath<ItemPool>(AssetDatabase.GUIDToAssetPath(guid))).ToList()[0];

        }
    }

    public void SelectItem(ScriptableObject item)
    {
        selectedItem = item;
    }

    public void Search(string searchQuery)
    {
        if (!string.IsNullOrEmpty(searchQuery))
        {
            filterItems = items.Where(item => item.name.ToLower().Contains(searchQuery.ToLower())).ToList();
        }
        else
        {
            filterItems = new List<ScriptableObject>(items);
        }
    }
}
#endif