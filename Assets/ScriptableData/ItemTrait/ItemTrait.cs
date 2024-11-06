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
        public float ATKAmount;
        public float HPAmount;
        public float speedAmount;
    }
    public string traitName;
    public Sprite icon;
    public int currentLevel { get; private set; } = -1;
    public List<ItemTraitEffectLevel> effects;
    public GameObject itemTraitEffect;

    public void ApplyStats(Entity e)
    {
        if (currentLevel == -1) return;
        e.IncreaseHP(effects[currentLevel].HPAmount);
        e.IncreaseDmg(effects[currentLevel].ATKAmount);
    }

    public void RemoveStats(Entity e)
    {
        if (currentLevel == -1) return;
        e.DecreaseHP(effects[currentLevel].HPAmount);
        e.DecreaseDmg(effects[currentLevel].ATKAmount);
    }

    public void UpdateCurrentLevel(int num)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            if (effects[i].levelNum <= num)
            {
                currentLevel = i;
            }
        }
    }
    public GameObject GetTraitEffect(Transform parent)
    {
        if (currentLevel == -1) return null;
        var effectObject = Instantiate(itemTraitEffect, parent) as GameObject;
        effectObject.GetComponent<ItemTraitEffect>().SetLevel(currentLevel);
        return effectObject;
    }
}