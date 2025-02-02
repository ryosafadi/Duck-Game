using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDuck : MonoBehaviour
{
    private float happiness = 50f;
    private float hunger = 50f;

    private readonly float decayRate = 1f;

    private void Update()
    {
        happiness = Mathf.Clamp(happiness - decayRate * Time.deltaTime, 0, 100);
        hunger = Mathf.Clamp(hunger - decayRate * Time.deltaTime, 0, 100);
    }

    public void ModifyHappiness(float amount)
    {
        happiness = Mathf.Clamp(happiness + amount, 0, 100);
    }

    public void ModifyHunger(float amount)
    {
        hunger = Mathf.Clamp(hunger + amount, 0, 100);
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
