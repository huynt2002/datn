using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemPool", order = 5)]
public class ItemPool : ScriptableObject
{
    public List<ItemStats> items;
}
