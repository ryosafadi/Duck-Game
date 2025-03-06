using System;

[Serializable]
public class IdleDuckData
{
    public float health = 100f;
    public float maxHealth = 100f;
    public float happiness = 100f;
    public float maxHappiness = 100f;
    public float hunger = 100f;
    public float maxHunger = 100f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float actualSpeed = 1.5f;
    public float speedLevel = 1;
    public int currLevel = 1;
    public int expThresh = 100;
    public int exp = 0;
    public int skillPoints = 0;
    public int silverFish = 0;
    public int redFish = 0;
    public int greenFish = 0;
}
