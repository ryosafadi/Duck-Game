using UnityEngine;

// Controls the random movement of fish in the active scene
public class RandomMovement : MonoBehaviour
{
    public float speed = 2f; 
    public float changeDirectionInterval = 1f; 
    public float rotationSpeed = 5f; 

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

        transform.Translate(randomDirection * speed * Time.deltaTime, Space.World);

        float angle = Mathf.Atan2(randomDirection.y, randomDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle + 180), Time.deltaTime * rotationSpeed);
    }

    Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
