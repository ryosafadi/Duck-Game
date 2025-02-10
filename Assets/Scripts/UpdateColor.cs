using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = CustomizationManager.Instance.GetColor();
    }
}
