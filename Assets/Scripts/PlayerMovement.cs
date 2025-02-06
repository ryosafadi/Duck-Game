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
    private float currentStamina;
    private float staminaDrainRate = 3.5f;
    private float dashSpeed = 2.5f;
    private float dashDuration = 0.75f;

    private bool isDashing = false;

    // [SerializeField] IdleDuck duck;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentStamina = IdleDuck.stamina;
        // currentStamina = duck.GetStamina();
    }

    void Update()
    {
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Camera.ScreenToWorldPoint.html
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        if (Input.GetMouseButtonDown(0) && !isDashing && currentStamina > 0)
        {
            isDashing = true;
            StartCoroutine(DashCoroutine());
        }

        // Move the character towards the mouse position
        if (Vector3.Distance(transform.position, mousePosition) > stoppingDistance)
        {
            if (isDashing)
            {
                characterController.Move(direction * IdleDuck.speed * dashSpeed * Time.deltaTime);
                currentStamina -= staminaDrainRate * dashSpeed * Time.deltaTime;
            }
            else
            {
                characterController.Move(direction * IdleDuck.speed * Time.deltaTime);
                currentStamina -= staminaDrainRate * Time.deltaTime;
            }
            // change to display as a meter
            Debug.Log("Stamina: " + currentStamina);
        }

        if (currentStamina <= 0f)
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
        return currentStamina;
    }
}

