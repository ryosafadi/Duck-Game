using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TMPro.Examples;

public class IdleDuck : MonoBehaviour
{
    public static float health = 100f;
    public static float maxHealth = 100f;
    public static float happiness = 100f;
    public static float maxHappiness = 100f;
    public static float hunger = 100f;
    public static float maxHunger = 100f;
    public static float stamina = 100f;
    public static float maxStamina = 100f;
    public static float actualSpeed = 1.19f;
    private static float speedLevel = 1;

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
    public TMP_Text healthBarText;

    public static int silverFish;
    public static int redFish;
    public static int greenFish;

    private AudioSource ourAudioSource;
    [SerializeField] private AudioClip Quack;
    [SerializeField] private AudioClip Munch;
    [SerializeField] private AudioClip Select;
    [SerializeField] private AudioClip Exit;

    //ALL OF THESE NEED TO BE REASSIGNED FOR FINAL BUILD
    private readonly float changeRate = 1f; // standard rate of change
    private readonly float healthDecayRate = 0.3f; 
    private readonly float staminaChangeRate = 0.5f;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer spriteAccessory;

    void Start()
    {
        ourAudioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = CustomizationManager.Instance.GetColor();

        UpdateAll();

    }

    void Update()
    {
        happiness = Mathf.Clamp(happiness - changeRate * Time.deltaTime, 0, maxHappiness);
        hunger = Mathf.Clamp(hunger - changeRate * Time.deltaTime, 0, maxHunger);
        stamina = Mathf.Clamp(stamina + (staminaChangeRate + happiness/100) * Time.deltaTime, 0, maxStamina);
        if (hunger <= 0){
            health = Mathf.Clamp(health - healthDecayRate * Time.deltaTime, 0, maxHealth);
        }
        else{
            health = Mathf.Clamp(health + (changeRate + happiness/100) * Time.deltaTime, 0, maxHealth);
        }
        UpdateAll();
        if(exp >= expThresh){
            LevelUp();
        }
        if(health <= 0){
            Revive();
        }
    }

    private void UpdateAll(){
        UpdateHealth();
        UpdateHunger();
        UpdateHappiness();
        UpdateStamina();
        UpdateSpeed();
        UpdateLevel();
        UpdateFish();
        UpdateLevel();
        UpdateSpeed();
    }

    private void Revive(){
        currLevel = 1;
        exp = 0;
        expThresh = 100;
        maxHealth = Mathf.Ceil(maxHealth - (maxHealth - 100) / 2);
        maxHappiness = Mathf.Ceil(maxHappiness - (maxHappiness - 100) / 2);
        maxHunger = Mathf.Ceil(maxHunger - (maxHunger - 100) / 2);
        maxStamina = Mathf.Ceil(maxStamina - (maxStamina - 100) / 2);
        actualSpeed = actualSpeed - (0.2f * (speedLevel / 2f));
        speedLevel = Mathf.Ceil(speedLevel / 2);
        UpdateAll();
        health = maxHealth;
        happiness = maxHappiness;
        hunger = maxHunger;
        stamina = maxStamina;
    }

    private void LevelUp()
    {
        skillPoints += 1;
        currLevel += 1;
        exp = (exp - expThresh);
        expThresh += 20;
        UpdateLevel();
    }

    void UpdateLevel()
    {
        levelCounter.text = "Lvl. " + currLevel + "\n(" + exp + "/" + expThresh + ")";
    }

    void UpdateSpeed()
    {
        speedCounter.text = "Speed = " + speedLevel;
    }

    void UpdateHunger()
    {
        hungerBarText.text = Mathf.Floor(hunger) + " / " + maxHunger;
    }

    void UpdateHappiness()
    {
        happinessBarText.text = Mathf.Floor(happiness) + " / " + maxHappiness;
    }

    void UpdateStamina()
    {
        staminaBarText.text = Mathf.Floor(stamina) + " / " + maxStamina;
    }

    void UpdateHealth()
    {
        healthBarText.text = Mathf.Floor(health) + " / " + maxHealth;
    }

    void UpdateFish()
    {
        silverFishCounter.text = "Fish: " + silverFish;
        redFishCounter.text = "Fish: " + redFish;
        greenFishCounter.text = "Fish: " + greenFish;
    }

    //funciton for petting
    public void ModifyHappiness(float amount)
    {
        ourAudioSource.PlayOneShot(Quack);
        happiness = Mathf.Clamp(happiness + amount, 0, maxHappiness);
    }

    //three functions for feeding
    public void FeedSilver(float amount)
    {
        if(silverFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 100; //for the sake of playtesting, the exp is lower than it should be
            silverFish--;
            UpdateFish();
        }
    }
    
    public void FeedRed(float amount)
    {
        if(redFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 23;
            redFish--;
            UpdateFish();
        }
    }

    public void FeedGreen(float amount)
    {
        if(greenFish > 0){
            ourAudioSource.PlayOneShot(Munch);
            hunger = Mathf.Clamp(hunger + amount, 0, maxHunger);
            exp += 31;
            greenFish--;
            UpdateFish();
        }
    }

    //level up stat increases
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

    public void IncreaseHealth(){
        ourAudioSource.PlayOneShot(Quack);
        maxHealth += 5f;
        skillPoints --;
    }

    public void IncreaseSpeed(){
        ourAudioSource.PlayOneShot(Quack);
        actualSpeed += 0.2f;
        speedLevel += 1;
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

    public float GetHealth()
    {
        return health;
    }
}
