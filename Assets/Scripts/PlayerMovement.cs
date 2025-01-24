using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float stoppingDistance; // How close the character should get to the mouse before stopping
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Camera.ScreenToWorldPoint.html
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // Calculate the direction to the mouse
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Move the character towards the mouse position
        if (Vector3.Distance(transform.position, mousePosition) > stoppingDistance)
        {
            characterController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}

