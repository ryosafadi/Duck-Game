using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShowContinue : MonoBehaviour
{
    void Start()
    {
        string savePath = Application.persistentDataPath + "/saveData.json";

        if (!File.Exists(savePath))
        {
            gameObject.SetActive(false);
        }
    }
}
