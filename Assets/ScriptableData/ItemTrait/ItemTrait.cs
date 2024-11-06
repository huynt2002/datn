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
    }
    public string traitName;
    public float ATKAmount;
    public float HPAmount;
    public float speedAmount;
    public string description;
    public Sprite icon;
    public int currentLevel = -1;
    public List<ItemTraitEffectLevel> effects;
    public GameObject itemTraitEffect;

    public void ApplyStats(Entity e)
    {
        e.IncreaseHP(HPAmount);
        e.IncreaseDmg(ATKAmount);
    }

    public void RemoveStats(Entity e)
    {
        e.DecreaseHP(HPAmount);
        e.DecreaseDmg(ATKAmount);
    }

    public void UpdateCurrentLevel(int num)
    {
        for (int i = effects.Count; i > 0; i--)
        {
            if (effects[i].levelNum < num)
            {
                currentLevel = i;
                break;
            }
        }
    }
    public GameObject GetTraitEffect(Transform parent)
    {
        var effectObject = Instantiate(itemTraitEffect, parent) as GameObject;
        effectObject.GetComponent<ItemTraitEffect>().level = currentLevel;
        return effectObject;
    }
}