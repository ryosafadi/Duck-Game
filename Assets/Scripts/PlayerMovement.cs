using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    //public float moveSpeed;
    public float stoppingDistance; // How close the character should get to the mouse before stopping
    private CharacterController characterController;

   // public float maxStamina; // placeholder
    private float staminaDrainRate = 2.5f;
    private float dashSpeed = 3.5f;
    private float dashDuration = 0.75f;

    private bool isDashing = false;
    public bool isStunned = false;
    [SerializeField] private AudioClip Dash;
    private AudioSource moveAudioSource;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        moveAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Camera.ScreenToWorldPoint.html
        // Get the mouse position in world space
        if (!isStunned)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Calculate the direction to the mouse
            Vector3 direction = (mousePosition - transform.position).normalized;

            if (Input.GetMouseButtonDown(0) && !isDashing && IdleDuck.stamina > 0)
            {
                isDashing = true;
                moveAudioSource.PlayOneShot(Dash);
                StartCoroutine(DashCoroutine());
            }

            // Move the character towards the mouse position
            if (Vector3.Distance(transform.position, mousePosition) > stoppingDistance)
            {
                if (isDashing)
                {
                    characterController.Move(direction * IdleDuck.actualSpeed * dashSpeed * Time.deltaTime);
                    IdleDuck.stamina -= staminaDrainRate * dashSpeed * Time.deltaTime;
                }
                else
                {
                    characterController.Move(direction * IdleDuck.actualSpeed * Time.deltaTime);
                    IdleDuck.stamina -= staminaDrainRate * Time.deltaTime;
                }
            }

        }
        else
        {
            UnityEngine.Debug.Log("Not Moving");
        }

        if (IdleDuck.stamina <= 0f)
        {
            SceneManager.LoadScene("Idle Mode");
        }
    }

    IEnumerator DashCoroutine()
    {
        float dashTime = Time.time + dashDuration;

        while (Time.time < dashTime)
        {
            yield return null;
        }
        isDashing = false;
    }

    public float GetStamina()
    {
        return IdleDuck.stamina;
    }
}

