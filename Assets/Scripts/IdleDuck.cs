using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class IdleDuck : MonoBehaviour
{
    public static float happiness = 100f;
    public static float maxHappiness = 100f;
    public static float hunger = 100f;
    public static float maxHunger = 100f;
    public static float stamina = 100f;
    public static float maxStamina = 100f;
    public static float speed = 1.19f;

    private static int currLevel = 1;
    private static int expThresh = 100;
    private static int exp = 0;
    public static int skillPoints = 0;

    public TMP_Text silverFishCounter;
    public TMP_Text redFishCounter;
    public TMP_Text greenFishCounter;
    public TMP_Text levelCounter;
    public TMP_Text speedCounter;
    public TMP_Text hungerBarText;
    public TMP_Text happinessBarText;
    public TMP_Text staminaBarText;

    public static int silverFish;
    public static int redFish;
    public static int greenFish;

    private AudioSource ourAudioSource;
    [SerializeField] private AudioClip Quack;
    [SerializeField] private AudioClip Munch;

    private readonly float decayRate = 1f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        ourAudioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = CustomizationManager.Instance.GetColor();

        UpdateFish();
        UpdateLevel();
        UpdateSpeed();
    }

    private void Update()
    {
        happiness = Mathf.Clamp(happiness - decayRate * Time.deltaTime, 0, maxHappiness);
        hunger = Mathf.Clamp(hunger - decayRate * Time.deltaTime, 0, maxHunger);
        stamina = Mathf.Clamp(stamina + (decayRate*2) * Time.deltaTime, 0, maxStamina);
        hungerBarText.text = hunger + " / " + maxHunger;
        happinessBarText.text = happiness + " / " + maxHappiness;
        staminaBarText.text = stamina + " / " + maxStamina;
        if(exp >= expThresh){
            levelUp();
        }
    }

    private void levelUp()
    {
        skillPoints += 1;
        currLevel += 1;
        exp = 0;
        expThresh += 20;
        UpdateFish();
    }

    void UpdateLevel()
    {
        levelCounter.text = "Lvl. " + currLevel;
    }

    void UpdateSpeed()
    {
        speedCounter.text = "Speed = " + speed;
    }

    void UpdateFish()
    {
        silverFishCounter.text = "Fish: " + silverFish;
        redFishCounter.text = "Fish: " + redFish;
        greenFishCounter.text = "Fish: " + greenFish;
    }

    public void ModifyHappiness(float amount)
    {
        ourAudioSource.PlayOneShot(Quack);
        happiness = Mathf.Clamp(happiness + amount, 0, maxHappiness);
    }

    public void FeedSilver(float amount)
    {
        if(silverFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 7;
            silverFish--;
            UpdateFish();
        }
    }
    
    public void FeedRed(float amount)
    {
        if(redFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 13;
            redFish--;
            UpdateFish();
        }
    }

    public void FeedGreen(float amount)
    {
        if(greenFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 20;
            greenFish--;
            UpdateFish();
        }
    }

    public void IncreaseHunger(){
        ourAudioSource.PlayOneShot(Quack);
        maxHunger += 5f;
        skillPoints --;
    }

    public void IncreaseHappiness(){
        ourAudioSource.PlayOneShot(Quack);
        maxHappiness += 5f;
        skillPoints --;
    }

    public void IncreaseStamina(){
        ourAudioSource.PlayOneShot(Quack);
        maxStamina += 5f;
        skillPoints --;
    }

    public void IncreaseSpeed(){
        ourAudioSource.PlayOneShot(Quack);
        speed += 0.2f;
        skillPoints --;
    }

    public float GetHappiness()
    {
        return happiness;
    }

    public float GetHunger()
    {
        return hunger;
    }

    public float GetStamina()
    {
        return stamina;
    }
}
