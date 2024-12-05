using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityStats", order = 1)]
public class EntityStats : ScriptableObject
{
    public float damage;
    public float maxHP;
    public float speed;
    public float criticalChance;
    public float criticalDamage;

}