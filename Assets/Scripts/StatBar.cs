using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{

    public enum StatType { Happiness, Hunger, Stamina }

    [SerializeField] IdleDuck duck;
    [SerializeField] StatType stat;

    private Image statBar;

    private void Start()
    {
        statBar = GetComponent<Image>();
    }

    private void Update()
    {
        switch (stat)
        {
            case StatType.Happiness:
                statBar.fillAmount = duck.GetHappiness() / IdleDuck.maxHappiness;
                break;

            case StatType.Hunger:
                statBar.fillAmount = duck.GetHunger() / IdleDuck.maxHunger;
                break;

            case StatType.Stamina:
                statBar.fillAmount = duck.GetStamina() / IdleDuck.maxStamina;
                break;

            default:
                break;
        }
    }
}
    
