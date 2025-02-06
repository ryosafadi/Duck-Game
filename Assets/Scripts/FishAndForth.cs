using UnityEngine;

public class FishAndForth : MonoBehaviour
{
    public GameObject puffer; // Reference to the child Puffer GameObject with the Animator
    private Vector3 stopA;    // First stop position
    private Vector3 stopB;    // Second stop position
    private float speed = 1.19f; // Speed of movement
    private Animator pufferAnimator; // Reference to the Animator component on the child
    private Vector3 lastPosition; // To track the fish's last position

    void Start()
    {
        // Initialize stopA as the fish's starting position
        stopA = transform.position;

        // Randomize the x-coordinate for stopB
        float randomOffset = UnityEngine.Random.Range(4, 10);
        stopB = stopA + new Vector3(randomOffset, 0, 0);

        // Get the Animator component from the child Puffer GameObject
        if (puffer != null)
        {
            pufferAnimator = puffer.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("Puffer GameObject is not assigned in the inspector!");
        }

        // Initialize lastPosition
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate the interpolation factor using PingPong
        float time = Mathf.PingPong(Time.time * speed, 1);

        // Move the parent Fish GameObject between stopA and stopB
        transform.position = Vector3.Lerp(stopA, stopB, time);

        // Determine the direction of movement
        Vector3 currentPosition = transform.position;
        float direction = currentPosition.x - lastPosition.x;

        // Update the Animator parameter on the child Puffer GameObject
        if (pufferAnimator != null)
        {
            pufferAnimator.SetFloat("Direction", direction);
        }

        // Update lastPosition for the next frame
        lastPosition = currentPosition;
    }
}