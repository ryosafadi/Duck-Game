using UnityEngine;

//Controls the random movement of fish in the active scene
public class RandomMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public float changeDirectionInterval = 1f; // How often the direction changes
    private Vector2 randomDirection;
    private float timer;

    void Start()
    {
        timer = changeDirectionInterval;
        randomDirection = GetRandomDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            randomDirection = GetRandomDirection();
            timer = changeDirectionInterval;
        }

        transform.Translate(randomDirection * speed * Time.deltaTime);
    }

    Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}