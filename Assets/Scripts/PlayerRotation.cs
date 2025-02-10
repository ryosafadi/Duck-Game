using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = -direction;
    }
}

//followed youtube tutorial: https://www.youtube.com/watch?v=9_i6S_rDZuA 