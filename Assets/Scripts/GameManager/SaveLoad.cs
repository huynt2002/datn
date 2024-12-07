
using UnityEngine;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public bool tutorial;
    public PlayerData playerStats;
    public int currentLevelIndex;
    public List<string> inventoryItemIds = new();

    public void Default()
    {
        tutorial = false;
        var data = PlayerStats.instance;
        data?.SetDefault();
        playerStats = new PlayerData(data.currentHP, data.maxHP, data.damage, data.speed, data.coin, data.gem, data.criticalChance, data.criticalDamage);
        currentLevelIndex = 0;
        inventoryItemIds.Clear();
    }

    public void UpdateGameState(PlayerStats playerStats, int currentLevelIndex, InventoryManager inventoryManager, bool tutorial = false)
    {
        foreach (var item in inventoryManager.items)
        {
            inventoryItemIds.Add(item.Key.id);
        }
        this.currentLevelIndex = currentLevelIndex;
        var data = playerStats;
        this.playerStats = new PlayerData(data.currentHP, data.maxHP, data.damage, data.speed, data.coin, data.gem, data.criticalChance, data.criticalDamage);
        this.tutorial = tutorial;
    }
}

[Serializable]
public class PlayerData
{
    public float currentHP { get; protected set; }
    public float maxHP { get; protected set; }

    public float speed { get; protected set; }

    public float damage { get; protected set; }
    public float criticalChance { get; private set; }
    public float criticalDamage { get; private set; }
    public int coin;
    public int gem;

    public PlayerData(float currentHP, float maxHP, float damage, float speed, int coin, int gem, float criticalChance, float criticalDamage)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
        this.damage = damage;
        this.speed = speed;
        this.coin = coin;
        this.gem = gem;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
    }
}

public static class SaveLoad
{
    private static string GetFilePath(string fileName)
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    public static void SaveGame(GameData data, string fileName = "savefile.json")
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        string filePath = GetFilePath(fileName);

        File.WriteAllText(filePath, json);
        Debug.Log($"Game saved to: {filePath}");
    }

    public static GameData LoadGame(string fileName = "savefile.json")
    {
        string filePath = GetFilePath(fileName);

        if (!File.Exists(filePath))
        {
            Debug.LogError("Save file not found!");
            return null;
        }

        string json = File.ReadAllText(filePath);
        GameData data = JsonConvert.DeserializeObject<GameData>(json);
        Debug.Log($"Game loaded from: {filePath}");
        return data;
    }

}
