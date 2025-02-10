using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCanvas : MonoBehaviour
{
    public GameObject canvas;

    public void Enable()
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

    public void Disable()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
}
