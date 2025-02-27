using UnityEngine;

public class PlayerMoveToClick : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Vector3 originalScale;

    [Header("Idle Animation")]
    public float breathingSpeed = 1f;
    public float breathingAmount = 0.1f;
    public float idleBounceHeight = 0.1f;
    public float idleBounceSpeed = 1.5f;

    [Header("Walking Animation")]
    public float walkBounceHeight = 0.3f;
    public float walkBounceSpeed = 4f;
    public float walkSquashAmount = 0.2f;
    private Vector3 currentPosition;

    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;
        currentPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if (isMoving)
        {
            // Move towards target
            currentPosition = Vector2.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
            
            // Walking animation
            float bounceOffset = Mathf.Sin(Time.time * walkBounceSpeed) * walkBounceHeight;
            transform.position = new Vector3(currentPosition.x, currentPosition.y + bounceOffset, currentPosition.z);

            // Squash and stretch while walking
            float squashStretch = 1f + Mathf.Sin(Time.time * walkBounceSpeed * 2f) * walkSquashAmount;
            transform.localScale = new Vector3(
                originalScale.x * (2f - squashStretch),
                originalScale.y * squashStretch,
                originalScale.z
            );

            // Check if reached destination
            if (Vector2.Distance(currentPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
            }
        }
        else
        {
            // Idle animations
            ApplyIdleAnimations();
        }
    }

    void ApplyIdleAnimations()
    {
        // Gentle breathing
        float breathing = 1f + Mathf.Sin(Time.time * breathingSpeed) * breathingAmount;
        transform.localScale = new Vector3(
            originalScale.x * breathing,
            originalScale.y * breathing,
            originalScale.z
        );

        // Gentle bounce
        float idleBounce = Mathf.Sin(Time.time * idleBounceSpeed) * idleBounceHeight;
        transform.position = new Vector3(currentPosition.x, currentPosition.y + idleBounce, currentPosition.z);
    }
}