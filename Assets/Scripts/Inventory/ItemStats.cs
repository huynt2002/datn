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
    public float damageAmount;
    public float hpAmount;
    public string description;
    public float criticalChance;
    public float criticalDamage;
    public float speed;
    public Sprite icon;
    public ItemType itemType;
    public GameObject itemEffectObject;
    public void ApplyItemStats(Entity e)
    {
        e.IncreaseHP(hpAmount);
        e.IncreaseDmg(damageAmount);
        e.IncreaseCriticalChance(criticalChance);
        e.IncreaseCriticalDamage(criticalDamage);
        e.IncreaseSpeed(speed);
    }

    public void RemoveItemStats(Entity e)
    {
        e.DecreaseHP(hpAmount);
        e.DecreaseDmg(damageAmount);
        e.IncreaseCriticalChance(-criticalChance);
        e.IncreaseCriticalDamage(-criticalDamage);
        e.IncreaseSpeed(-speed);
    }
}
