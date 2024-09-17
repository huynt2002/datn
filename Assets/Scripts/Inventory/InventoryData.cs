using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory", order = 5)]
public class InventoryData : ScriptableObject
{
    public List<ItemStats> items;
}
