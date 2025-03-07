using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TrashSpawn : MonoBehaviour
{
    [Header("Trash Prefab")]
    public GameObject trashPrefab;  // Assign your trash prefab here
    public GameObject trashPrefab2;
    public GameObject trashPrefab3;
    private GameObject spawningPrefab;

    [Header("Spawn Settings")]
    public float spawnInterval = 5f;   // How often (in seconds) to spawn
    private float timer;
    private float randomGeneration;

    [Header("Camera Reference")]
    public Camera mainCamera;          // Reference to your orthographic camera

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;  // Fallback to the main camera if not assigned
        }

        timer = spawnInterval;         // Start spawning immediately
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            UnityEngine.Debug.Log("Trash Spawned");
            SpawnTrash();
            timer = spawnInterval;     // Reset timer
        }
    }

    private void SpawnTrash()
    {
        if (trashPrefab == null || mainCamera == null)
        {
            UnityEngine.Debug.LogWarning("Missing trash prefab or camera reference.");
            return;
        }
        randomGeneration = UnityEngine.Random.Range(0, 3);
        if(randomGeneration == 0)
        {
            spawningPrefab = trashPrefab;
        }
        else if(randomGeneration == 1)
        {
            spawningPrefab = trashPrefab2;
        }
        else
        {
            spawningPrefab = trashPrefab3;
        }
        Vector3 spawnPosition = GetTopOfCameraPosition();
        if(spawningPrefab.activeSelf == false)
        {
            spawningPrefab.SetActive(true);
        }
        Instantiate(spawningPrefab, spawnPosition, Quaternion.identity);
    }

    /// <summary>
    /// Finds a random position along the top of an **orthographic** camera�s view.
    /// </summary>
    private Vector3 GetTopOfCameraPosition()
    {
        // For an orthographic camera in 2D:
        // - 'orthographicSize' is half the vertical size of the camera's view.
        // - 'orthographicSize * camera.aspect' is half the horizontal size.

        float camY = mainCamera.transform.position.y;
        float camX = mainCamera.transform.position.x;
        float camSize = mainCamera.orthographicSize;     // vertical half-size
        float camAspect = mainCamera.aspect;             // width/height ratio

        // Calculate top edge (Y)
        float topEdge = camY + 5 + camSize;

        // Calculate left & right edges (X)
        float leftEdge = camX - (camSize * camAspect) + 10;
        float rightEdge = camX + (camSize * camAspect) - 10;

        // Pick a random X between left and right
        float randomX = UnityEngine.Random.Range(leftEdge, rightEdge);

        // We�ll fix Z=0 for a 2D game (adjust if you have a different setup)
        return new Vector3(randomX, topEdge, 0f);
    }
}
