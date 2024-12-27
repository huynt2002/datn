using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class DetailWindow : EditorWindow
{
    private static string fileName;
    private Editor objectEditor;
    static DetailViewModel controller;

    public static void ShowWindow(ScriptableObject obj)
    {
        var window = GetWindow<DetailWindow>();
        window.titleContent = new GUIContent("DetailWindow");
        controller = new DetailViewModel();
        controller.selectedObject = obj;
        if (obj)
        {
            fileName = obj.name;
        }
        window.Show();
    }

    private void OnGUI()
    {
        if (controller.selectedObject != null)
        {
            fileName = EditorGUILayout.TextField("Asset name:", fileName);
            GUILayout.Label("Object Detail", EditorStyles.boldLabel);
            if (objectEditor == null || objectEditor.target != controller.selectedObject)
                objectEditor = Editor.CreateEditor(controller.selectedObject);

            // Draw the inspector for the ScriptableObject
            objectEditor.OnInspectorGUI();
        }

        if (GUILayout.Button("Save"))
        {
            controller.SaveData(fileName);
        }

        if (GUILayout.Button("Close"))
        {
            Close();
        }

    }
}


#endif
