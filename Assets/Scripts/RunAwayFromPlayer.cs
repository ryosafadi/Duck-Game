using UnityEngine;

//Controls how fish run away from the player
public class RunAwayFromPlayer : MonoBehaviour
{
    public float speed = 1.5f; // Speed of movement (slower than the player)
    public Transform player; // Reference to the player

    void Update()
    {
        if (player != null)
        {
            // Calculate direction away from the player
            Vector2 direction = (transform.position - player.position).normalized;

            // Move away from the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}