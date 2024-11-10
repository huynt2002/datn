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
        //public float speedAmount;
    }
    public string traitName;
    public Sprite icon;
    public int currentLevel { get; private set; } = 0;
    public List<ItemTraitEffectLevel> effects;
    public GameObject itemTraitEffect;

    public void ApplyStats(Entity e)
    {
        if (currentLevel == 0) { return; }
        e.IncreaseHP(effects[currentLevel - 1].HPAmount);
        e.IncreaseDmg(effects[currentLevel - 1].ATKAmount);
    }

    public ItemTraitEffectLevel GetCurrentLevel()
    {
        return effects[currentLevel - 1];
    }

    public void RemoveStats(Entity e)
    {
        if (currentLevel == 0) { return; }
        e.DecreaseHP(effects[currentLevel - 1].HPAmount);
        e.DecreaseDmg(effects[currentLevel - 1].ATKAmount);
    }

    public void UpdateCurrentLevel(int num)
    {
        currentLevel = 0;
        if (num == 0) { return; }
        for (int i = 1; i <= effects.Count; i++)
        {
            if (effects[i - 1].levelNum == num)
            {
                currentLevel = i;
            }
        }
    }
    public GameObject GetTraitEffect(Transform parent)
    {
        if (currentLevel == 0) { return null; }
        if (!itemTraitEffect) { return null; }
        var effectObject = Instantiate(itemTraitEffect, parent) as GameObject;
        effectObject.GetComponent<ItemTraitEffect>().SetLevel(currentLevel);
        return effectObject;
    }
}