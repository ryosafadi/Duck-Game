using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMoveToClick : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private Vector3 originalScale;

    [Header("Boundary Settings")]
    public GameObject pondObject;
    private PolygonCollider2D pondCollider;

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

    [Header("Random Movement Settings")]
    public float randomMoveInterval = 3f;
    private float randomMoveTimer;

    [Header("AccessoryCosemic")]
    public GameObject hat;

    void Start()
    {
        originalScale = transform.localScale;
        targetPosition = transform.position;
        currentPosition = transform.position;

        if (pondObject != null)
        {
            pondCollider = pondObject.GetComponent<PolygonCollider2D>();
            if (pondCollider == null)
            {
                pondCollider = pondObject.AddComponent<PolygonCollider2D>();
            }
        }

        randomMoveTimer = randomMoveInterval;
    }

    void Update()
    {
        randomMoveTimer -= Time.deltaTime;

        if (randomMoveTimer <= 0f && !isMoving)
        {
            SetRandomTargetPosition();
            randomMoveTimer = randomMoveInterval;
        }

        if (isMoving)
        {

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

        // Handle click movement (right-click to move to a clicked position)
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (pondCollider != null)
            {
                if (pondCollider.OverlapPoint(clickPosition))
                {
                    targetPosition = clickPosition;
                }
                else
                {
                    UnityEngine.Debug.Log("Before: "+clickPosition);
                    targetPosition = FindClosestPointOnPondEdge(clickPosition);
                    UnityEngine.Debug.Log("After: "+targetPosition);
                }
            }
            else
            {
                targetPosition = clickPosition;
            }

            isMoving = true;
            if (currentPosition.x < targetPosition.x)
            {
                UnityEngine.Debug.Log("Less than");
                GetComponent<SpriteRenderer>().flipX = true;
                if (hat) {
                    hat.GetComponent<SpriteRenderer>().flipX = true;
                }
                
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                if (hat)
                {
                    hat.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
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

    void SetRandomTargetPosition()
    {
        // Pick a random point within the bounds of the pond collider
        Vector3 randomPoint = (Vector3)pondCollider.bounds.center +
    new Vector3(UnityEngine.Random.Range(-pondCollider.bounds.size.x / 2, pondCollider.bounds.size.x / 2),
                UnityEngine.Random.Range(-pondCollider.bounds.size.y / 2, pondCollider.bounds.size.y / 2), 0f);


        // Check if the random point is within the pond collider; if not, pick a point on the edge
        if (pondCollider.OverlapPoint(randomPoint))
        {
            targetPosition = randomPoint;
        }
        else
        {
            targetPosition = FindClosestPointOnPondEdge(randomPoint);
        }

        isMoving = true;
        if (currentPosition.x < targetPosition.x)
        {
            UnityEngine.Debug.Log("Less than");
            Vector3 scale = transform.localScale;
            UnityEngine.Debug.Log(scale);
            GetComponent<SpriteRenderer>().flipX = true;
            if (hat)
            {
                hat.GetComponent<SpriteRenderer>().flipX = true;
            }
            UnityEngine.Debug.Log(scale);
            transform.localScale = scale;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (hat)
            {
                hat.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    Vector2 FindClosestPointOnPondEdge(Vector2 outsidePoint)
    {
        Vector2[] points = pondCollider.points;
        Vector2 closestPoint = points[0];
        float closestDistance = Vector2.Distance(outsidePoint, points[0]);

        for (int i = 0; i < points.Length; i++)
        {
            Vector2 p1 = pondCollider.transform.TransformPoint(points[i]);
            Vector2 p2 = pondCollider.transform.TransformPoint(points[(i + 1) % points.Length]);
            
            Vector2 closestOnSegment = GetClosestPointOnLineSegment(p1, p2, outsidePoint);
            
            float distance = Vector2.Distance(outsidePoint, closestOnSegment);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = closestOnSegment;
            }
        }
        return closestPoint;
    }

    Vector2 GetClosestPointOnLineSegment(Vector2 p1, Vector2 p2, Vector2 outsidePoint)
    {
        Vector2 segmentDirection = p2 - p1;
        Vector2 p1ToPoint = outsidePoint - p1;

        float segmentLengthSquared = segmentDirection.sqrMagnitude;
        if (segmentLengthSquared == 0f)
        {
            return p1;
        }

        float t = Mathf.Clamp(Vector2.Dot(p1ToPoint, segmentDirection) / segmentLengthSquared, 0f, 1f);

        return p1 + t * segmentDirection;
    }

    void OnDrawGizmos()
    {
        if (pondObject != null)
        {
            PolygonCollider2D editorPondCollider = pondObject.GetComponent<PolygonCollider2D>();
            if (editorPondCollider != null)
            {
                Gizmos.color = Color.cyan;

                Vector2[] points = editorPondCollider.points;
                for (int i = 0; i < points.Length; i++)
                {
                    Vector2 worldPoint1 = pondObject.transform.TransformPoint(points[i]);
                    Vector2 worldPoint2 = pondObject.transform.TransformPoint(points[(i + 1) % points.Length]);

                    Gizmos.DrawLine(worldPoint1, worldPoint2);
                }
            }
        }
    }
}
