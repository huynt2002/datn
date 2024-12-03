using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class ItemEditor : EditorWindow
{
    private ItemStats selectedItem;
    ItemPool itemPool;
    string folderPath = "Assets/ScriptableData/Item/";
    private List<ItemStats> items = new List<ItemStats>();
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

        selectedItem = (ItemStats)EditorGUILayout.ObjectField("Item", selectedItem, typeof(ItemStats), false);

        if (selectedItem != null)
        {
            EditorGUI.BeginChangeCheck();

            selectedItem.id = EditorGUILayout.TextField("ID", selectedItem.id);
            selectedItem.itemName = EditorGUILayout.TextField("Item Name", selectedItem.itemName);
            selectedItem.icon = (Sprite)EditorGUILayout.ObjectField("Icon", selectedItem.icon, typeof(Sprite), false);
            selectedItem.description = EditorGUILayout.TextField("Description", selectedItem.description);
            selectedItem.itemType = (ItemStats.ItemType)EditorGUILayout.EnumPopup("Item Type", selectedItem.itemType);
            selectedItem.ATKAmount = EditorGUILayout.FloatField("ATK Amount", selectedItem.ATKAmount);
            selectedItem.HPAmount = EditorGUILayout.FloatField("HP Amount", selectedItem.HPAmount);
            selectedItem.itemEffectObject = (GameObject)EditorGUILayout.ObjectField("Item", selectedItem.itemEffectObject, typeof(GameObject), false);

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
        ItemStats newItem = CreateInstance<ItemStats>();
        newItem.itemName = itemName;
        string path = folderPath + $"{itemName}.asset";
        path = AssetDatabase.GenerateUniqueAssetPath(path);
        itemPool.items.Add(newItem);
        itemPool.items.Distinct();
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
        items = AssetDatabase.FindAssets("t:ItemStats")
            .Select(guid => AssetDatabase.LoadAssetAtPath<ItemStats>(AssetDatabase.GUIDToAssetPath(guid)))
            .ToList();
        itemPool = AssetDatabase.FindAssets("t:ItemPool")
            .Select(guid => AssetDatabase.LoadAssetAtPath<ItemPool>(AssetDatabase.GUIDToAssetPath(guid))).ToList()[0];
    }

    private void DeleteItem(ItemStats item)
    {
        string path = AssetDatabase.GetAssetPath(item);
        itemPool.items.Remove(item);
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.SaveAssets();
        LoadAllItems();
    }

}
#endif