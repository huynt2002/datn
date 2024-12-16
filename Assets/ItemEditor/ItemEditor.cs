using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ItemEditor : EditorWindow
{
    private MonoScript selectedScript;
    private Type scriptType;
    private Editor objectEditor;
    private ScriptableObject selectedItem;
    ItemPool itemPool;
    private List<ScriptableObject> items = new List<ScriptableObject>();
    private List<ScriptableObject> filterItems = new List<ScriptableObject>();
    string searchQuery = "";
    private Vector2 scrollPosition;

    [MenuItem("Tools/ScriptableEditor")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("Scriptable Editor");
    }

    private string newItemName = "New Object";
    private void OnGUI()
    {
        selectedScript = (MonoScript)EditorGUILayout.ObjectField(
            "Script",
            selectedScript,
            typeof(MonoScript),
            false);

        // Check if the selected script is valid
        if (selectedScript != null)
        {
            scriptType = selectedScript.GetClass();

            if (scriptType == null || !scriptType.IsSubclassOf(typeof(ScriptableObject)) || scriptType.IsAbstract)
            {
                EditorGUILayout.HelpBox(
                    "The selected script must be a non-abstract class that inherits from ScriptableObject.",
                    MessageType.Error);
            }
            else
            {
                if (GUILayout.Button("Create Item"))
                {
                    CreateScriptableObject(scriptType);
                }
            }
            // GUILayout.Label("Object Editor", EditorStyles.boldLabel);
            // selectedItem = (ScriptableObject)EditorGUILayout.ObjectField("Selected Object", selectedItem, scriptType, false);
            // if (selectedItem != null)
            // {
            //     GUILayout.Label("Object Editor", EditorStyles.boldLabel);
            //     if (objectEditor == null || objectEditor.target != selectedItem)
            //         objectEditor = Editor.CreateEditor(selectedItem);

            //     // Draw the inspector for the ScriptableObject
            //     objectEditor.OnInspectorGUI();
            // }

            if (GUILayout.Button("Refresh Objects"))
            {
                LoadAllItems();
            }
            searchQuery = EditorGUILayout.TextField("Search:", searchQuery);
            if (!string.IsNullOrEmpty(searchQuery))
            {
                filterItems = items.Where(item => item.name.ToLower().Contains(searchQuery.ToLower())).ToList();
            }
            else
            {
                filterItems = new List<ScriptableObject>(items);
            }
            ObjectListView(filterItems);
            GUILayout.Label("Count: " + items.Count);
        }
    }

    void ObjectListView(List<ScriptableObject> items)
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        foreach (var item in items)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(item.name);
            if (GUILayout.Button("Select", GUILayout.Width(60)))
            {
                Selection.activeObject = item;
                selectedItem = item;
            }

            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                DeleteItem(item);
                break;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
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
            selectedItem = instance;

            if (scriptType == typeof(ItemStats))
            {
                itemPool.items.Add((ItemStats)instance);
            }
        }
    }

    private void LoadAllItems()
    {

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

    private void DeleteItem(ScriptableObject item)
    {
        itemPool.items.Remove((ItemStats)item);

        string path = AssetDatabase.GetAssetPath(item);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        selectedItem = null;
        Selection.activeObject = null;
        LoadAllItems();
    }

}
#endif