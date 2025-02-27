using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static SaveManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject across scenes

            SaveSystem.LoadGame();
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveGame(); // Automatically save when the game closes
    }
}
