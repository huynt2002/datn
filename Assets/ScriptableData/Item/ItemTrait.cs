using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemTrait", order = 3)]
public class ItemTrait : ScriptableObject
{
    [Serializable]
    public class ItemTraitEffectLevel
    {
        public int levelNum;
        public string description;
        public GameObject effectObject;
    }
    public string name;
    public List<ItemTraitEffectLevel> effects;
    public GameObject GetTraitEffect(int num, Vector2 pos)
    {
        for (int i = effects.Count; i > 0; i--)
        {
            if (effects[i].levelNum == num)
            {
                return Instantiate(effects[i].effectObject, pos, Quaternion.identity) as GameObject;
            }
        }
        return null;
    }
}