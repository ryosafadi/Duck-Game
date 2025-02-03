using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IdleDuck : MonoBehaviour
{
    private float happiness = 50f;
    private float hunger = 50f;
    public TMP_Text totalFishCounter;
    public static int totalFish;

    private readonly float decayRate = 0.5f;

    void Start()
    {
        FishToEat();
    }

    private void Update()
    {
        happiness = Mathf.Clamp(happiness - decayRate * Time.deltaTime, 0, 100);
        hunger = Mathf.Clamp(hunger - decayRate * Time.deltaTime, 0, 100);
    }

    void FishToEat()
    {
        totalFishCounter.text = "Fish: " + totalFish;
    }

    public void ModifyHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    public void ModifyHunger(float amount)
    {
        if(totalFish > 0){
            hunger = Mathf.Clamp(hunger + amount, 0, 100);
            totalFish--;
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
