using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    public void LoadIdle()
    {
        SceneManager.LoadScene("Idle Mode");
    }
    public void LoadFishing()
    {
        SceneManager.LoadScene("Active Mode");
    }
    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void LoadStart()
    {
        SceneManager.LoadScene("Start Screen");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void NewGame()
    {
        string savePath = Application.persistentDataPath + "/saveData.json";

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        SaveSystem.ResetGame();

        LoadIdle();
    }
    public void ContinueGame()
    {
        SaveSystem.LoadGame();

        LoadIdle();
    }
    public void ExitToMenu()
    {
        SaveSystem.SaveGame();

        LoadStart();
    }
}
