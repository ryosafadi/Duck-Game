using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AccessorySelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite hat;
    public Sprite bow;
    public Sprite glasses;
    public Sprite cone;

    public void PlayerHat()
    {
        spriteRenderer.sprite = hat;
        CustomizationManager.Instance.SetAccessory(hat);
    }

    public void PlayerBow()
    {
        spriteRenderer.sprite = bow;
        CustomizationManager.Instance.SetAccessory(bow);
    }

    public void PlayerGlasses()
    {
        spriteRenderer.sprite = glasses;
        CustomizationManager.Instance.SetAccessory(glasses);
    }

    public void PlayerCone()
    {
        spriteRenderer.sprite = cone;
        CustomizationManager.Instance.SetAccessory(cone);
    }
}
