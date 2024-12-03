using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System;
using System.Reflection;

#if UNITY_EDITOR
public static class ItemEditorDefine
{
    public static string className = "ItemData";
    public static string folderPath = "Assets/ScriptableObjects"; // Default folder for saving script
}
public class ScriptEditor : EditorWindow
{
    public enum AttributeType
    {
        Int, Float, String, Sprite, Enum, GameObject
    }

    public class Attribute
    {
        public Attribute()
        {
            attributeName = "New attribute name";
            attributeType = 0;
        }
        public string attributeName;
        public AttributeType attributeType;
    }

    public class EnumType
    {
        public EnumType()
        {
            enumName = "Enum class name";
            elements = new List<string>();
        }
        public string enumName;
        public List<string> elements;
    }
    private Vector2 scrollPosition;
    private string scriptContent = "";
    List<KeyValuePair<Attribute, EnumType>> attributes;
    [MenuItem("Tools/ScriptableObject Class Generator")]
    public static void ShowWindow()
    {
        GetWindow<ScriptEditor>("Script Gen");
    }

    private void OnGUI()
    {
        GUILayout.Label("Data info", EditorStyles.boldLabel);

        LabelComponent("Item Class Name:", ItemEditorDefine.className);
        LabelComponent("Path:", ItemEditorDefine.folderPath);
        if (attributes != null)
        {
            AttributeView();
        }
        else
        {
            attributes = LoadData();
        }
        if (GUILayout.Button("Generate Script"))
        {
            GenerateScript();
        }
    }

    void LabelComponent(string title, string text)
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(5);
        EditorGUILayout.LabelField(title, text);
        GUILayout.Space(5);
        GUILayout.EndHorizontal();
    }

    void AttributeView()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("Attribute", EditorStyles.boldLabel);
        if (GUILayout.Button("Add attribute"))
        {
            Attribute attr = new Attribute();
            attributes.Add(new KeyValuePair<Attribute, EnumType>(attr, new EnumType()));
        }
        GUILayout.EndHorizontal();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        var tmp = new List<KeyValuePair<Attribute, EnumType>>(attributes);
        foreach (var attribute in tmp)
        {
            AttributeItem(attribute);
        }

        GUILayout.EndScrollView();
    }

    void AttributeItem(KeyValuePair<Attribute, EnumType> attribute)
    {
        GUILayout.BeginVertical("box");
        GUILayout.BeginHorizontal();
        attribute.Key.attributeName = EditorGUILayout.TextField(attribute.Key.attributeName.Trim());
        attribute.Key.attributeType = (AttributeType)EditorGUILayout.EnumPopup(attribute.Key.attributeType);
        if (GUILayout.Button("Remove"))
        {
            attributes.Remove(attribute);
        }
        GUILayout.EndHorizontal();
        if (attribute.Key.attributeType == AttributeType.Enum)
        {
            EnumView(attribute.Value);
        }
        GUILayout.EndVertical();
    }

    void EnumView(EnumType enumType)
    {
        GUILayout.BeginHorizontal();
        enumType.enumName = EditorGUILayout.TextField(enumType.enumName.Trim());
        if (GUILayout.Button("Add"))
        {
            string newElement = "value";
            enumType.elements.Add(newElement);
        }
        GUILayout.EndHorizontal();

        for (int i = 0; i < enumType.elements.Count; i++)
        {
            GUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            enumType.elements[i] = EditorGUILayout.TextField(enumType.elements[i].Trim());
            if (GUILayout.Button("Remove"))
            {
                enumType.elements.Remove(enumType.elements[i]);
                i--;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }

    void GenerateScript()
    {
        // Create folder if it doesn't exist
        if (!Directory.Exists(ItemEditorDefine.folderPath))
        {
            Directory.CreateDirectory(ItemEditorDefine.folderPath);
        }

        string scriptFilePath = Path.Combine(ItemEditorDefine.folderPath, ItemEditorDefine.className + ".cs");

        // Check if the file already exists
        if (File.Exists(scriptFilePath))
        {
            File.Delete(scriptFilePath);
        }

        // Define the content of the ScriptableObject class
        scriptContent = GenerateScriptContent();

        // Write the content to the file
        File.WriteAllText(scriptFilePath, scriptContent);

        // Refresh the asset database to reflect the new file
        AssetDatabase.Refresh();
    }

    List<KeyValuePair<Attribute, EnumType>> LoadData()
    {
        try
        {
            var f = File.ReadAllText(Path.Combine(ItemEditorDefine.folderPath, ItemEditorDefine.className + ".cs"));
            return new List<KeyValuePair<Attribute, EnumType>>();
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
            return new List<KeyValuePair<Attribute, EnumType>>();
        }
    }

    string GenerateScriptContent()
    {
        string attributesValue = "";
        foreach (var attribute in attributes)
        {
            var name = attribute.Key.attributeName;
            var type = attribute.Key.attributeType;
            if (type == AttributeType.Enum)
            {
                string enumElement = "";
                var enumName = attribute.Value.enumName;
                var elements = attribute.Value.elements;
                for (int i = 0; i < elements.Count - 1; i++)
                {
                    enumElement += elements[i] + ", ";
                }
                enumElement += elements[elements.Count - 1];
                attributesValue += "public enum " + enumName + "{\n" + enumElement + "}\n";
                attributesValue += "public " + enumName + " " + name + ";\n";

            }
            else
            {
                var typeText = "int";
                switch (type)
                {
                    case AttributeType.Float:
                        typeText = "float";
                        break;
                    case AttributeType.String:
                        typeText = "string";
                        break;
                    case AttributeType.Int:
                        typeText = "int";
                        break;
                    case AttributeType.Sprite:
                        typeText = "Sprite";
                        break;
                    case AttributeType.GameObject:
                        typeText = "GameObject";
                        break;
                }
                attributesValue += "public " + typeText + " " + name + ";\n";
            }
        }

        return
            "using UnityEngine;\n\n" +
            "public class " + ItemEditorDefine.className + " : ScriptableObject\n" +
            "{\n" +
                attributesValue
            + "\n}";
    }
}
#endif
