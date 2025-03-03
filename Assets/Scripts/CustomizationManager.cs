using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance;

    private SpriteRenderer selectedAccessory;

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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Referenced ChatGPT for help in saving accessory between scenes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Idle Mode")
        {
            GameObject duck = GameObject.Find("duck");
            if (duck != null)
            {
                Transform hatTransform = duck.transform.Find("hat");
                if (hatTransform != null)
                {
                    selectedAccessory = hatTransform.GetComponent<SpriteRenderer>();
                    if (selectedAccessory == null)
                    {
                        Debug.LogError("SpriteRenderer component not found on 'hat'.");
                    }
                    else
                    {
                        string spriteName = PlayerPrefs.GetString("SelectedHatSprite", "");
                        if (!string.IsNullOrEmpty(spriteName))
                        {
                            Sprite loadedSprite = Resources.Load<Sprite>("Sprites/" + spriteName);
                            if (loadedSprite != null)
                            {
                                selectedAccessory.sprite = loadedSprite;
                            }
                            else
                            {
                                Debug.LogError("Sprite could not be loaded from Resources: " + spriteName);
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError("'hat' child not found under 'duck'.");
                }
            }
            else
            {
                Debug.LogError("'duck' object not found in the scene.");
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetColor(Color color)
    {
        PlayerPrefs.SetFloat("Color_R", color.r);
        PlayerPrefs.SetFloat("Color_G", color.g);
        PlayerPrefs.SetFloat("Color_B", color.b);
        PlayerPrefs.SetFloat("Color_A", color.a);
        PlayerPrefs.Save();
    }

    public Color GetColor()
    {
        if (PlayerPrefs.HasKey("Color_R"))
        {
            float r = PlayerPrefs.GetFloat("Color_R");
            float g = PlayerPrefs.GetFloat("Color_G");
            float b = PlayerPrefs.GetFloat("Color_B");
            float a = PlayerPrefs.GetFloat("Color_A");

            Color savedColor = new Color(r, g, b, a);
            return savedColor;
        }
            return new Color(255f / 255f, 255f / 255f, 255f / 255f);
    }

    public void SetAccessory(Sprite sprite)
    {
        if (selectedAccessory != null && sprite != null)
        {
            selectedAccessory.sprite = sprite;

            string spriteName = sprite.name;
            PlayerPrefs.SetString("SelectedHatSprite", spriteName);
            PlayerPrefs.Save();
        }
    }

    public Sprite GetAccessory()
    {
        return selectedAccessory != null ? selectedAccessory.sprite : null;
    }
}
