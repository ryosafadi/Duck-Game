using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Image redOverlay;
    [SerializeField] private float warningThreshold = 40f; // can change this in inspector 
    private Image staminaBar;

    void Start()
    {
        staminaBar = GetComponent<Image>();
    }

    void Update()
    {
        float currentStamina = IdleDuck.stamina;

        staminaBar.fillAmount = currentStamina / 100f;

        // starts showing red border at threshold number we choose in inspector
        if (currentStamina <= warningThreshold)
        {
            float alpha = Mathf.Lerp(0f, 1f, (warningThreshold - currentStamina) / warningThreshold);
            redOverlay.color = new Color(redOverlay.color.r, redOverlay.color.g, redOverlay.color.b, alpha);
        }
        else
        {
            redOverlay.color = new Color(redOverlay.color.r, redOverlay.color.g, redOverlay.color.b, 0f);
        }
    }
}
