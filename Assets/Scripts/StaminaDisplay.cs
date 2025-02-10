using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private Image staminaBar;

    void Start()
    {
        staminaBar = GetComponent<Image>();
    }

    void Update()
    {
        float currentStamina = IdleDuck.stamina;

        staminaBar.fillAmount = currentStamina / 100f;
    }
}
