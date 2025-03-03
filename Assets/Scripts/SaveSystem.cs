using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Drawing;

public static class SaveSystem
{
    private static readonly string savePath = Application.persistentDataPath + "/saveData.json";

    public static void SaveGame()
    {
        IdleDuckData data = new IdleDuckData()
        {
            health = IdleDuck.health,
            maxHealth = IdleDuck.maxHealth,
            happiness = IdleDuck.happiness,
            maxHappiness = IdleDuck.maxHappiness,
            hunger = IdleDuck.hunger,
            maxHunger = IdleDuck.maxHunger,
            stamina = IdleDuck.stamina,
            maxStamina = IdleDuck.maxStamina,
            actualSpeed = IdleDuck.actualSpeed,
            speedLevel = IdleDuck.speedLevel,
            currLevel = IdleDuck.currLevel,
            expThresh = IdleDuck.expThresh,
            exp = IdleDuck.exp,
            skillPoints = IdleDuck.skillPoints,
            silverFish = IdleDuck.silverFish,
            redFish = IdleDuck.redFish,
            greenFish = IdleDuck.greenFish
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log(Application.persistentDataPath);
    }

    public static void ResetGame()
    {
        IdleDuck.health = 100f;
        IdleDuck.maxHealth = 100f;
        IdleDuck.happiness = 100f;
        IdleDuck.maxHappiness = 100f;
        IdleDuck.hunger = 100f;
        IdleDuck.maxHunger = 100f;
        IdleDuck.stamina = 100f;
        IdleDuck.maxStamina = 100f;
        IdleDuck.actualSpeed = 1.19f;
        IdleDuck.speedLevel = 1;
        IdleDuck.currLevel = 1;
        IdleDuck.expThresh = 100;
        IdleDuck.exp = 0;
        IdleDuck.skillPoints = 0;
        IdleDuck.silverFish = 0;
        IdleDuck.redFish = 0;
        IdleDuck.greenFish = 0;

        PlayerPrefs.DeleteKey("SelectedHatSprite");
        PlayerPrefs.DeleteKey("Color_R");
        PlayerPrefs.DeleteKey("Color_G");
        PlayerPrefs.DeleteKey("Color_B");
        PlayerPrefs.DeleteKey("Color_A");
    }

    public static void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            IdleDuckData data = JsonUtility.FromJson<IdleDuckData>(json);

            IdleDuck.health = data.health;
            IdleDuck.happiness = data.happiness;
            IdleDuck.hunger = data.hunger;
            IdleDuck.stamina = data.stamina;
            IdleDuck.actualSpeed = data.actualSpeed;
            IdleDuck.speedLevel = data.speedLevel;
            IdleDuck.currLevel = data.currLevel;
            IdleDuck.exp = data.exp;
            IdleDuck.skillPoints = data.skillPoints;
            IdleDuck.silverFish = data.silverFish;
            IdleDuck.redFish = data.redFish;
            IdleDuck.greenFish = data.greenFish;
        }
    }
}
