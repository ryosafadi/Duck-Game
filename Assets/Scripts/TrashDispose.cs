using UnityEngine;

public class TrashDispose : MonoBehaviour
{
    [SerializeField] float attackRange = 2f;
    [SerializeField] float knockbackPower = 5f;
    [SerializeField] float knockbackDuration = 0.5f;
    [SerializeField] float knockbackDelay = 1.0f;    // in seconds

    [SerializeField] private AudioClip hitSound; //assign in inspector


    [SerializeField] GameObject player;

    // We'll track the knockback timing on the player
    private float knockbackTimer = 0f;
    private Vector3 knockbackDirection;

    // We'll track the "attack cooldown" (how long until we can knock back again)
    private float attackCooldownTimer = 0f;

    private CharacterController playerCC;

    private AudioSource audioSource;

    void Start()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        playerCC = player.GetComponent<CharacterController>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Decrement our cooldown timer if needed
        if (attackCooldownTimer > 0f)
            attackCooldownTimer -= Time.deltaTime;

        // If the player is in the middle of a knockback
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.deltaTime;

            // Move the player each frame of the knockback
            playerCC.Move(knockbackDirection * (knockbackPower * Time.deltaTime));
        }
        else
        {
            // Do normal movement for the player if you want (or let the player's own script handle it)
        }

        // Attempt a new knockback only if our cooldown is up
        if (attackCooldownTimer <= 0f)
        {
            float dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < attackRange)
            {
                // Begin knockback
                knockbackTimer = knockbackDuration;
                knockbackDirection = (player.transform.position - transform.position).normalized;

                if (hitSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }

                // Reset the attack cooldown
                attackCooldownTimer = knockbackDelay;
            }
        }
    }
}
