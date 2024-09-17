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
    public int id;
    public string ItemName;
    public float ATKAmount;
    public float DEFAmount;
    public float HPAmount;
    public string description;
    public Sprite icon;
    public ItemType itemType;
}
