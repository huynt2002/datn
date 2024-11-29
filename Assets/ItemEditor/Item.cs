using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int id;
    public string description;
    public ItemType itemType;
    public int value;

    public enum ItemType
    {
        Consumable,
        Equipment,
        QuestItem
    }

    // You can add additional properties like stats, effects, etc.
}
