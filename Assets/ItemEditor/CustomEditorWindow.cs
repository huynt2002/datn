using UnityEditor;
using UnityEngine;

public class MyCustomEditorWindow : EditorWindow
{
    private string gameObjectName = "New GameObject";
    private GameObject selectedGameObject;
    private MonoScript componentToAdd;

    // Specify the base class or interface to filter
    private System.Type filterType = typeof(ItemEffect);

    [MenuItem("Tools/Custom GameObject Creator")]
    public static void ShowWindow()
    {
        GetWindow<MyCustomEditorWindow>("GameObject Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create a GameObject", EditorStyles.boldLabel);

        // Input field for the GameObject name
        gameObjectName = EditorGUILayout.TextField("GameObject Name", gameObjectName);

        // Field to assign a GameObject in the scene
        selectedGameObject = (GameObject)EditorGUILayout.ObjectField("Target GameObject", selectedGameObject, typeof(GameObject), true);

        // Field to select a component type, filtered by the specific class
        componentToAdd = SelectFilteredComponentField("Component to Add", componentToAdd, filterType);

        if (componentToAdd != null && !filterType.IsAssignableFrom(componentToAdd.GetClass()))
        {
            EditorGUILayout.HelpBox($"Selected script must inherit from {filterType.Name}.", MessageType.Error);
        }

        // Button to create a new GameObject or modify the selected one
        if (GUILayout.Button("Create GameObject or Add Component"))
        {
            CreateOrModifyGameObject();
        }
    }

    private MonoScript SelectFilteredComponentField(string label, MonoScript current, System.Type baseType)
    {
        // Create an object field
        MonoScript selectedScript = (MonoScript)EditorGUILayout.ObjectField(label, current, typeof(MonoScript), false);

        // If a script is selected, validate if it inherits from the base type
        if (selectedScript != null && baseType.IsAssignableFrom(selectedScript.GetClass()))
        {
            return selectedScript; // Valid selection
        }

        // If invalid, return null
        return null;
    }

    private void CreateOrModifyGameObject()
    {
        GameObject targetObject = selectedGameObject;

        // Create a new GameObject if none is selected
        if (targetObject == null)
        {
            targetObject = new GameObject(gameObjectName);
            Debug.Log($"Created new GameObject: {targetObject.name}");
        }

        // Add the selected component if valid
        if (componentToAdd != null)
        {
            System.Type componentType = componentToAdd.GetClass();

            if (componentType != null && filterType.IsAssignableFrom(componentType))
            {
                targetObject.AddComponent(componentType);
                Debug.Log($"Added {componentType.Name} to {targetObject.name}");
            }
            else
            {
                Debug.LogError($"The selected script does not inherit from {filterType.Name}.");
            }
        }

        // Select the GameObject in the scene
        Selection.activeGameObject = targetObject;
    }
}
