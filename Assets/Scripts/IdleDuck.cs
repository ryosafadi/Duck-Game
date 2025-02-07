using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleDuck : MonoBehaviour
{
    private float happiness = 100f;
    private float hunger = 100f;
    private static int currLevel = 1;
    private static int expThresh = 100;
    private static int exp = 0;
    public static float speed = 1.19f;
    public static float stamina = 100f;


    public TMP_Text silverFishCounter;
    public TMP_Text redFishCounter;
    public TMP_Text greenFishCounter;
    public TMP_Text levelCounter;
    public static int silverFish;
    public static int redFish;
    public static int greenFish;

    private readonly float decayRate = 1f;

    void Start()
    {
        FishToEat();
        ShowLevel();
    }

    private void Update()
    {
        happiness = Mathf.Clamp(happiness - decayRate * Time.deltaTime, 0, 100);
        hunger = Mathf.Clamp(hunger - decayRate * Time.deltaTime, 0, 100);
        if(exp >= expThresh){
            levelUp();
        }
    }

    private void levelUp()
    {
        currLevel += 1;
        exp = 0;
        expThresh += 20;
        ShowLevel();
    }

    void ShowLevel()
    {
        levelCounter.text = "Lvl. " + currLevel;
    }

    void FishToEat()
    {
        silverFishCounter.text = "Fish: " + silverFish;
        redFishCounter.text = "Fish: " + redFish;
        greenFishCounter.text = "Fish: " + greenFish;
    }

    public void ModifyHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    public void FeedSilver(float amount)
    {
        if(silverFish > 0){
            hunger = Mathf.Clamp(hunger + amount, 0, 100);
            exp += 7;
            silverFish--;
            FishToEat();
        }
    }
    
    public void FeedRed(float amount)
    {
        if(redFish > 0){
            hunger = Mathf.Clamp(hunger + amount, 0, 100);
            exp += 13;
            redFish--;
            FishToEat();
        }
    }

    public void FeedGreen(float amount)
    {
        if(greenFish > 0){
            hunger = Mathf.Clamp(hunger + amount, 0, 100);
            exp += 20;
            greenFish--;
            FishToEat();
        }
    }

    public float GetHappiness()
    {
        return happiness;
    }

    public float GetHunger()
    {
        return hunger;
    }
}
