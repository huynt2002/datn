using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EntityStats", order = 1)]
public class EntityStats : ScriptableObject
{
    public float Damage;
    public float MaxHP;
    public float Speed;

}