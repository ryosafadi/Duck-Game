using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance;

    public Color selectedColor;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color)
    {
        selectedColor = color;
    }

    public Color GetColor()
    {
        return selectedColor;
    }
}
