using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    private static readonly string savePath = Application.persistentDataPath + "/saveData.json";

    public static void SaveGame()
    {
        IdleDuckData data = new IdleDuckData()
        {
            health = IdleDuck.health,
            happiness = IdleDuck.happiness,
            hunger = IdleDuck.hunger,
            stamina = IdleDuck.stamina,
            actualSpeed = IdleDuck.actualSpeed,
            speedLevel = IdleDuck.speedLevel,
            currLevel = IdleDuck.currLevel,
            exp = IdleDuck.exp,
            skillPoints = IdleDuck.skillPoints
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log(Application.persistentDataPath);
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
        }
    }
}
