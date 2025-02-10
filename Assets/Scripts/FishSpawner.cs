using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [System.Serializable]
    public class FishSpawnData
    {
        public GameObject fishPrefab; // Prefab of the fish
        public float shallowWeight;  // Spawn weight in shallow depth
        public float mediumWeight;   // Spawn weight in medium depth
        public float deepWeight;     // Spawn weight in deep depth
    }

    public List<FishSpawnData> fishToSpawn; // List of fish and their spawn weights
    public float spawnInterval = 2f;       // Time between spawns
    public Transform spawnArea;            // Area where fish can spawn
    public Transform player;               // Reference to the player (to check depth)
    public float shallowDepth = -5f;       // Y position for shallow depth
    public float mediumDepth = -15f;       // Y position for medium depth

    private float timer;

    void Start()
    {
        timer = spawnInterval; // Start spawning immediately
    }

    void Update()
    {
        // Move the spawn area to follow the player
        if (spawnArea != null && player != null)
        {
            spawnArea.position = new Vector3(player.position.x, player.position.y, spawnArea.position.z);
        }

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnFish();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnFish()
    {
        // Get the player's current depth
        float playerDepth = player.position.y;

        // Calculate total weight based on depth
        float totalWeight = 0f;
        foreach (var fishData in fishToSpawn)
        {
            totalWeight += GetWeightForDepth(fishData, playerDepth);
        }

        // Pick a random weight value
        float randomValue = Random.Range(0f, totalWeight);

        // Determine which fish to spawn based on the random value
        foreach (var fishData in fishToSpawn)
        {
            float weight = GetWeightForDepth(fishData, playerDepth);
            if (randomValue < weight)
            {
                Spawn(fishData.fishPrefab);
                return;
            }
            randomValue -= weight;
        }
    }

    float GetWeightForDepth(FishSpawnData fishData, float playerDepth)
    {
        // Determine spawn weight based on player depth
        if (playerDepth > shallowDepth)
        {
            Debug.Log("Player is in SHALLOW depth zone.");
            return fishData.shallowWeight; // Shallow depth
        }
        else if (playerDepth > mediumDepth)
        {
            Debug.Log("Player is in MEDIUM depth zone.");
            return fishData.mediumWeight;  // Medium depth
        }
        else
        {
            Debug.Log("Player is in DEEP depth zone.");
            return fishData.deepWeight;    // Deep depth
        }
    }

    void Spawn(GameObject fishPrefab)
    {
        // Randomize spawn position within the spawn area
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Instantiate the fish
        Instantiate(fishPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Get a random position within the spawn area
        Vector3 center = spawnArea.position;
        Vector3 size = spawnArea.localScale;

        float x = center.x + Random.Range(-size.x / 2f, size.x / 2f);
        float y = center.y + Random.Range(-size.y / 2f, size.y / 2f);

        return new Vector3(x, y, center.z);
    }
}