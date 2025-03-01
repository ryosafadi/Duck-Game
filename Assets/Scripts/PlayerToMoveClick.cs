using UnityEngine;
using System.Collections.Generic;

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
    }

    void Update()
    {
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
                    targetPosition = FindClosestPointOnPondEdge(clickPosition);
                }
            }
            else
            {
                targetPosition = clickPosition;
            }
            
            isMoving = true;
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

    Vector2 FindClosestPointOnPondEdge(Vector2 outsidePoint)
    {
        Vector2 pondCenter = pondCollider.bounds.center;
        
        Vector2 direction = (outsidePoint - pondCenter).normalized;
        
        RaycastHit2D hit = Physics2D.Raycast(pondCenter, direction, 100f);
        
        if (hit.collider != null && hit.collider == pondCollider)
        {
            // Hit the edge of the pond
            return hit.point;
        }
        
        return pondCollider.bounds.ClosestPoint(outsidePoint);
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