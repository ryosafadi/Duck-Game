using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{

    public enum StatType { Happiness, Hunger }

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
                statBar.fillAmount = duck.GetHappiness() / 100f;
                break;

            case StatType.Hunger:
                statBar.fillAmount = duck.GetHunger() / 100f;
                break;

            default:
                break;
        }
    }
}
    
