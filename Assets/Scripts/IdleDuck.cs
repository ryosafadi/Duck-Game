using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDuck : MonoBehaviour
{
    private float happiness = 50;
    private float hunger = 50;

    private readonly float decayRate = 1;

    private void Update()
    {
        // Decrease happiness and hunger every second
        happiness = Mathf.Clamp(happiness - decayRate * Time.deltaTime, 0, 100);
        hunger = Mathf.Clamp(hunger - decayRate * Time.deltaTime, 0, 100);
    }

    public void ModifyHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
        Debug.Log("Happiness: " + happiness);
    }

    public void ModifyHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0, 100);
        Debug.Log("Hunger: " + hunger);
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
