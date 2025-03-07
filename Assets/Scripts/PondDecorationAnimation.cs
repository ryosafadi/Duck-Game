using System.Collections;
using UnityEngine;

public class PondDecorationAnimation : MonoBehaviour
{
    [Header("Animation Settings")]
    public float animationRadius = 2f; // Distance at which player triggers animation
    public float swayAmount = 0.2f; // How much the decoration sways
    public float swaySpeed = 3f; // Speed of the sway animation
    public float returnSpeed = 1f; // Speed to return to original position
    
    [Header("Animation Variants")]
    public bool useRotationAnimation = true; // Sway like grass/reeds
    public bool useScaleAnimation = false; // Pulsate like lilies
    public bool useColorShift = false; // Subtle color shift
    
    [Header("Color Shift Settings")]
    public Color baseColor = Color.white; // Base decoration color
    public Color activeColor; // Color when player passes by
    
    [Header("References")]
    public PlayerMoveToClick playerScript; // Direct reference to the player script
    
    // Private variables
    private GameObject player;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;
    private float animationIntensity = 0f;
    private SpriteRenderer spriteRenderer;
    private bool isAnimating = false;
    
    void Start()
    {
        // Store original transform values
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalScale = transform.localScale;
        
        // Find the player script in the scene if not assigned
        if (playerScript == null)
        {
            playerScript = FindObjectOfType<PlayerMoveToClick>();
            if (playerScript == null)
            {
                Debug.LogWarning("PlayerMoveToClick script not found in scene. Please assign it manually to " + gameObject.name);
                enabled = false;
                return;
            }
        }
        
        player = playerScript.gameObject;
        
        // Get sprite renderer if using color shift
        if (useColorShift)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("Color shift animation enabled but no SpriteRenderer found on " + gameObject.name);
                useColorShift = false;
            }
            else if (activeColor == Color.clear)
            {
                // Create a slight variation of the base color if no active color was specified
                activeColor = new Color(
                    baseColor.r * 1.2f,
                    baseColor.g * 1.2f, 
                    baseColor.b * 1.2f, 
                    baseColor.a
                );
            }
        }
    }
    
    void Update()
    {
        if (player == null || playerScript == null) return;
        
        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(player.transform.position.x, player.transform.position.y)
        );
        
        // Check if player is moving
        bool playerIsMoving = playerScript.isMoving;
        
        // Only animate if player is moving and within radius
        if (playerIsMoving && distanceToPlayer < animationRadius)
        {
            // Calculate animation intensity based on proximity (closer = stronger effect)
            animationIntensity = Mathf.Lerp(animationIntensity, 
                                            1f - (distanceToPlayer / animationRadius), 
                                            Time.deltaTime * swaySpeed);
            
            // Start animation if not already animating
            if (!isAnimating)
            {
                isAnimating = true;
                StartCoroutine(AnimateDecoration());
            }
        }
        else
        {
            // Gradually reduce animation intensity
            animationIntensity = Mathf.Lerp(animationIntensity, 0f, Time.deltaTime * returnSpeed);
            
            // If intensity is very low, stop animation
            if (animationIntensity < 0.01f)
            {
                animationIntensity = 0f;
                isAnimating = false;
            }
        }
    }
    
    IEnumerator AnimateDecoration()
    {
        float randomOffset = Random.Range(0f, 2f * Mathf.PI); // Random starting phase
        
        while (isAnimating)
        {
            if (useRotationAnimation)
            {
                // Sway rotation based on sine wave
                float rotationAmount = Mathf.Sin(Time.time * swaySpeed + randomOffset) * swayAmount * animationIntensity;
                transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationAmount * 15f);
            }
            
            if (useScaleAnimation)
            {
                // Pulsate scale slightly
                float scaleMultiplier = 1f + Mathf.Sin(Time.time * swaySpeed * 0.5f + randomOffset) * (swayAmount * 0.2f) * animationIntensity;
                transform.localScale = new Vector3(
                    originalScale.x * scaleMultiplier,
                    originalScale.y * scaleMultiplier,
                    originalScale.z
                );
            }
            
            if (useColorShift && spriteRenderer != null)
            {
                // Shift color slightly
                spriteRenderer.color = Color.Lerp(baseColor, activeColor, animationIntensity * 0.7f);
            }
            
            yield return null;
        }
        
        // Return to original state
        transform.rotation = originalRotation;
        transform.localScale = originalScale;
        if (spriteRenderer != null) spriteRenderer.color = baseColor;
    }
    
    // Visualize the animation radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.2f, 0.8f, 0.2f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, animationRadius);
    }
}