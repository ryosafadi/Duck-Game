using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public void PlayerWhite()
    {
        SetAndSaveColor(new Color(255f / 255f, 255f / 255f, 255f / 255f));
    }
    public void PlayerYellow()
    {
        SetAndSaveColor(new Color(255f / 255f, 237f / 255f, 90f / 255f));
    }
    public void PlayerBlue()
    {
        SetAndSaveColor(new Color(136f / 255f, 255f / 255f, 250f / 255f));
    }
    public void PlayerPink()
    {
        SetAndSaveColor(new Color(255f / 255f, 136f / 255f, 203f / 255f));
    }
    public void PlayerGreen()
    {
        SetAndSaveColor(new Color(115f / 255f, 234f / 255f, 114f / 255f));
    }

    private void SetAndSaveColor(Color color)
    {
        CustomizationManager.Instance.SetColor(color);
        spriteRenderer.color = color;
    }
}
