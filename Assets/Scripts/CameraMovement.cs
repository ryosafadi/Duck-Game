using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    private GameObject player;
    private Vector3 cameraOffset;
    private float smoothSpeed = 5f;

    void Start()
    {
        player = GameObject.Find("Player");
        cameraOffset = transform.position;
    }

    void Update()
    {
        // Calculate the desired position of the camera (with offset)
        Vector3 desiredPosition = player.transform.position + cameraOffset;

        // Smoothly interpolate between the camera's current position and the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        // Apply the smoothed position to the camera
        transform.position = smoothedPosition;
    }
}
