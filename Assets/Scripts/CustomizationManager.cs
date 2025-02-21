using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    public static CustomizationManager Instance;

    public Color selectedColor;
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
#if UNITY_EDITOR
                            Sprite loadedSprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/" + spriteName + ".png");
                            if (loadedSprite != null)
                            {
                                selectedAccessory.sprite = loadedSprite;
                                Debug.Log("Loaded saved sprite: " + spriteName);
                            }
                            else
                            {
                                Debug.LogError("Sprite could not be loaded from Assets/Sprites: " + spriteName);
                            }
#else
                        Debug.LogError("AssetDatabase can only be used in the Unity Editor.");
#endif
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
        selectedColor = color;
    }

    public Color GetColor()
    {
        return selectedColor;
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
