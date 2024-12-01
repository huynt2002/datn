using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ItemEditor : EditorWindow
{
    private Item selectedItem;

    private List<Item> items = new List<Item>();
    private Vector2 scrollPosition;

    [MenuItem("Tools/Item Editor")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditor>("Item Editor");
    }

    private string newItemName = "New Item";
    private void OnGUI()
    {
        GUILayout.Label("Item Editor", EditorStyles.boldLabel);

        selectedItem = (Item)EditorGUILayout.ObjectField("Item", selectedItem, typeof(Item), false);

        if (selectedItem != null)
        {
            EditorGUI.BeginChangeCheck();

            selectedItem.itemName = EditorGUILayout.TextField("Item Name", selectedItem.itemName);
            selectedItem.icon = (Sprite)EditorGUILayout.ObjectField("Icon", selectedItem.icon, typeof(Sprite), false);
            selectedItem.id = EditorGUILayout.IntField("ID", selectedItem.id);
            selectedItem.description = EditorGUILayout.TextField("Description", selectedItem.description);
            selectedItem.itemType = (Item.ItemType)EditorGUILayout.EnumPopup("Item Type", selectedItem.itemType);
            selectedItem.value = EditorGUILayout.IntField("Value", selectedItem.value);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(selectedItem);
            }
        }
        else
        {
            GUILayout.Label("Select an Item to edit.");
        }

        if (GUILayout.Button("Refresh Items"))
        {
            LoadAllItems();
        }



        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        foreach (var item in items)
        {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            item.itemName = EditorGUILayout.TextField("Item Name", item.itemName);

            if (GUILayout.Button("Select", GUILayout.Width(60)))
            {
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
        GUILayout.Space(10);
        GUILayout.Label("Create New Item", EditorStyles.boldLabel);

        newItemName = EditorGUILayout.TextField("Item Name", newItemName);

        if (GUILayout.Button("Create Item"))
        {
            CreateNewItem(newItemName);
        }


    }

    private void CreateNewItem(string itemName)
    {
        Item newItem = CreateInstance<Item>();
        newItem.itemName = itemName;
        string path = $"Assets/ItemEditor/{itemName}.asset";
        path = AssetDatabase.GenerateUniqueAssetPath(path);

        AssetDatabase.CreateAsset(newItem, path);
        AssetDatabase.SaveAssets();
        LoadAllItems();
    }

    private void OnEnable()
    {
        LoadAllItems();
    }

    private void LoadAllItems()
    {
        items = AssetDatabase.FindAssets("t:Item")
            .Select(guid => AssetDatabase.LoadAssetAtPath<Item>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
    }

    private void DeleteItem(Item item)
    {
        string path = AssetDatabase.GetAssetPath(item);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        LoadAllItems();
    }

}
#endif