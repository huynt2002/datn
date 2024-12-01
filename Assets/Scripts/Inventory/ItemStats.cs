using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemStats", order = 2)]
public class ItemStats : ScriptableObject
{
    public enum ItemType
    {
        Common, Rare, Legend
    }
    public string id;
    public string itemName;
    public float ATKAmount;
    public float HPAmount;
    public string description;
    public Sprite icon;
    public ItemType itemType;
    public GameObject itemEffectObject;
    public void ApplyItemStats(Entity e)
    {
        e.IncreaseHP(HPAmount);
        e.IncreaseDmg(ATKAmount);
    }

    public void RemoveItemStats(Entity e)
    {
        e.DecreaseHP(HPAmount);
        e.DecreaseDmg(ATKAmount);
    }
}
