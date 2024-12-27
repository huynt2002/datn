using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class MainWindow : EditorWindow
{
    static MainViewModel controller;
    private Vector2 scrollPosition;

    public string searchQuery = "";

    [MenuItem("Tools/ScriptableEditor")]
    public static void ShowWindow()
    {
        GetWindow<MainWindow>("MainWindow");
        controller = new MainViewModel();
    }

    private void OnGUI()
    {
        controller.selectedScript = (MonoScript)EditorGUILayout.ObjectField(
            "Type",
            controller.selectedScript,
            typeof(MonoScript),
            false);

        // Check if the selected script is valid
        if (controller.selectedScript != null)
        {
            controller.scriptType = controller.selectedScript.GetClass();

            if (controller.scriptType == null || !controller.scriptType.IsSubclassOf(typeof(ScriptableObject)) || controller.scriptType.IsAbstract)
            {
                EditorGUILayout.HelpBox(
                    "The selected script must be a non-abstract class that inherits from ScriptableObject.",
                    MessageType.Error);
            }
            else
            {
                if (GUILayout.Button("Create"))
                {
                    controller.CreateScriptableObject(controller.scriptType);
                    if (controller.selectedItem)
                    {
                        DetailWindow.ShowWindow(controller.selectedItem);
                    }
                }
            }

            if (GUILayout.Button("Refresh Objects"))
            {
                controller.LoadAllItems();
            }
            searchQuery = EditorGUILayout.TextField("Search:", searchQuery);
            controller.Search(searchQuery);
            ObjectListView(controller.filterItems);
            GUILayout.Label("Count: " + controller.filterItems.Count);
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
                controller.SelectItem(item);
                DetailWindow.ShowWindow(controller.selectedItem);
            }

            if (GUILayout.Button("Delete", GUILayout.Width(60)))
            {
                controller.DeleteItem(item);
                break;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndScrollView();
    }






}
#endif