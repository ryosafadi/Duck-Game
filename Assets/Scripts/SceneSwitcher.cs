using System.Collections;
using System.Collections.Generic;
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
}
