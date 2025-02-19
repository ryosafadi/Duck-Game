using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void PlayerWhite()
    {
        Color color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
    public void PlayerYellow()
    {
        Color color = new Color(255f / 255f, 237f / 255f, 90f / 255f);
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
    public void PlayerBlue()
    {
        Color color = new Color(136f / 255f, 255f / 255f, 250f / 255f);
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
    public void PlayerPink()
    {
        Color color = new Color(255f / 255f, 136f / 255f, 203f / 255f);
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
    public void PlayerGreen()
    {
        Color color = new Color(115f / 255f, 234f / 255f, 114f / 255f);
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
}
