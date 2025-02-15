using UnityEngine;

// Controls how fish run away from the player while facing the correct direction
public class RunAwayFromPlayer : MonoBehaviour
{
    public float speed = 1.5f; 
    public Transform player; 
    public float rotationSpeed = 5f; 

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (transform.position - player.position).normalized;

            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle + 180), Time.deltaTime * rotationSpeed);
        }
    }
}
